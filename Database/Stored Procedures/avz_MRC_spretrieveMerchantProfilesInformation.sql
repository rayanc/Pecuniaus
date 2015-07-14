
drop procedure if exists avz_MRC_spretrieveMerchantProfilesInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantProfilesInformation(iNmerchantId bigint,iNActivityID bigint,iNProcessorId int,iNProcessorNumber varchar(200))
begin
/*if(iNActivityID=0) then*/
Select 
GrossAnnualSales GrossYearlySale,sum(TotalAmount) MonthlyCCSale,avg(TotalAmount) AverageMonthlyCCValue,avg(TotalTickets) AverageMonthlyTicket
from tb_creditcardactivity ACT
inner join tb_processor PR on PR.ProcessorId=ACT.ProcessorId
inner join lkp_tb_processorlist LKPPR on LKPPR.ProcessorId=PR.ProcessorTypeId
inner join tb_merchants MRC on MRC.MerchantId=ACT.MerchantId
where ACT.MerchantId=iNmerchantId and PR.ProcessorTypeId=IF(iNProcessorId=0,PR.ProcessorTypeId,iNProcessorId) 
and PR.processorNumber=If(iNProcessorNumber='All',PR.processorNumber,iNProcessorNumber)
and ACT.ContractId=avz_gen_fnRetrieveActiveContract(iNmerchantId);
/*elseif(iNActivityID>0) then
Select 
CreditCardActivityId as ActivityID,Month,Year,LKPPR.ProcessorId,Name as ProcessorName,TotalAmount as Amount,TotalTickets as Tickets
from tb_creditcardactivity ACT
inner join tb_processor PR on PR.ProcessorId=ACT.ProcessorId
inner join lkp_tb_processorlist LKPPR on LKPPR.ProcessorId=PR.ProcessorTypeId
where ACT.CreditCardActivityId=iNActivityID;
end if;*/
end;