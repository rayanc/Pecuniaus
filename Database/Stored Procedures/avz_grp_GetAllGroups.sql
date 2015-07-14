use pecuniaus;
drop procedure if exists avz_grp_GetAllGroups;
DELIMITER $$
create PROCEDURE avz_grp_GetAllGroups(
iNsearch nvarchar(100)
)
BEGIN
if(iNsearch!='') then
begin
   SELECT grp.GroupID,grp.GroupName,grp.IsActive,grpTypes.GroupTypeID,
   grpTypes.Name as GroupTypeName
   from    
    tb_groups as grp 
    left join lkp_tb_grouptypes as grpTypes on grp.GroupTypeId=grpTypes.GroupTypeId
    WHERE   grp.GroupName like  CONCAT( iNsearch, '%') 
  ORDER BY grp.ModifyDate desc;
  end;
  else
  begin
    SELECT grp.GroupID,grp.GroupName,grp.IsActive,grpTypes.GroupTypeID,
    grpTypes.Name as GroupTypeName
   from    
    tb_groups as grp 
    left join lkp_tb_grouptypes as grpTypes on grp.GroupTypeId=grpTypes.GroupTypeId    
  ORDER BY grp.ModifyDate desc;
  end;
  end if;
  end 
       
      
      
