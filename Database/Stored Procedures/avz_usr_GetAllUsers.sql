use Pecuniaus;
drop procedure if exists avz_usr_GetAllUsers;
DELIMITER $$
create PROCEDURE avz_usr_GetAllUsers(
iNsearch nvarchar(100)
)
BEGIN
if(iNsearch!='') then
begin
   SELECT
    users.ID,users.UserName,contacts.FirstName,contacts.LastName,contacts.SSN,
    users.password as Password,users.ContactID,address.AddressID, users.email,
  contacts.DateOfbirth as DateofBirth,grp.GroupName,ifNULL(grp.GroupID,0)as GroupID ,
  address.State as StateID,users.IsActive,users.IsSales,users.IsCollector,address.ZipCode as PostalCode,
  address.Address1 as AddressLine1,address.Address2 as AddressLine2
  FROM tb_users as users
   left join tb_usergroups as usrGroups on usrGroups.UserID=users.ID
 left   join tb_groups as grp on grp.GroupID=usrGroups.GroupID
   left join tb_contacts as contacts on contacts.ContactId=users.ContactId
   left join tb_addresses as  address on address.AddressId=contacts.AddressId1
	
    WHERE   users.UserName like  CONCAT( iNsearch, '%')
       
  ORDER BY users.ModifyDate desc;
  end;
  else
  begin
      SELECT
    users.ID,users.UserName,contacts.FirstName,contacts.LastName,contacts.SSN,
    users.password as Password,users.ContactID,address.AddressID,users.email,
  contacts.DateOfbirth as DateofBirth,grp.GroupName,ifNULL(grp.GroupID,0)as GroupID ,
  address.State as StateID,users.IsActive,users.IsSales,users.IsCollector,address.ZipCode as PostalCode,
  address.Address1 as AddressLine1,address.Address2 as AddressLine2
  FROM tb_users as users
   left join tb_usergroups as usrGroups on usrGroups.UserID=users.ID
 left   join tb_groups as grp on grp.GroupID=usrGroups.GroupID
   left join tb_contacts as contacts on contacts.ContactId=users.ContactId
 left   join tb_addresses as  address on address.AddressId=contacts.AddressId1
  ORDER BY users.ModifyDate desc;
  end;
  end if;
  end 
       
      
      
