drop procedure if exists avz_usr_getPermissions;

delimiter $$
 
create procedure avz_usr_getPermissions
(
	iNUserId bigint
)
begin
	select UserID, ModuleID, PageID, Edit, r.read, r.write, r.edit 
	from tb_usergroups g 
	inner join tb_grouprights r on g.GroupID=r.GroupID 
	where g.isActive= 1 and PageID>0 and UserId=iNUserId ;
end$$
delimiter ;


call avz_usr_getPermissions(2);