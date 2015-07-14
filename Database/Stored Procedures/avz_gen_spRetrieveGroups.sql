use Pecuniaus;
drop procedure if exists avz_gen_spRetrieveGroups;
delimiter $$
create procedure avz_gen_spRetrieveGroups()
begin 
select GroupID as keyId, GroupName as description from tb_groups
where isactive=1
;
end;
