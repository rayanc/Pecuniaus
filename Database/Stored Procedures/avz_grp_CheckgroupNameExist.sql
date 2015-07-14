use pecuniaus;
drop procedure if exists avz_grp_CheckgroupNameExist;
DELIMITER $$
create PROCEDURE avz_grp_CheckgroupNameExist(
iNgroupName nvarchar(100),
iNGroupID int
)
BEGIN
if exists(SELECT groups.GroupName FROM tb_groups as groups WHERE groups.GroupName = iNgroupName) then
begin
   if(iNGroupID>0) then
	begin
      if exists(SELECT groups.GroupName FROM tb_groups as groups 
      WHERE groups.GroupName = iNgroupName and groups.GroupId<>iNGroupID) then
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
   
 
       
      
      

