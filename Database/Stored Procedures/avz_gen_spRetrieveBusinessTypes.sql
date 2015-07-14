drop procedure if exists avz_gen_spRetrieveBusinessTypes;
delimiter $$
create procedure avz_gen_spRetrieveBusinessTypes()
begin
select EntitytypeID as keyId, name as description from lkp_tb_entitytypes;
end;
