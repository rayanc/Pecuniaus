drop procedure if exists avz_mrc_spRetrieveOwnerbyMerchant;
DELIMITER $$
CREATE PROCEDURE `avz_mrc_spRetrieveOwnerbyMerchant`(
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

where tb_owners.MerchantId=iNmerchantId;
end$$
DELIMITER ;