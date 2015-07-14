-- ================================================
-- Name: AVZ_COL_spRetrieveOneAuthorizedOwners
-- 
-- Description : To Retrieve one authhorized owner deatil for particular Contract
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrieveOneAuthorizedOwners;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveOneAuthorizedOwners
(iNMerchantId bigint,
iNContractId bigint
)
BEGIN

select 
concat(cnt.FirstName,' ',cnt.LastName) as ownerName,
adr.Address1 as address1,
adr.Address2 as address2,
adr.Phone1 as telephone,
cnt.cellPhone
from tb_contracts ct
left join tb_owners own on ct.MerchantId=own.MerchantId
left join tb_contacts cnt on own.ContactId= cnt.ContactId
left join tb_addresses adr on cnt.AddressId1 = adr.AddressId
where ct.merchantId=iNmerchantId and ct.contractId=iNContractId and own.IsAuthorized=1 limit 1;

END$$
DELIMITER ;