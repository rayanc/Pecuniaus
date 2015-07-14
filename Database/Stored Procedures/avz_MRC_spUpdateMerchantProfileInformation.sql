drop procedure if exists avz_MRC_spUpdateMerchantProfileInformation;

delimiter $$

create procedure avz_MRC_spUpdateMerchantProfileInformation
(
iNactivityId bigint,
iNmonth int,
iNyear int,
iNprocessorTypeId int,
iNamount double,
iNtickets int
)
begin

start transaction;
Update tb_creditcardactivity set Month=iNmonth,year=iNyear,TotalAmount=iNamount,TotalTickets=iNtickets where CreditCardActivityId=iNactivityId;

Update tb_processor as PR inner Join tb_creditcardactivity as ACT ON PR.ProcessorId=ACT.ProcessorId 
set ProcessorTypeId=iNprocessorTypeId
Where ACT.CreditCardActivityId=iNactivityId;
commit;
end;