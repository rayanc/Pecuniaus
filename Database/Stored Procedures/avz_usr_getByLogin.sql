drop procedure if exists avz_usr_getByLogin;

delimiter $$
 
create procedure avz_usr_getByLogin
(
	iNUserID varchar(100)
)
begin
	select ID, UserID, UserName, Password, SecurityStamp from tb_users where UserID=iNUserID Limit 1;
end$$

delimiter ;
