drop procedure if exists avz_MRC_spDeleteMerchantOwnersInformation;

delimiter $$

create procedure avz_MRC_spDeleteMerchantOwnersInformation
(
iNOwnerID bigint
)
begin
delete ADR from tb_addresses ADR
inner join tb_contacts CNT on CNT.AddressId1=ADR.AddressId
inner join tb_owners OWN on OWN.ContactId=CNT.ContactId
where OWN.ownerid=iNOwnerID;

delete from tb_owners where ownerid=iNOwnerID;

end;