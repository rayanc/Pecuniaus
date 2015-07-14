drop procedure if exists avz_USER_spUpdateUser;

delimiter $$
 
create procedure avz_USER_spUpdateUser
(
iNuId bigint(20),
iNuUserId varchar(100),
iNuUserName varchar(200),
iNuFirstName varchar(200),
iNuLastName varchar(200),
iNuDateofBirth date,
iNuGroupID int,
iNuisActive bool,
iNuisSales bool,
iNuisCollector bool,
iNuSSN varchar(200),
iNuAddressLine1 varchar(200),
iNuAddressLine2 varchar(200),
iNuStateID varchar(200),
iNuPostalCode varchar(200),
iNuemail varchar(200),
iNuContactID int,
iNuAddressID int,
iNuInsertUserID int,
iNuModifyUserID int,
iNuPassword varchar(200)
)
begin
declare CID int;

/*start transaction;*/
start transaction;
if(iNuId>0) then
begin
select CID=CollectorID from tb_collectors Where UserId=iNuId;

if exists(select addressid from tb_addresses where addressId=iNuAddressID) then
begin
Update  tb_addresses
set AddressTypeId=6,Address1=iNuAddressLine1,Address2=iNuAddressLine2,Country=1,State=iNuStateID,ZipCode=iNuPostalCode,InsertUserID=iNuInsertUserID
,ModifyUserID=iNuModifyUserID,ModifyDate=Now()
where addressId=iNuAddressID;
end;
end if;

if exists(select ContactId from tb_contacts where ContactId=iNuContactID) then
begin
Update tb_contacts
set merchantId=0,FirstName=iNuFirstName,LastName=iNuLastName,DateOfbirth=iNuDateofBirth,AddressId1=iNuAddressID,SSN=iNuSSN
where ContactId=iNuContactID;
end;
end if;

Update tb_users
set IsActive=iNuisActive,isSales=iNuisSales,isCollector=iNuisCollector,email=iNuemail,ModifyUserID=iNuModifyUserID,ModifyDate=Now(), Password=iNuPassword
where ID=iNuId;

if(iNuisCollector=0) then
	if exists(select UserId from tb_collectors where UserId=iNuId) then
	begin
		Delete from tb_collections where AssignedUserId=iNuId;
		Update tb_assignedcollections Set CollectorID=null Where CollectorID=CID;
	end;
	end if;
else
	if not exists(select UserId from tb_collectors where UserId=iNuId) then
	begin
		insert into tb_collectors(UserId,CompanyId,ContactId,InsertDate,InsertUserId) values(iNuId,1,iNuContactID,Now(),iNuModifyUserID);
	end;
	end if;
end if;

if(iNuGroupID>0) then
 begin
	if exists(select 1 from tb_usergroups where UserID=iNuId) then
		 update tb_usergroups 
		 set GroupID=iNuGroupID,ModifyUserID=iNuModifyUserID,ModifyDate=Now()
		 where UserID=iNuId; 
		else
		insert into tb_usergroups(UserID,GroupID,insertUserId,InsertDate, IsActive)
		values(iNuId,iNuGroupID,iNuInsertUserID,now(),iNuisActive);
	end if;	
end;
end if;
end;
end if;
commit;
end;
