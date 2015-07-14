drop procedure if exists avz_MRC_spDeleteMerchantContactsInformation;

delimiter $$

create procedure avz_MRC_spDeleteMerchantContactsInformation
(
iNcontactId bigint
)
begin
start transaction;
insert into tb_contactshistory(AddressId1,Authorized,CellPhone,ContactId,ContactTypeId,DateOfbirth,FirstName,HomePhone,JobTitle,LastName,
MerchantId,MiddleName,PassportNbr,sfid,SSN)
select AddressId1,Authorized,CellPhone,ContactId,ContactTypeId,DateOfbirth,FirstName,HomePhone,JobTitle,LastName,
MerchantId,MiddleName,PassportNbr,sfid,SSN from tb_contacts where ContactID=iNcontactId;

insert into tb_addresseshistory(AddressId,AddressTypeId,Address1,Address2,Country,City,State,ZipCode,Phone1,Phone2,Phone3,email1,email2,email3,InsertUserId,
InsertDate,ModifyUserId,ModifyDate,FaxNum)
select AddressId,AddressTypeId,Address1,Address2,Country,City,State,ZipCode,Phone1,Phone2,Phone3,email1,email2,email3,InsertUserId,
InsertDate,ModifyUserId,ModifyDate,FaxNum 
from tb_addresses ADR inner join tb_contacts CNT on CNT.AddressId1=ADR.AddressId where CNT.ContactID=iNcontactId;

delete ADR from tb_addresses ADR inner join tb_contacts CNT on CNT.AddressId1=ADR.AddressId where CNT.ContactID=iNcontactId;
delete from tb_contacts where ContactID=iNcontactId;
commit;
end;