drop procedure if exists avz_MRC_spretrieveMerchantStatementSummaryInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantStatementSummaryInformation(
iNmerchantId bigint,
iNStatementFrom datetime,
iNStatementTo datetime
)
begin
declare iNNcfNumber varchar(20) default null;

#select month(iNStatementFrom),month(iNStatementTo),day(iNStatementFrom),day(iNStatementTo),day(last_day(iNStatementTo));

#if(month(iNStatementFrom)=month(iNStatementTo) and day(iNStatementFrom)=1 and day(iNStatementTo)=day(last_day(iNStatementTo)))
if(DAYOFMONTH(iNStatementFrom)=1 AND LAST_DAY(iNStatementFrom)=iNStatementTo)
then
begin
if exists(select * from tb_ncfnumber)
then
begin
select NcfNumber into iNNcfNumber from tb_ncfnumber;
end;
else 
begin
	insert into tb_ncfnumber(ncfnumber,ncfnumberdate) select FLOOR(RAND() * 401) + 100,now();
select NcfNumber into iNNcfNumber from tb_ncfnumber;
end;
end if;
end;
else
begin
set iNNcfNumber='';
end;
end if;

if exists (Select ACT.AppliedAmount from tb_processoractivities ACT Where ActivityTypeId=20 
and ACT.MerchantId=iNmerchantId and ACT.ProcessedDate>=IF(iNStatementFrom='1900-01-01',ACT.ProcessedDate,iNStatementFrom) 
and ACT.ProcessedDate<=If(iNStatementTo='1900-01-01',ACT.ProcessedDate,iNStatementTo))
then
begin
if(iNmerchantId=0) then
select Distinct ifnull(iNNcfNumber,'') NFC,MRC.MerchantId TradeID,MRC.BusinessName FirmName,MRC.RNCNumber RNC,MRC.legalName TradeName,
ADR.Address1 Address,City,Phone1,email1 Email,CONCAT('$', FORMAT(-1*sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end), 2)) TotalCashRecieved,
CONCAT('$', FORMAT(sum(case when ACT.ActivityTypeId!=4 then 0 else case when ACT.AppliedAmount<0 then -1*ACT.AppliedAmount else ACT.AppliedAmount end end), 2)) TotalAdjustmentApplied,
CONCAT('$', FORMAT(-1*(sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end)+sum(case when  ACT.ActivityTypeId=4 then  ACT.AppliedAmount else 0 end )), 2))  TotalCashApplied,CONCAT('$', FORMAT(Sum(ACT.Price), 2)) TotalPriceApplied,CONCAT('$', FORMAT(CNT.OwnedAmount-(Select sum(-1*AppliedAmount) from tb_processoractivities where contractId=CNT.contractId and processeddate<=iNStatementTo AND ActivityTypeId!=4), 2)) OutstandingBalance,CONCAT('$', FORMAT(CNT.OwnedAmount, 2)) RightstoRecieve,CONCAT('$', FORMAT(case when month(iNStatementTo)=month(CNT.fundingdate) AND month(iNStatementTo)=month(CNT.fundingdate) then CNT.AdministrativeExpenses else 0 end, 2)) AdminCharges
from tb_processoractivities ACT
inner join tb_processor PR on PR.ProcessorTypeId=ACT.ProcessorId AND ACT.MerchantId=PR.MerchantId
inner join lkp_tb_processorlist LKP on LKP.ProcessorId=PR.ProcessorTypeId
inner join tb_contracts CNT on CNT.contractId=ACT.contractID
inner join tb_merchants MRC on MRC.MerchantId=ACT.MerchantId
inner join tb_addresses ADR on ADR.AddressId=MRC.AddressId
Where ACT.ProcessedDate>=IF(iNStatementFrom='1900-01-01',ACT.ProcessedDate,iNStatementFrom) and 
ACT.ProcessedDate<=If(iNStatementTo='1900-01-01',ACT.ProcessedDate,iNStatementTo);
else
select Distinct ifnull(iNNcfNumber,'') NFC,MRC.MerchantId TradeID,MRC.BusinessName FirmName,MRC.RNCNumber RNC,MRC.legalName TradeName,
ADR.Address1 Address,City,Phone1,email1 Email,CONCAT('$', FORMAT(-1*sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end), 2)) TotalCashRecieved,
CONCAT('$', FORMAT(sum(case when ACT.ActivityTypeId!=4 then 0 else case when ACT.AppliedAmount<0 then -1*ACT.AppliedAmount else ACT.AppliedAmount end end), 2)) TotalAdjustmentApplied,
CONCAT('$', FORMAT(-1*(sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end)+sum(case when ACT.ActivityTypeId=4 then  ACT.AppliedAmount else 0 end )), 2))  TotalCashApplied,CONCAT('$', FORMAT(Sum(ACT.Price), 2)) TotalPriceApplied,CONCAT('$', FORMAT(CNT.OwnedAmount-(Select sum(-1*AppliedAmount) from tb_processoractivities where contractId=CNT.contractId and processeddate<=iNStatementTo AND ActivityTypeId!=4), 2)) OutstandingBalance,CONCAT('$', FORMAT(CNT.OwnedAmount, 2)) RightstoRecieve,CONCAT('$', FORMAT(case when month(iNStatementTo)=month(CNT.fundingdate) AND month(iNStatementTo)=month(CNT.fundingdate) then CNT.AdministrativeExpenses else 0 end, 2)) AdminCharges
from tb_processoractivities ACT
inner join tb_processor PR on PR.ProcessorTypeId=ACT.ProcessorId AND ACT.MerchantId=PR.MerchantId
inner join lkp_tb_processorlist LKP on LKP.ProcessorId=PR.ProcessorTypeId
inner join tb_contracts CNT on CNT.contractId=ACT.contractID
inner join tb_merchants MRC on MRC.MerchantId=ACT.MerchantId
inner join tb_addresses ADR on ADR.AddressId=MRC.AddressId
Where ACT.MerchantId=iNmerchantId and 
ACT.ProcessedDate>=IF(iNStatementFrom='1900-01-01',ACT.ProcessedDate,iNStatementFrom) and 
ACT.ProcessedDate<=If(iNStatementTo='1900-01-01',ACT.ProcessedDate,iNStatementTo);
end if;
end;
else
begin
if(iNmerchantId=0) then
select Distinct ifnull(iNNcfNumber,'') NFC,MRC.MerchantId TradeID,MRC.BusinessName FirmName,MRC.RNCNumber RNC,MRC.legalName TradeName,
ADR.Address1 Address,City,Phone1,email1 Email,CONCAT('$', FORMAT(-1*sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end), 2)) TotalCashRecieved,
CONCAT('$', FORMAT(sum(case when ACT.ActivityTypeId!=4 then 0 else case when ACT.AppliedAmount<0 then -1*ACT.AppliedAmount else ACT.AppliedAmount end end), 2))TotalAdjustmentApplied,
CONCAT('$', FORMAT(-1*(sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end)+sum(case when  ACT.ActivityTypeId=4 then  ACT.AppliedAmount else 0 end )), 2))  TotalCashApplied,CONCAT('$', FORMAT(Sum(ACT.Price), 2)) TotalPriceApplied,CONCAT('$', FORMAT(CNT.OwnedAmount-(Select sum(-1*AppliedAmount) from tb_processoractivities where contractId=CNT.contractId and processeddate<=iNStatementTo AND ActivityTypeId!=4), 2)) OutstandingBalance,CONCAT('$', FORMAT(CNT.OwnedAmount, 2)) RightstoRecieve,CONCAT('$', FORMAT(case when month(iNStatementTo)=month(CNT.fundingdate) AND month(iNStatementTo)=month(CNT.fundingdate) then CNT.AdministrativeExpenses else 0 end, 2)) AdminCharges
from tb_processoractivities ACT
inner join tb_processor PR on PR.ProcessorTypeId=ACT.ProcessorId AND ACT.MerchantId=PR.MerchantId
inner join lkp_tb_processorlist LKP on LKP.ProcessorId=PR.ProcessorTypeId
inner join tb_contracts CNT on CNT.contractId=ACT.contractID
inner join tb_merchants MRC on MRC.MerchantId=ACT.MerchantId
inner join tb_addresses ADR on ADR.AddressId=MRC.AddressId
Where ACT.ProcessedDate>=IF(iNStatementFrom='1900-01-01',ACT.ProcessedDate,iNStatementFrom) and 
ACT.ProcessedDate<=If(iNStatementTo='1900-01-01',ACT.ProcessedDate,iNStatementTo);
else
/*select Distinct iNNcfNumber NFC,MRC.MerchantId TradeID,MRC.BusinessName FirmName,MRC.RNCNumber RNC,MRC.legalName TradeName,
ADR.Address1 Address,City,Phone1,email1 Email,CONCAT('$', FORMAT(sum(ACT.AppliedAmount), 2)) TotalCashRecieved,
CONCAT('$', FORMAT(0, 2))TotalAdjustmentApplied,
CONCAT('$', FORMAT(sum(ACT.Capital), 2)) TotalCashApplied,CONCAT('$', FORMAT(Sum(ACT.Price), 2)) TotalPriceApplied,CONCAT('$', FORMAT(sum(CNT.OwnedAmount-CNT.PaidAmount), 2)) OutstandingBalance,CONCAT('$', FORMAT(sum(CNT.OwnedAmount), 2)) RightstoRecieve,CONCAT('$', FORMAT(CNT.AdministrativeExpenses, 2)) AdminCharges */

