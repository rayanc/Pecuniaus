-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
DELIMITER $$

CREATE  PROCEDURE `avz_mrc_spmerchantSearch`(
iNmerchantId bigint  ,
iNcontractId bigint,
intaskType varchar(200),
iNworkflowId bigint,
instatusId bigint,
iNrnc varchar(200),
iNlegalName varchar(200),
iNbusinessName varchar(200),
iNprocessornbr bigint,
iNprocessorName varchar(200)
)
begin

SELECT 
    LegalName AS legalname,
    BusinessName AS businessName,
    BusinessStartDate as businessStartDate,
    BusinessTypeId,
   # BusinessWebSite businessUrl,
    RNCNumber
    #concat(repsal.FirstName,'' ,repsal.LastName) as assignedSales
FROM
avz_vw_merchantdetails
where merchantid in (iNmerchantId) or contractid in (iNcontractId) or workflowid in (iNworkflowId) or 
 legalname  like CONCAT('%', legalname ,'%');


end