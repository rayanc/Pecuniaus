drop procedure if exists avz_mrc_spRetrieveMerchant;
DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spRetrieveMerchant`(
iNmerchantId bigint
)
begin

SELECT 
    'merchantName' as merchantName,
    LegalName AS legalname,
    BusinessName AS businessName,
    BusinessStartDate as businessStartDate,
    BusinessTypeId,
    BusinessWebSite businessUrl,
    RNCNumber as rnc,
    repsal.FirstName+',' +repsal.LastName as assignedSales
FROM
    tb_merchants mrc
    join tb_salesrep sal 
    on mrc.SalesRepId=sal.SalesRepId
    join tb_salesrep_contact repsal 
    on sal.Contactid=repsal.ContactId
    where merchantId=iNmerchantId;
    end$$
DELIMITER ;