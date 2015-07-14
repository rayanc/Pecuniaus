
drop procedure if exists avz_MRC_spretrieveMerchantOwnersInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantOwnersInformation(iNmerchantId bigint,iNOwnerID bigint)
begin
if(iNOwnerID=0) then
Select 
ownerid as OwnerId,FirstName,LastName,DateOfbirth as DateofBirth,IsAuthorized,IF(PassportNbr is null,'',PassportNbr) as OwnerIdentification
from tb_owners OWN
inner join tb_contacts CNT on OWN.ContactId=CNT.ContactId
where OWN.MerchantId=iNmerchantId;
elseif(iNOwnerID>0) then
Select 
ownerid as OwnerId,FirstName,LastName,DateOfbirth as DateofBirth,DateBecameOwner,IsAuthorized,IF(PassportNbr is null,'',PassportNbr) as OwnerIdentification
from tb_owners OWN
inner join tb_contacts CNT on OWN.ContactId=CNT.ContactId
where OWN.ownerid=iNOwnerID;
end if;
end;