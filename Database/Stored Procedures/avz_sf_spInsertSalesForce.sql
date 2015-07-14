drop procedure if exists  `avz_sf_spInsertSalesForce`;
delimiter $$
CREATE  PROCEDURE `avz_sf_spInsertSalesForce`(
iNlegalName varchar(200),
iNbusinessName varchar(200),
iNsalesRepId varchar(200),
iNbusinessStartDate date,
iNindustryTypeId int,
iNbusinessTypeId int,
iNinsertUserId bigint,
iNinsertDate datetime,
iNrnc varchar(11),
iNcardNetNbr varchar(20),
iNvNetnbr varchar(20),
iNtelephone1 varchar(20),
iNtelephone2 varchar(20),
iNcity varchar(200),
iNprovince varchar(200),
iNcountry varchar(200),
iNindustryType varchar(200),
iNbusinessType varchar(200),
iNjobTitle varchar(200),
iNfirstname varchar(200),
iNlastName varchar(200),
iNisOwner bit,
iNhomePhone varchar(25),
iNcellPhone varchar(25),
iNaccountId varchar(200),
iNsfId varchar(200),
iNsalesRepName varchar(200)
)
begin
declare _merchantId bigint;
start transaction;
SELECT 
    EXISTS( SELECT 
            merchantid_tmp
        FROM
            tmp_tb_merchants
        WHERE
            accountId = iNaccountId)
INTO _merchantId;

if (_merchantId=0) then 
begin 
INSERT INTO tmp_tb_merchants
(
`CompanyId`,
`LegalName`,
`BusinessName`,
`SalesRepId`,
`BusinessStartDate`,
`IndustryTypeId`,
`BusinessTypeId`,
`InsertUserId`,
`InsertDate`,
`RNCNumber`,
`CNetProcessorNbr`,
`VNetProcessorNbr`,
`TelePhone1`,
`TelePhone2`,
`City`,
`Province`,
`Country`,
`BusinessType`,
`IndustryType`,
`AccountId`,
`salesRepName`
)
VALUES
(
5,
iNlegalName,
iNbusinessName,
iNsalesRepId,
iNbusinessStartDate,
iNindustryTypeId,
iNbusinessTypeId,
iNinsertUserId,
now(),
iNrnc,
iNcardNetNbr ,
iNvNetnbr,
iNtelephone1,
iNtelephone2,
iNcity ,
iNprovince ,
iNcountry,
iNbusinessType,
iNindustryType,
iNaccountId,
iNsalesRepName
);

set  _merchantId= last_insert_id();
INSERT INTO `tmp_tb_contacts`
(
`merchantId`,
`JobTitle`,
`FirstName`,
`LastName`,
`isowner`,
`cellphone`,
`homephone`,
`sfId`

)
VALUES
(last_insert_id(),iNjobTitle,iNfirstname,iNlastName,iNisOwner ,iNhomePhone ,iNcellPhone,iNsfId );
# Create a merchant review task for the newly inserted merchant.
call CreateTask(1 ,22001 ,1 ,now() ,null ,null ,0 ,_merchantId ,1 );
commit;
end;
else 
begin
declare merchantidtmp bigint;

SELECT 
    merchantid_tmp
INTO merchantidtmp FROM
    tmp_tb_merchants
WHERE
    `AccountId` = iNaccountId;
UPDATE tmp_tb_merchants 
SET 
    `CompanyId` = 5,
    `LegalName` = iNlegalName,
    `BusinessName` = iNbusinessName,
    `SalesRepId` = iNsalesRepId,
    `BusinessStartDate` = iNbusinessStartDate,
    `IndustryTypeId` = iNindustryTypeId,
    `BusinessTypeId` = iNbusinessTypeId,
    `ModifyUserId` = 1,
    `Modifydate` = now(),
    `RNCNumber` = iNrnc,
    `CNetProcessorNbr` = iNcardNetNbr,
    `VNetProcessorNbr` = iNvNetnbr,
    `TelePhone1` = iNtelephone1,
    `TelePhone2` = iNtelephone2,
    `City` = iNcity,
    `Province` = iNprovince,
    `Country` = iNcountry,
    `BusinessType` = iNbusinessType,
    `IndustryType` = iNindustryType
WHERE
    merchantid_tmp = merchantidtmp;

UPDATE tmp_tb_contacts 
SET 
    `JobTitle` = iNjobTitle,
    `FirstName` = iNfirstname,
    `LastName` = iNlastName,
    `isOwner` = iNisOwner,
    `homePhone` = iNhomePhone,
    `cellPhone` = iNcellPhone
WHERE
    sfid = iNsfId;
end;
end if;
end ;