select Distinct ifnull(iNNcfNumber,'') NFC,MRC.MerchantId TradeID,MRC.BusinessName FirmName,MRC.RNCNumber RNC,MRC.legalName TradeName,
ADR.Address1 Address,City,Phone1,email1 Email,CONCAT('$', FORMAT(-1*sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end), 2)) TotalCashRecieved,
CONCAT('$', FORMAT(sum(case when ACT.ActivityTypeId!=4 then 0 else case when ACT.AppliedAmount<0 then -1*ACT.AppliedAmount else ACT.AppliedAmount end end), 2))TotalAdjustmentApplied,
CONCAT('$', FORMAT(-1*(sum(case when ACT.ActivityTypeId=4 then 0 else ACT.AppliedAmount end)+sum(case when ACT.ActivityTypeId=4 then  ACT.AppliedAmount else 0 end )), 2))  TotalCashApplied,CONCAT('$', FORMAT(Sum(ACT.Price), 2)) TotalPriceApplied,CONCAT('$', FORMAT(CNT.OwnedAmount-(Select sum(-1*AppliedAmount) from tb_processoractivities where contractId=CNT.contractId and processeddate<=iNStatementTo  AND ActivityTypeId!=4), 2)) OutstandingBalance,CONCAT('$', FORMAT(CNT.OwnedAmount, 2)) RightstoRecieve,CONCAT('$', FORMAT(case when month(iNStatementTo)=month(CNT.fundingdate) AND month(iNStatementTo)=month(CNT.fundingdate) then CNT.AdministrativeExpenses else 0 end, 2)) AdminCharges
from tb_processoractivities ACT
inner join tb_processor PR on PR.ProcessorTypeId=ACT.ProcessorId AND ACT.MerchantId=PR.MerchantId
inner join lkp_tb_processorlist LKP on LKP.ProcessorId=PR.ProcessorTypeId
inner join tb_contracts CNT on CNT.contractId=ACT.contractID
inner join tb_merchants MRC on MRC.MerchantId=ACT.MerchantId
inner join tb_addresses ADR on ADR.AddressId=MRC.AddressId
Where ACT.MerchantId=iNmerchantId and 
ACT.ProcessedDate>=IF(iNStatementFrom='1900-01-01',ACT.ProcessedDate,iNStatementFrom) and 
ACT.ProcessedDate<=If(iNStatementTo='1900-01-01',ACT.ProcessedDate,iNStatementTo);
end if;

end;
end if;
end;