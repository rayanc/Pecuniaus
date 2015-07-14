drop procedure if exists avz_gen_spRetrieveAdvances;
delimiter $$
create procedure avz_gen_spRetrieveAdvances()
begin
select 
ty.TypeId keyId,ty.Description description
from lkp_tb_types ty where ty.Usage='ADV' and ty.IsActive=1;

end;