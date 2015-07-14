drop procedure if exists avz_gen_spRetrieveProvinceTypes;
delimiter $$
create procedure avz_gen_spRetrieveProvinceTypes()

begin
select stateid as keyId, State as description from lkp_tb_province;
end;