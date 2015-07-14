#call avz_MRC_spretrieveMerchantContractActivitiesInformation(159,0,'All','1900-01-01','1900-01-01');


drop procedure if exists avz_MRC_spretrieveMerchantContractActivitiesInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantContractActivitiesInformation(
iNcontractId bigint,
iNProcessorTypeId int,
iNProcessorNumber varchar(200),
iNActivityStartDate datetime,
iNActivityEndDate datetime
)
begin
                                                                                                                                                                           
Select distinct
ACT.ProcessorId,ACT.ContractId,ProcessedDate,concat(concat(month(ProcessedDate),"/"),year(ProcessedDate)) MonthName,Name ProcessorName,CNT.Retention RetentionPercentage,ACT.AppliedAmount Total,
ACT.Price Price,ACT.Capital Capital, case when ACT.ActivityTypeId =70 then ACT.AppliedAmount else 0 end ProcessorIncome, 
case when ACT.ActivityTypeId <>70 then ACT.AppliedAmount else 0 end OtherIncome,FL.activityname TypeofActivity,ContractNumber Contract,ACT.Notes Note
from tb_processor PR
inner join lkp_tb_processorlist LKPPR on LKPPR.ProcessorId=PR.ProcessorTypeId
inner join tb_processoractivities ACT on ACT.ProcessorId=PR.ProcessorTypeId
inner join tb_contracts CNT on CNT.ContractId=ACT.ContractId
inner join lkp_tb_financeactivitylist FL on FL.activityid=ACT.ActivityTypeId
where ACT.ContractId=iNcontractId and 
PR.ProcessorTypeId=IF(iNProcessorTypeId=0,PR.ProcessorTypeId,iNProcessorTypeId) and 
PR.processorNumber=If(iNProcessorNumber='All',PR.processorNumber,iNProcessorNumber) and
ACT.ProcessedDate>=IF(iNActivityStartDate='1900-01-01',ACT.ProcessedDate,iNActivityStartDate) and 
ACT.ProcessedDate<=If(iNActivityEndDate='1900-01-01',ACT.ProcessedDate,iNActivityEndDate)
order by ACT.activityid desc;
end;