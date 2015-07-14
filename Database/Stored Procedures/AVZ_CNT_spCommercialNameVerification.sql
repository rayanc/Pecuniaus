-- ================================================
-- Name: AVZ_CNT_spCommercialNameVerification
-- 
-- Description : To Retrieve details for Commercial Name Verification
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 13-09-2014
-- ================================================
drop procedure if exists AVZ_CNT_spCommercialNameVerification;
DELIMITER //
CREATE PROCEDURE AVZ_CNT_spCommercialNameVerification 
(iNMerchantId bigint,
iNContractId bigint)
BEGIN

Select m.businessName, ad.Phone1 as telephone, ad.Address1 as Address, ad.City, ad.State as StateId, td.fileName, td.fileDetails, ad.AddressId as addressId
from tb_merchants m
left join tb_addresses ad on m.AddressId=ad.AddressId
left join tb_documents td on m.MerchantId=td.MerchantId and td.ContractId=iNContractId and td.DocumentTypeId =3
where m.MerchantId=iNMerchantId ;

END;
//
DELIMITER ;

