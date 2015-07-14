drop procedure if exists avz_MRC_spAddMerchantOwnerInformation;

delimiter $$

create procedure avz_MRC_spAddMerchantOwnerInformation
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
iNDateBecameOwner date,
iNIsAuthorized bit,
iNUserId bigint
)
begin
declare AID,CID int;


/*start transaction;*/
Insert into tb_addresses(Address1,Phone1,Phone2,Email1,City)
Select iNAddress,iNPhone,iNCellPhone,iNEmail,iNCity;

Set AId=LAST_INSERT_ID();

insert into tb_contacts (FirstName,LastName,passportnbr,DateofBirth,merchantId,AddressId1,ContactTypeId)
select iNFirstName,iNLastName,iNOwnerIdentification,iNDateofBirth,iNmerchantId,AID,1;

Set CId=LAST_INSERT_ID();

Insert into tb_owners(dateBecameOwner,IsAuthorized,ContactId,MerchantId,insertUserId,InsertDate)
Select iNDateBecameOwner,iNIsAuthorized,CID,iNmerchantId,InUserId,now();

/*commit;*/
end;