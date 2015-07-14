drop procedure if exists avz_gen_spRetrieveModuleTypes;

delimiter $$
Create procedure avz_gen_spRetrieveModuleTypes()
begin
select typ.moduleid as keyId, typ.modulename as description
from lkp_tb_modules  typ
where typ.parentmodule is  null
 Order By typ.modulename asc;

end;