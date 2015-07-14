use pecuniaus;
drop procedure if exists avz_usr_CheckusernameExist;
DELIMITER $$
create PROCEDURE avz_usr_CheckusernameExist(
iNuserName nvarchar(100),
iNID int
)
BEGIN
if exists(SELECT users.UserName FROM tb_users as users WHERE users.UserName = iNuserName) then
begin
   if(iNID>0) then
	begin
      if exists(SELECT users.UserName FROM tb_users as users WHERE users.UserName = iNuserName and users.id<>iNID) then
		select 1 as ID;
	  else
      select 0 as ID;
	 end if;
    end;
else
select 1 as ID;
end if;
end;
else
select 0 as ID;
end if;
end;
   
 
       
      
      
