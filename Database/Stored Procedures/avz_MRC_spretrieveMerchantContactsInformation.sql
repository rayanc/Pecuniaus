
drop procedure if exists avz_MRC_spretrieveMerchantContactsInformation;

delimiter $$

create procedure avz_MRC_spretrieveMerchantContactsInformation(iNmerchantId bigint,iNContactID bigint)
begin
if(iNContactID=0) then
Select 
ContactId,FirstName,LastName,JobTitle as Position,DateOfbirth as DateofBirth,IF(TT.Description='OWNER','Yes','No') as IsOwner,IF(PassportNbr is null,'',PassportNbr) as OwnerIdentification
from tb_contacts CNT
inner join lkp_tb_types TT on TT.TypeId=CNT.ContactTypeId
where CNT.MerchantId=iNmerchantId and TT.Usage='CON';
elseif(iNContactID>0) then
Select 
ContactId,FirstName,LastName,JobTitle as Position,DateOfbirth as DateofBirth,IF(TT.Description='OWNER','Yes','No') as IsOwner,IF(PassportNbr is null,'',PassportNbr) as OwnerIdentification
from tb_contacts CNT
inner join lkp_tb_types TT on TT.TypeId=CNT.ContactTypeId
where CNT.ContactID=iNContactID and TT.Usage='CON';
end if;
end;