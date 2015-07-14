use Pecuniaus;
drop procedure if exists avz_gen_spRetrieveUserTypes;

delimiter $$
Create procedure avz_gen_spRetrieveUserTypes()
begin
select usr.ID as keyId, usr.UserName as description
from tb_users usr Order By usr.UserName asc;
end;
