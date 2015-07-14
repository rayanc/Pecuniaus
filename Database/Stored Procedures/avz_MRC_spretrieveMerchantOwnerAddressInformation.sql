
drop procedure if exists avz_MRC_spretrieveMerchantOwnerAddressInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantOwnerAddressInformation(iNOwnerID bigint)
begin
Select 
ADR.Address1 as Address,IF(ADR.Phone1 Is Null,0,ADR.Phone1) as Phone1,IF(ADR.Phone2 Is Null,0,ADR.Phone2) as Cell,
ADR.email1 as Email,City,PRV.StateID as ProvinceID
from tb_addresses ADR
inner join tb_contacts CNT on CNT.AddressId1=ADR.AddressId
Inner Join lkp_tb_province PRV on PRV.StateID=ADR.State
inner join tb_owners OWN on OWN.ContactId=CNT.ContactId
where OWN.ownerid=iNOwnerID;
end;