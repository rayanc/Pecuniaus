drop procedure if exists avz_MRC_spretrieveMerchantActivitiesGroupedInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantActivitiesGroupedInformation(
iNmerchantId bigint,
iNProcessorTypeId int,
iNProcessorNumber varchar(200),
iNActivityStartDate datetime,
iNActivityEndDate datetime
)
begin
select ProcessorId,MonthName,ProcessorName,max(RetentionPercentage) RetentionPercentage,sum(Total) Total,sum(Price) Price,
sum(Capital) Capital,sum(ProcessorIncome) ProcessorIncome,sum(ProcessorIncome) ProcessorIncome,TypeofActivity,Contract from (
Select distinct
/*,ACT.MerchantId MerchantId,*/
ACT.ProcessorId,concat(concat(month(ProcessedDate),"/"),year(ProcessedDate)) MonthName,Name ProcessorName,CNT.Retention RetentionPercentage,
AppliedAmount Total,
Price,Capital,case when ACT.ActivityTypeId in (20,70) then AppliedAmount Else 0 end ProcessorIncome,
case when ACT.ActivityTypeId not in (20,70) then AppliedAmount Else 0 end  OtherIncome,FL.activityname TypeofActivity,ContractNumber Contract,ACT.Notes Note
from tb_processor PR
inner join lkp_tb_processorlist LKPPR on LKPPR.ProcessorId=PR.ProcessorTypeId
right join tb_processoractivities ACT on ACT.ProcessorId =LKPPR.ProcessorId
inner join tb_contracts CNT on CNT.ContractId=ACT.ContractId
inner join lkp_tb_financeactivitylist FL on FL.activityid=ACT.ActivityTypeId
where ACT.MerchantId=iNmerchantId and ACT.ContractId=avz_gen_fnRetrieveActiveContract(iNmerchantId) and
(iNProcessorTypeId=0 or PR.ProcessorTypeId=iNProcessorTypeId) and 
((iNProcessorNumber='All' or iNProcessorNumber=0 ) or PR.processorNumber=iNProcessorNumber) and 
(ACT.ProcessedDate between iNActivityStartDate and iNActivityEndDate)) XX
Group By ProcessorId,MonthName,ProcessorName;
end;