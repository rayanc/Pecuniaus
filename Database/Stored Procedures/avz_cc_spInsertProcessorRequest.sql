drop procedure if exists avz_cc_spInsertProcessorRequest;

delimiter $$
create procedure avz_cc_spInsertProcessorRequest(iNmerchantId bigint,iNprocessorId bigint)
begin

if exists (select 1 from tb_ccvolumesqueue where merchantId=iNmerchantId and isprocessed is null)=0 then
insert into tb_ccvolumesqueue(merchantId,processorid,processornumber) 
select merchantid,ProcessorId,processorNumber from tb_processor where merchantId=iNmerchantId ;
end if;
end;




