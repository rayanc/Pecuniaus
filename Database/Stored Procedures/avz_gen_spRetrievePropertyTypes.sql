drop procedure if exists avz_gen_spRetrievePropertyTypes;

delimiter $$
Create procedure avz_gen_spRetrievePropertyTypes()
begin
select typ.TypeId as keyId, typ.Description as description
from lkp_tb_types typ where typ.Usage='PTY';

end;
