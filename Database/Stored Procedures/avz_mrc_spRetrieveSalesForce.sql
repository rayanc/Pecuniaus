drop procedure if exists  `avz_mrc_spRetrieveSalesForce`;
DELIMITER $$
CREATE PROCEDURE avz_mrc_spRetrieveSalesForce(

iNlegalName varchar(200),
iNbusinessName varchar(200),
iNownerName varchar(200),
iNrnc varchar(20)
)
begin
if(iNlegalName='' and iNbusinessName ='' and iNownerName='' and  iNrnc='')
then 

SELECT MerchantId_TMP as merchantId,
    legalName,
    businessName,
    SalesRepId,
    businessStartDate,
    IndustryTypeId,
    BusinessTypeId,
    RNCNumber as rnc,
    TelePhone1 as phone1,
    TelePhone2 as phone2
    
FROM tmp_tb_merchants;
else 
SELECT MerchantId_TMP as merchantId,
    legalName,
    businessName,
    SalesRepId,
    businessStartDate,
    IndustryTypeId,
    BusinessTypeId,
    RNCNumber as rnc,
    TelePhone1 as phone1,
    TelePhone2 as phone2
    
FROM tmp_tb_merchants
where legalName like concat('%',iNlegalName,'%') or businessName like concat('%',iNbusinessName,'%') or RNCNumber like concat('%',iNrnc,'%');
end if;
end$$
DELIMITER ;
