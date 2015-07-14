#call avz_MRC_spretrieveMerchantActivitiesInformation(238,10002,'06','2015');
drop procedure if exists avz_MRC_spretrieveMerchantActivitiesInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantActivitiesInformation(
iNmerchantId bigint,
iNProcessorTypeId int,
iNmonth Varchar(30),
iNyear varchar(10)
)
begin
select * from (
Select distinct
ACT.ProcessorId,ACT.MerchantId MerchantId,ProcessedDate,concat(concat(month(ProcessedDate),"/"),year(ProcessedDate)) MonthName,LKPPR.name ProcessorName,
OFF.Retention RetentionPercentage,AppliedAmount Total,
OFF.Proportion Price,Capital,case when ACT.ActivityTypeId in (20,70) then AppliedAmount Else 0 end ProcessorIncome,
case when ACT.ActivityTypeId not in (20,70) then AppliedAmount Else 0 end  OtherIncome,
FL.activityname TypeofActivity,ContractNumber Contract,ACT.Notes Note
from tb_processor PR
 inner join lkp_tb_processorlist LKPPR on LKPPR.ProcessorId=PR.ProcessorTypeId
right join tb_processoractivities ACT on ACT.ProcessorId=LKPPR.ProcessorId
inner join tb_contracts CNT on CNT.ContractId=ACT.ContractId
left join tb_offers OFF on OFF.ContractId=CNT.ContractId and OFF.status=190001
inner join lkp_tb_financeactivitylist FL on FL.activityid=ACT.ActivityTypeId
where ACT.MerchantId=iNmerchantId and ACT.ContractId=avz_gen_fnRetrieveActiveContract(iNmerchantId))xx
where (iNProcessorTypeId=0 or ProcessorId=iNProcessorTypeId) and month(ProcessedDate)=iNmonth and year(ProcessedDate)=iNyear;

end;


