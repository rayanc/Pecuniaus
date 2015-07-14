
drop procedure if exists avz_MRC_spretrieveMerchantProfileDetailsInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantProfileDetailsInformation(iNmerchantId bigint,iNActivityID bigint,iNProcessorId int,iNProcessorNumber varchar(200))
begin
if(iNActivityID=0) then
Select 
CreditCardActivityId as ActivityID,Month,Year,LKPPR.ProcessorId,Name as ProcessorName,sum(TotalAmount) as Amount,sum(TotalTickets) as Tickets,IsAutomated
from tb_creditcardactivity ACT
inner join tb_processor PR on PR.ProcessorId=ACT.ProcessorId
inner join lkp_tb_processorlist LKPPR on LKPPR.ProcessorId=PR.ProcessorTypeId
where ACT.MerchantId=iNmerchantId and PR.ProcessorTypeId=IF(iNProcessorId=0,PR.ProcessorTypeId,iNProcessorId) and 
PR.processorNumber=If(iNProcessorNumber='All',PR.processorNumber,iNProcessorNumber)
and ACT.ContractId=avz_gen_fnRetrieveActiveContract(iNmerchantId)
group by month,year,LKPPR.ProcessorId,Name;
elseif(iNActivityID>0) then
Select 
CreditCardActivityId as ActivityID,Month,Year,LKPPR.ProcessorId,Name as ProcessorName,TotalAmount as Amount,TotalTickets as Tickets,IsAutomated
from tb_creditcardactivity ACT
inner join tb_processor PR on PR.ProcessorId=ACT.ProcessorId
inner join lkp_tb_processorlist LKPPR on LKPPR.ProcessorId=PR.ProcessorTypeId
where ACT.CreditCardActivityId=iNActivityID;
end if;
end;