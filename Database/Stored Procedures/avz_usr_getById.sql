drop procedure if exists avz_usr_getById;

delimiter $$
 
create procedure avz_usr_getById
(
	iNId bigint
)
begin
	select ID, UserID, UserName, Password, SecurityStamp from tb_users where ID=iNId Limit 1;
end$$
delimiter ;
