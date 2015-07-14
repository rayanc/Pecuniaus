drop procedure if exists avz_MRC_spAddMerchantContactInformation;

delimiter $$

create procedure avz_MRC_spAddMerchantContactInformation
(
iNmerchantId bigint,
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


start transaction;
Insert into tb_addresses(Address1,Phone1,Phone2,Email1,City,InsertUserId,InsertDate)
Select iNAddress,iNPhone,iNCellPhone,iNEmail,iNCity,iNUserId,now();

Set AId=LAST_INSERT_ID();

insert into tb_contacts (FirstName,LastName,passportnbr,JobTitle,DateofBirth,merchantId,AddressId1,ContactTypeId)
select iNFirstName,iNLastName,iNOwnerIdentification,iNPosition,iNDateofBirth,iNmerchantId,AID,1;
commit;
end;