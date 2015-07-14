#call avz_mrc_spInsertProcessorActivities()

drop procedure if exists avz_mrc_spInsertProcessorActivities;
delimiter $$;
CREATE  PROCEDURE `avz_mrc_spInsertProcessorActivities`(iNprovidedXml text)
BEGIN

    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned; 
    declare _setFlag int unsigned default 0;
	declare _processedDate date ;
      declare _activityType  int unsigned;
	declare _merchantID, _contractid bigint default null;
	declare _OwnedAmount,_LoanedAmount,iappliedAmount,_Balance,_PaidAmount double default 0.0;
    -- calculate the number of row elements.   
     set pRowCount  = extractValue(iNprovidedXml, 'count(//processoractivity)');
    
 InsertActivities:   while pRowIndex <= pRowCount do
   
  set  _processedDate= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processedDate'), ''),'1900-01-01');
set _activityType=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@activityType'), ''),0);
set _merchantID = NUll;   
Select C.merchantId, C.ContractId,C.OwnedAmount,C.LoanedAmount into
_merchantID,_contractid,_OwnedAmount,_LoanedAmount from tb_processor P JOIN tb_contracts C on P.MerchantId=C.MerchantId and C.StatusId=20007
 where processornumber= COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@pMerchantNumber'),''));
set iappliedAmount=    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@withdrawalAmount'),''),0);

if exists(Select contractid from tb_processorActivities where  merchantid=_merchantID and contractid=_Contractid) then
Select ifnull(balanceamount,_OwnedAmount),ifnUll(paidamount,0) into _Balance,_PaidAmount from tb_processoractivities 
where merchantid=_merchantID and contractid=_Contractid and activityid=(Select max(activityid) from tb_processoractivities WHERE MERCHANTID=_merchantID);
	else
 set _Balance=_OwnedAmount;
set _PaidAmount=0;
end if;

If(isnull(_merchantID) OR isnull(_contractID)) then
   set pRowIndex = pRowIndex + 1;
Select 'Continue';
	ITERATE InsertActivities;
end if;

/*

[0]- Processor Processed date - processed date DONE
 [1]-AMI Processor Code - AMIProcessorCode
 [2]- Processor Merchant Number, - ProcessorMID DONE
 [3]-Retrieval source -  RetrievalSource DONE
[4]-Total Amount,- Total Amount DONE
[5]-Withdrawal Amount,-APPLIED AMOUNT DONE
[6]-Activity type,- ActivityTypeID
[7]-(Specified Percentage) Rate,- Rate DONE
 [8]-Start date- start date DONE
 [9]-End date,- startdatte DONE
*/  
#if exists (select 1 from  tb_processoractivities where ProcessedDate=_processedDate and ActivityTypeId=_activityType)=0 then 

insert into  tb_processoractivities
(
MerchantId,
contractID,
price,
capital,
BalanceAmount,
PaidAmount,
processeddate,
AMIProcessorCode,
ProcessorMID,
RetrievalSource,
TotalAmount,
AppliedAmount,
ActivityTypeId,
Rate,
StartDate,
EndDate,
Insertdate,
Insertuserid,
ProcessorId
)
SELECT 
_merchantID,
_contractid,
((_LoanedAmount/_OwnedAmount)*iappliedAmount),
(((_OwnedAmount-_LoanedAmount)/_OwnedAmount)*iappliedAmount),
_Balance-(-1*iappliedAmount),_PaidAmount+(-1*iappliedAmount),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processedDate'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processorCode'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@pMerchantNumber'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@retrievalSource'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@totalAmount'),
                    ''),
            0),
    iappliedAmount,
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@activityType'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@specifiedRate'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@startDate'),
                    ''),
            '1900-01-01'),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@endDate'),
                    ''),
            '1900-01-01'),
current_date(),1,
(Select ProcessorID from lkp_tb_processorlist where processorcode=     COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processorCode'),
                    ''),
            0));

Update tb_contracts set 
paidAmount= _PaidAmount+(-1*iappliedAmount), 
statusid=case when (_Balance-(-1*iappliedAmount))<=0 then 20008
else
statusid
end
where contractId=_contractID;
   
 /*  else 

UPDATE tb_processoractivities 
SET 
    processeddate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processedDate'),
                    ''),
            0),
    AMIProcessorCode = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processorCode'),
                    ''),
            0),
    ProcessorMID = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@pMerchantNumber'),
                    ''),
            0),
    RetrievalSource = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@retrievalSource'),
                    ''),
            0),
    TotalAmount = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@totalAmount'),
                    ''),
            0),
    AppliedAmount = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@withdrawalAmount'),
                    ''),
            0),
    ActivityTypeId = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@activityType'),
                    ''),
            0),
    StartDate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@specifiedRate'),
                    ''),
            '1900-01-01'),
    EndDate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@startDate'),
                    ''),
            '1900-01-01'),
    ProcessedDate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@endDate'),
                    ''),
            '1900-01-01')
WHERE
    ProcessedDate = _processedDate
        AND ActivityTypeId = _activityType;
   
   
   end if;*/

      set pRowIndex = pRowIndex + 1;

	 end while;

END