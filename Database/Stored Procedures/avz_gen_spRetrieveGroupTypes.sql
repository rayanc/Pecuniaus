use pecuniaus;
drop procedure if exists avz_gen_spRetrieveGroupTypes;
delimiter $$
create procedure avz_gen_spRetrieveGroupTypes()
begin
select GroupTypeID as keyId, name as description from lkp_tb_grouptypes
where isactive=1
;
end;
