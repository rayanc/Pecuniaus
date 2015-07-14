drop procedure if exists avz_gen_spRetrieveIndustryTypes;
delimiter $$
create procedure avz_gen_spRetrieveIndustryTypes()
begin
select IndustryTypeID as keyId, name as description from lkp_tb_industrytypes;
end;
