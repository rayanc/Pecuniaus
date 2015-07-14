Use Pecuniaus;
drop procedure if exists avz_USER_spAddUser;

delimiter $$
 
create procedure avz_USER_spAddUser
(
iNId bigint(20),
iNUserId varchar(100),
iNUserName varchar(200),
iNPassword varchar(200),
iNSecurityStamp varchar(45),
iNFirstName varchar(200),
iNLastName varchar(200),
iNDateofBirth date,
iNGroupID int,
iNisActive bool,
iNisSales bool,
iNisCollector bool,
iNSSN varchar(200),
iNAddressLine1 varchar(200),
iNAddressLine2 varchar(200),
iNStateID varchar(200),
iNPostalCode varchar(200),
iNuemail varchar(200),
iNContactID int,
iNAddressID int,
iNInsertUserID int,
iNModifyUserID int
)
begin
declare AID,CID,UID int;

/*start transaction;*/
if(iNId=0) then
begin
Insert into tb_addresses(AddressTypeId,Address1,Address2,Country,State,ZipCode,InsertUserID,InsertDate,ModifyUserID,ModifyDate)
Select 6,iNAddressLine1,iNAddressLine2,1,iNStateID,iNPostalCode,iNInsertUserID,Now(),iNModifyUserID,Now();

Set AId=LAST_INSERT_ID();

insert into tb_contacts( merchantId,FirstName,LastName,DateofBirth,AddressId1,SSN,JobTitle,ContactTypeId)
select 0,iNFirstName,iNLastName,iNDateofBirth,AID,iNSSN,'',1;

Set CId=LAST_INSERT_ID();

Insert into tb_users(UserID,UserName,Password,IsActive,SecurityStamp,email,isSales,isCollector,IsReset,islocked,IsLogggedIN,InsertDate,InsertUserID,ModifyUserID
,ModifyDate,ContactID)
Select iNUserId,iNUserName,iNPassword,iNisActive,iNSecurityStamp,iNuemail,iNisSales,iNisCollector,0,0,0,Now(), iNInsertUserID,iNModifyUserID
,Now(),CID;

Set UID=LAST_INSERT_ID();

if(iNisSales=1) then
insert into tb_salesrep(Userid,AddressId,ContactId, InsertDate) values(UID,AId,CId,Now());
end if;

if(iNisCollector=1) then
insert into tb_collectors(UserId,CompanyId,ContactId,InsertDate,InsertUserId) values(UID,1,CId,Now(),iNInsertUserID);
end if;

if(iNGroupID>0) then
 begin
insert into tb_usergroups
(UserID,GroupID,IsActive,InsertUserId,InsertDate,ModifyUserID,ModifyDate)
Select UID,iNGroupID,1,iNInsertUserID,Now(),iNModifyUserID,Now();
end;
end if;
/*commit;*/
end;
end if;
end;


