-- ================================================
-- Name: AVZ_COL_spGenerateCollectionLetter
-- 
-- Description : To generate letter for collections
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spGenerateCollectionLetter;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spGenerateCollectionLetter
(iNMerchantId bigint,
iNContractId bigint
)
BEGIN

declare pOwnerName longtext;
declare pOwnerEmail longtext;
declare pCcEmail varchar(200);

select value into pCcEmail from tb_configuration where config='CollectionLetter' and configsystem='COL';
set pCcEmail = CONCAT(',',pCcEmail);

-- To send email to single owner
select CONCAT(ad.email1,pCcEmail) into pOwnerEmail
from tb_owners ow 
join tb_contacts co on ow.ContactId = co.ContactId	
join tb_addresses ad on co.AddressId1=ad.AddressId 	
where ow.MerchantId = iNMerchantId and ow.IsAuthorized=1 limit 1;

-- To send email to multiple owners
-- select replace(group_concat(ad.email1),',',';') into pOwnerEmail
-- from tb_owners ow 
-- join tb_contacts co on ow.ContactId = co.ContactId	
-- join tb_addresses ad on co.AddressId1=ad.AddressId 	
-- where ow.MerchantId = iNMerchantId and ow.IsAuthorized=1;

select group_concat(CONCAT_WS(' ', co.FirstName, co.MiddleName, co.LastName), ' ') into pOwnerName
from tb_owners ow 
join tb_contacts co on ow.ContactId = co.ContactId	
where ow.MerchantId = iNMerchantId and ow.IsAuthorized=1;

select now() as currentDate, m.legalName,pOwnerName as ownerName, ad.Address1 as address, p.State as province,
ct.contractNumber, m.businessName as merchantName, 
pst.ProcessedDate as dateLastActivity,
(ct.ownedAmount - IFNULL(ct.Paidamount, 0)) as pendingAmount,
pOwnerEmail as toEmail
from tb_contracts ct
left join tb_merchants m on ct.MerchantId=m.MerchantId
left join tb_addresses ad on m.AddressId = ad.AddressId
left join lkp_tb_province p on ad.State=p.StateID and ad.AddressTypeId=1
left join tb_processoractivities pst on ct.ContractId = pst.ContractId
where ct.MerchantId=iNMerchantId and ct.ContractId=iNContractId  limit 1;

END$$
DELIMITER ;