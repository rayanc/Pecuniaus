drop procedure if exists avz_cnt_spGenrateIOU;

delimiter $$
Create Procedure avz_cnt_spGenrateIOU
(
iNmerchantId bigint,
iNcontractId bigint
)
begin

select cnt.OwnedAmount as TotalOwnedAmount, cnt.fundingDate, mrc.LegalName, mrc.RNCNumber, now() as LetterDate, adr.Address1 as CompanyAddress,
prov.State as Province, prov.stateId as stateid, mrc.BusinessTypeId, mrc.LegalName as LegalCompanyName
from tb_merchants mrc
left join tb_contracts cnt on cnt.MerchantId=mrc.MerchantId
left join tb_addresses adr on adr.AddressId=mrc.AddressId
left join lkp_tb_province prov on adr.State=prov.StateID
where mrc.MerchantId = iNmerchantId;

select CONCAT_WS(' ', co.FirstName, co.MiddleName, co.LastName) AS ownerName,
co.PassportNbr as ID, prov.State as Province, ow.IsAuthorized as IsAuthorised , owad.Address1 as Address
from tb_owners ow 
	left join tb_contacts co on ow.ContactId = co.ContactId
	left join tb_addresses owad on co.AddressId1 = owad.AddressId
	left join lkp_tb_province prov on owad.State=prov.StateID
where ow.MerchantId = iNmerchantId;

end;