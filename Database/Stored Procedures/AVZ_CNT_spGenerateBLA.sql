drop procedure if exists AVZ_CNT_spGenerateBLA;
-- ================================================
-- Name: AVZ_CNT_spGenerateBLA
-- 
-- Description : To Generate BLA for a particular contract
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 02-10-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_CNT_spGenerateBLA 
(iNMerchantId bigint,
iNContractId bigint)
BEGIN

Select ct.contractNumber, CONCAT_WS(' ', src.FirstName, ' ', src.LastName) AS salesRepName, 
m.salesRepId, now() as date, m.legalName as companyName, m.businessName, m.BusinessTypeId as businessTypeId,
ad.country as orgCountry, ad.Address1 as address, p.State as province, c.CountryName as country, 
ad.Address1 as legalAddress, p.State as legalProvince, c.CountryName as legalCountry,
m.businessStartDate, m.RNCNumber, ltb.bankName, ltb.City as bankCity, ltb.Country as bankCountry,
ct.LoanedAmount as mcaAmount, 
ct.OwnedAmount as totalOwnedAount,
off.retention,
lte.expenseAmount -- Administrative Expenses    
from
    tb_contracts ct
	left join tb_merchants m on ct.MerchantId=m.MerchantId
	left join tb_salesrep sa on m.SalesRepId=sa.SalesRepId
	left join tb_contacts src on sa.Contactid = src.ContactId
	left join tb_addresses ad on m.AddressId = ad.AddressId and ad.AddressTypeId=1
	left join lkp_tb_country c on c.CountryId= ad.Country
	left join lkp_tb_province p on p.StateID=ad.State
	left join tb_offers off on ct.ContractId-off.ContractId
	left join tb_bankdetails bd on ct.ContractId = bd.ContractId
	left join lkp_tb_banknames ltb on bd.BankId = ltb.BankId
	left join lkp_tb_expenses lte on ct.ContractTypeId=lte.ContractTypeID and (ct.LoanedAmount between lte.MinimumFundingAmount and lte.MaximumFundingAmount)
where    
	ct.MerchantId=iNMerchantId and ct.ContractId=iNContractId limit 1; 

select CONCAT_WS(' ', co.FirstName, co.MiddleName, co.LastName) AS ownerName,
-- ID number
	co.jobTitle, 
    owad.Phone1 as telephone, 
    owad.email1 as email,
    PassportNbr,
	owad.Address1,
		owad.Address2,
		owad.City,
		owad.Phone1,
		mcnt.CountryName,
        st.State
        
from tb_owners ow 
	left join tb_contacts co on ow.ContactId = co.ContactId
	left join tb_addresses owad on co.AddressId1 = owad.AddressId
    left join lkp_tb_country mcnt on owad.Country = mcnt.CountryId 
	left join lkp_tb_province st on owad.State=st.StateID
	
where ow.MerchantId = iNMerchantId and ow.IsAuthorized=1;

Select entityTypeId, name as entityName
FROM lkp_tb_entitytypes;

END;
//
DELIMITER ;

