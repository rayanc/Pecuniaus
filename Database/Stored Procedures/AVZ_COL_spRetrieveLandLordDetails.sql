-- ================================================
-- Name: AVZ_COL_spRetrieveLandLordDetails
-- 
-- Description : To Retrieve landlord deatils for collection screen
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 01-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrieveLandLordDetails;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveLandLordDetails
(iNMerchantId bigint
)
BEGIN
Select 
TYP.TypeId as typeOfPropertyId, MRC.LandlordName as landlordCompanyName, 
-- concat_ws(' ',CNT.FirstName, CNT.MiddleName,CNT.LastName, ' ') as landlordName,
CNT.FirstName as landlordFirstName,
CNT.LastName as landlordLastName,
MRC.RentedAmount as monthlyRentAmount, ADR1.Address1 as address, ADR1.Phone1 as telePhone,
ADR1.Phone2 as cellPhone, ADR1.email1 as email, ADR1.city
from tb_merchants MRC
-- left join tb_owners OWN on MRC.MerchantId = OWN.MerchantId
left join tb_merchantlandlords LL on MRC.MerchantId= LL.MerchantId
left join tb_contacts CNT on LL.ContactId = CNT.ContactId
left Join tb_addresses as ADR1 on CNT.AddressId1=ADR1.AddressId
left Join lkp_tb_types TYP on TYP.TypeId = MRC.RentFlag and TYP.Usage='PTY'
where MRC.MerchantId=iNMerchantId limit 1;

END$$
DELIMITER ;