drop procedure if exists AVZ_CNT_spCorporateDocVerification;
-- ================================================
-- Name: AVZ_CNT_spCorporateDocVerification
-- 
-- Description : To Retrieve details for Corporate Documents
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 12-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_CNT_spCorporateDocVerification 
(
	iNMerchantId bigint,
	iNContractId bigint
)
BEGIN

	Select m.LegalName as nameOfCompany, ad.Address1 as addressDesc, m.RNCNumber 
		from tb_merchants m
		left join tb_addresses ad on m.AddressId=ad.AddressId
		where m.MerchantId=iNMerchantId; 

		
	SELECT 
		`ow`.`ownerId` AS `ownerId`,
		`cont`.`contactId` AS `contactId`,
		cont.firstname AS `ownerFirstName`,
		cont.lastname AS `ownerLastName`,
		cont.dateofbirth AS `ownerDOB`,
		cont.passportnbr,
		cont.contactid as `contactId`,
		adr.Phone1 AS `phone1`,
		adr.Phone2 AS `phone2`,
		cont.ssn,
		adr.address1 AS `addressLine1`,
		adr.address2 AS `addressLine2`,
		adr.country,
		adr.city,
		pro.state,
		pro.StateID AS `stateId`,
		adr.ZipCode AS `zip`,
		adr.email1 AS `email`,
		ow.rentamount,
		adr.addressid AS `addressId`,
		ow.IsAuthorized
	FROM
		tb_owners ow
			left JOIN
		tb_contacts cont ON cont.contactid = ow.contactid
			left JOIN
		tb_addresses adr ON cont.addressid1 = adr.addressid
			left JOIN
		lkp_tb_province pro ON pro.StateID = adr.State
	WHERE
		ow.merchantid = iNmerchantId;
    
	/*select  ow.OwnerId as OwnerId,
		ct.FirstName as OwnerName, 
        ct.LastName as OwnerLastName, 
        ct.PassportNbr, 
        ad.Phone1 as Telephone, 
        ow.IsAuthorized,
        ct.ContactId,
		ad.AddressId
		from tb_merchants m
		join tb_owners ow on m.MerchantId=ow.MerchantId
		left join tb_contacts ct on ow.ContactId=ct.ContactId
		left join tb_addresses ad on ct.AddressId1=ad.AddressId
		where m.MerchantId=iNMerchantId;
	*/
	select fileName, fileDetails
		from tb_documents
		where MerchantId=iNMerchantId and ContractId=iNContractId and documenttypeid=2; 

END;
//
DELIMITER ;
