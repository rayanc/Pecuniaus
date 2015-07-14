#call avz_mccv_spUpdateCalculatedCCV();
drop procedure if exists avz_mccv_spUpdateCalculatedCCV;
delimiter $$

Create procedure avz_mccv_spUpdateCalculatedCCV()
begin
DECLARE pProcessorMID varchar(25);
DECLARE pProcessorID SMALLINT(6);
DECLARE pmerchantId INT;
DECLARE pcontractID INT;
DECLARE pAppliedAmount , pRetention double;


DECLARE _finished INTEGER DEFAULT 0;
declare _exists int;

Declare MCCVMerchants Cursor for 
Select MerchantID,contractid from tb_merchantretrievalratio where DaysWithoutActivity=0;
DECLARE CONTINUE HANDLER FOR NOT FOUND SET _finished = 1;

OPEN MCCVMerchants;
 
GETDATA: loop
fetch MCCVMerchants into pmerchantId,pContractID;

IF _finished = 1 THEN 
LEAVE GETDATA;
END IF;

Select pmerchantId,pContractID;

Set pProcessorMID=null;
set pProcessorID=null;

Select retention into pRetention from tb_offers where contractId=pcontractID and Status=190001;
Select processorMID,ProcessorId,appliedamount into pProcessorMID,pProcessorID,pAppliedAmount from tb_processorActivities where merchantID=pmerchantID order by processeddate desc limit 1;

if(exists(Select * from tb_creditcardactivity where Month=month(current_date) and Year=Year(current_date) and contractId=pcontractID)=0) then
insert into tb_creditcardactivity (
`MerchantId`,
`ProcessorId`,
`ProcessorMerchantId`,
`AcctivityTypeId`,
`CurrencyId`,
`Month`,
`Year`,
`TotalAmount`,
`TotalTickets`,
`StartDate`,
`EndDate`,
`ProcessedDate`,
`IndustryId`,
`MerchantStatusId`,
`AuthorizedOwnerId`,
`RetrievalSource`,
`ContractId`,
`IsCalculated`)
values
(
pmerchantId,
pProcessorID,
pProcessorMID,
1200,
1,
Month(current_date),
Year(current_date),
(pAppliedAmount*100/pRetention),
0,
current_date,
current_date,
current_date,
null,
null,
null,
null,
pContractID,
1
);
end if;

if(exists(Select * from tb_creditcardvolumes where Month=month(current_date) and Year=Year(current_date) and contractId=pcontractID)=0) then
INSERT INTO `tb_creditcardvolumes`
(
`merchantId`,
`month`,
`year`,
`totaltickets`,
`totalAmount`,
`processorId`,
`contractId`)

Select `merchantId`,
`month`,
`year`,
Sum(`totaltickets`),
Sum(`totalAmount`),
`processorId`,
`contractId` from
tb_creditcardactivity
 where merchantId=pmerchantId
AND IsCalculated = 1
AND Month=month(current_date) and Year=Year(current_date) and contractId=pContractID
Group by contractId, year,month ;

else

update tb_creditcardvolumes Set 
totalAmount=(Select 
Sum(`totalAmount`)
 from
tb_creditcardactivity
 where merchantId=pmerchantId
AND IsCalculated = 1
AND Month=month(current_date) and Year=Year(current_date) and contractId=pContractID
Group by contractId, year,month)
where merchantId=pmerchantId
AND IsCalculated = 1
AND Month=month(current_date) and Year=Year(current_date) and contractId=pContractID ;
end if;

update tb_merchants set avgmccv=(Select avg(totalamount) from tb_creditcardvolumes where merchantid=pmerchantId) where merchantid=pmerchantId;

END LOOP GETDATA;
end  $$

Delimiter ;