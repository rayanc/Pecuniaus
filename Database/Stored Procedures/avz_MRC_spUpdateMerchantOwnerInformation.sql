drop procedure if exists avz_MRC_spUpdateMerchantOwnerInformation;

delimiter $$

create procedure avz_MRC_spUpdateMerchantOwnerInformation
(
iNownerId bigint,
iNAddress varchar(200),
iNCity varchar(200),
iNPhone varchar(20),
iNCellPhone varchar(20),
iNEmail varchar(200),
iNProvinceID int,
iNFirstName varchar(200),
iNLastName varchar(200),
iNOwnerIdentification varchar(200),
iNDateofBirth date,
iNDateBecameOwner date,
iNIsAuthorized bit,
iNUserId bigint
)
begin
declare AID, CID int;

select AddressId1 INTO AID from tb_contacts CNT Inner Join tb_owners OWN on OWN.ContactId=CNT.ContactId where OWN.OwnerId=iNownerId;
select CNT.ContactID INTO CID from tb_contacts CNT Inner Join tb_owners OWN on OWN.ContactId=CNT.ContactId where OWN.OwnerId=iNownerId;

start transaction;
Update tb_contacts set FirstName=iNFirstName,LastName=iNLastName,passportnbr=iNOwnerIdentification,DateofBirth=iNDateofBirth where ContactId=CID;

Update tb_addresses set Address1=iNAddress,Phone1=iNPhone,Phone2=iNCellPhone,email1=iNemail,City=iNCity,
State=iNProvinceID,ModifyUserId=iNUserId,ModifyDate=now()
where AddressID=AID; 

update tb_owners set dateBecameOwner=iNDateBecameOwner,isauthorized=iNIsAuthorized,ModifyUser=iNUserId,ModifyDate=now() where ownerid=iNownerId;
commit;
end;