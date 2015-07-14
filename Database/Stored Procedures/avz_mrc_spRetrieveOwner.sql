drop procedure if exists avz_mrc_spRetrieveOwner;
DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spRetrieveOwner`(
iNmerchantId bigint

)
begin

select 
cnt.FirstName +','+ cnt.LastName as ownerName,
cnt.SSN as ssn,
adr.Address1 as addressLine1,
adr.Address2  as addressLine2,
adr.Phone1 as phone1

from tb_owners own
join tb_contacts cnt 
on own.ContactId= cnt.ContactId
join tb_addresses adr 
on adr.AddressId=cnt.AddressId1

where own.merchantId=iNmerchantId;
end$$
DELIMITER ;