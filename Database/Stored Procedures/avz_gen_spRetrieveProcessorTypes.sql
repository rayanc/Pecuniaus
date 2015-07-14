drop procedure if exists avz_gen_spRetrieveProcessorTypes;
delimiter $$
create procedure avz_gen_spRetrieveProcessorTypes()

begin
select `ProcessorId` as keyId, Name as description from lkp_tb_processorlist;
end;

