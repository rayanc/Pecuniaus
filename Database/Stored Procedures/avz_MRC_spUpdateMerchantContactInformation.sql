drop procedure if exists avz_MRC_spUpdateMerchantContactInformation;

delimiter $$

create procedure avz_MRC_spUpdateMerchantContactInformation
(
iNcontactId bigint,
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
iNPosition varchar(200),
iNUserId bigint
)
begin
declare AID int;

select AddressId1 INTO AID from tb_contacts where ContactId=iNcontactId;

start transaction;
Update tb_contacts set FirstName=iNFirstName,LastName=iNLastName,passportnbr=iNOwnerIdentification,JobTitle=iNPosition,
DateofBirth=iNDateofBirth where ContactId=iNcontactId;

Update tb_addresses set Address1=iNAddress,Phone1=iNPhone,Phone2=iNCellPhone,email1=iNemail,City=iNCity,
State=iNProvinceID,ModifyUserId=iNUserId,ModifyDate=now() where AddressID=AID;
commit;
end;