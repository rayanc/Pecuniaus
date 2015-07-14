drop procedure if exists avz_MRC_spretrieveMerchantStatementsInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantStatementsInformation(
iNmerchantId bigint,
iNStatementFrom datetime,
iNStatementTo datetime
)
begin
declare iNNcfNumber varchar(20);

#select month(iNStatementFrom),month(iNStatementTo),day(iNStatementFrom),day(iNStatementTo),day(last_day(iNStatementTo));

if(month(iNStatementFrom)=month(iNStatementTo) and day(iNStatementFrom)=1 and day(iNStatementTo)=day(last_day(iNStatementTo)))
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

if(iNmerchantId=0) then
select ACT.MerchantId,ProcessedDate TransactionDate,LKP.Name ProcessorName,CONCAT('$', FORMAT((ACT.AppliedAmount / (Retention/100)), 2)) TotalTransaction,CONCAT('$', FORMAT(ACT.AppliedAmount, 2)) PaymentsReceived,Retention RetentionPercentage
/*
select Distinct iNNcfNumber NFC,ACT.MerchantId,ProcessedDate TransactionDate,LKP.Name ProcessorName,CONCAT('$', FORMAT(TotalAmount, 2)) TotalTransaction,CONCAT('$', FORMAT(ACT.PaidAmount, 2)) PaymentsReceived,Retention RetentionPercentage*/
from tb_processoractivities ACT
inner join tb_processor PR on PR.ProcessorTypeId=ACT.ProcessorId
inner join lkp_tb_processorlist LKP on LKP.ProcessorId=PR.ProcessorTypeId
inner join tb_contracts CNT on CNT.contractId=ACT.contractID
Where ACT.ProcessedDate>=IF(iNStatementFrom='1900-01-01',ACT.ProcessedDate,iNStatementFrom) and 
ACT.ProcessedDate<=If(iNStatementTo='1900-01-01',ACT.ProcessedDate,iNStatementTo);
else
/*
select ACT.MerchantId,ProcessedDate TransactionDate,LKP.Name ProcessorName,(ACT.AppliedAmount/(Retention/100)) TotalTransaction,ACT.AppliedAmount PaymentsReceived,Retention RetentionPercentage*/

select ACT.MerchantId,ProcessedDate TransactionDate,LKP.Name ProcessorName,CONCAT('$', FORMAT((ACT.AppliedAmount / (Retention/100)), 2)) TotalTransaction,CONCAT('$', FORMAT(ACT.AppliedAmount, 2)) PaymentsReceived,Retention RetentionPercentage
from tb_processoractivities ACT
inner join tb_processor PR on PR.ProcessorTypeId=ACT.ProcessorId
inner join lkp_tb_processorlist LKP on LKP.ProcessorId=PR.ProcessorTypeId
inner join tb_contracts CNT on CNT.contractId=ACT.contractID
Where ACT.MerchantId=iNmerchantId and 
ACT.ProcessedDate>=IF(iNStatementFrom='1900-01-01',ACT.ProcessedDate,iNStatementFrom) and 
ACT.ProcessedDate<=If(iNStatementTo='1900-01-01',ACT.ProcessedDate,iNStatementTo);
end if;
end;