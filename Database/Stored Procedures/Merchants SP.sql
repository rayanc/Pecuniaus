DELIMITER $$
CREATE PROCEDURE `avz_mrc_RetrieveRecentSearch`(iNmerchantId bigint)
begin
/*
merchant name
business name 
task name 
task staus 
*/
select mrc.BusinessName,tsktype.TaskName,stat.StatusName from avz.tb_merchants mrc 
join avz.tb_tasks tsk
on mrc.merchantid= tsk.merchantid
join avz.tb_tasktypes  tsktype
on tsktype.TaskTypeId=tsk.TaskTypeId
join lkp_tb_statuses stat
on stat.StatusId=tsk.StatusId

where mrc.MerchantId =iNmerchantId;

end$$

DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_gen_spRetrieveIndustry`()
begin
SELECT lkp_tb_industrytype.id,
    lkp_tb_industrytype.industryName    
FROM avz.lkp_tb_industrytype;

end$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spDeleteOwner`(
iNOwnerId bigint,
iNMerchantId bigint,
iNBusinessStartDate datetime,
iNRentFlag tinyint,
iNRentAmount double,
iNMortgageAmount double,
iNInsertUserId bigint,
iNInsertDate datetime,
iNModifyUser varchar(500),
iNModifyDate datetime,
iNContactId bigint
)
begin

delete from `tb_owners`
where `OwnerId`=iNOwnerId;
end$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spInsertMerchant`(
iNcompanyId smallint,
iNlegalName varchar(200) ,
iNbusinessName varchar(200),
iNworkflowId smallint,
iNaddressId bigint,
iNaddressId2 bigint,
iNstatusId bigint,
iNstatusModifyDate datetime,
iNsalesRepId bigint,
iNbusinessStartDate date,
iNnndustryTypeId int,
iNbusinessTypeId int,
iNinsertUserId bigint,
iNinsertDate datetime,
iNownerId bigint,
iNbankInfoId bigint,
iNcNetProcessorId varchar(20),
iNvNetProcessoIdd varchar(200) ,
iNbusinessWebSite varchar(500),
iNrentFlag tinyint,
iNlandlordName varchar(200),
iNrentedAmount double,
iNcurrencyId int,
iNrncNumber varchar(11),
iNtelePhone1 varchar(12),
iNtelePhone2 varchar(12)
)
BEGIN 
INSERT INTO `tb_merchants`
(
`CompanyId`,
`LegalName`,
`BusinessName`,
`WorkflowId`,
`AddressId`,
`AddressId2`,
`StatusId`,
`StatusModifyDate`,
`SalesRepId`,
`BusinessStartDate`,
`IndustryTypeId`,
`BusinessTypeId`,
`LastActivityDate`,
`InsertUserId`,
`InsertDate`,
`ModifyUserId`,
`ModifyDate`,
`OwnerId`,
`BankInfoId`,
`CNetProcessorId`,
`VNetProcessoIdd`,
`BusinessWebSite`,
`RentFlag`,
`LandlordName`,
`RentedAmount`,
`currencyId`,
`RNCNumber`,
`TelePhone1`,
`TelePhone2`)
VALUES
(

CompanyId,
LegalName,
BusinessName,
WorkflowId,
AddressId,
AddressId2,
StatusId,
StatusModifyDate,
SalesRepId,
BusinessStartDate,
IndustryTypeId,
BusinessTypeId,
LastActivityDate,
InsertUserId,
InsertDate,
ModifyUserId,
ModifyDate,
OwnerId,
BankInfoId,
CNetProcessorId,
VNetProcessoIdd,
BusinessWebSite,
RentFlag,
LandlordName,
RentedAmount,
currencyId,
RNCNumber,
TelePhone1,
TelePhone2);
END$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spInsertMerchantTemp`(
iNMerchantId_TMP bigint,
iNCompanyId smallint,
iNLegalName varchar(200),
iNBusinessName varchar(200),
iNWorkflowId smallint,
iNAddressId bigint,
iNAddressId2 bigint,
iNStatusId int,
iNStatusModifyDate datetime,
iNSalesRepId bigint,
iNBusinessStartDate date,
iNIndustryTypeId int,
iNBusinessTypeId int,
iNLastActivityDate datetime,
iNInsertUserId bigint,
iNInsertDate datetime,
iNModifyUserId bigint,
iNModifyDate datetime,
iNOwnerId bigint,
iNBankInfoId bigint,
iNCNetProcessorId varchar(20),
iNVNetProcessoIdd varchar(200),
iNBusinessWebSite varchar(500),
iNRentFlag tinyint,
iNLandlordName varchar(100),
iNRentedAmount double,
iNcurrencyId int,
iNRNCNumber varchar(11),
iNTelePhone1 varchar(12),
iNTelePhone2 varchar(12),
iNIsProcessed tinyint
)
begin

INSERT INTO tmp_tb_merchants
(`MerchantId_TMP`,
`CompanyId`,
`LegalName`,
`BusinessName`,
`WorkflowId`,
`AddressId`,
`AddressId2`,
`StatusId`,
`StatusModifyDate`,
`SalesRepId`,
`BusinessStartDate`,
`IndustryTypeId`,
`BusinessTypeId`,
`LastActivityDate`,
`InsertUserId`,
`InsertDate`,
`ModifyUserId`,
`ModifyDate`,
`OwnerId`,
`BankInfoId`,
`CNetProcessorId`,
`VNetProcessoIdd`,
`BusinessWebSite`,
`RentFlag`,
`LandlordName`,
`RentedAmount`,
`currencyId`,
`RNCNumber`,
`TelePhone1`,
`TelePhone2`,
`IsProcessed`)
VALUES
(iNMerchantId_TMP,
iNCompanyId,
iNLegalName,
iNBusinessName,
iNWorkflowId,
iNAddressId,
iNAddressId2,
iNStatusId,
iNStatusModifyDate,
iNSalesRepId,
iNBusinessStartDate,
iNIndustryTypeId,
iNBusinessTypeId,
iNLastActivityDate,
iNInsertUserId,
iNInsertDate,
iNModifyUserId,
iNModifyDate,
iNOwnerId,
iNBankInfoId,
iNCNetProcessorId,
iNVNetProcessoIdd,
iNBusinessWebSite,
iNRentFlag,
iNLandlordName,
iNRentedAmount,
iNcurrencyId,
iNRNCNumber,
iNTelePhone1,
iNTelePhone2,
iNIsProcessed);

end$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spInsertowner`(
iNOwnerId bigint,
iNMerchantId bigint,
iNBusinessStartDate datetime,
iNRentFlag tinyint,
iNRentAmount double,
iNMortgageAmount double,
iNInsertUserId bigint,
iNInsertDate datetime,
iNModifyUser varchar(500),
iNModifyDate datetime,
iNContactId bigint,
iNfirstName varchar(200),
iNlastName varchar(200),
iNpassportNumber varchar(20),
iNphone varchar(12),
iNphone2 varchar(12),
iNaddress1 varchar(200),
iNcity varchar(200),
iNState varchar(200),
inzipCode varchar(12),
iNemail varchar(200)
)
begin

INSERT INTO `tb_owners`
(`OwnerId`,
`MerchantId`,
`BusinessStartDate`,
`RentFlag`,
`RentAmount`,
`MortgageAmount`,
`InsertUserId`,
`InsertDate`,
`ModifyUser`,
`ModifyDate`,
`ContactId`)
VALUES
(iNOwnerId,
iNMerchantId,
iNBusinessStartDate,
iNRentFlag,
iNRentAmount,
iNMortgageAmount,
iNInsertUserId,
iNInsertDate,
iNModifyUser,
iNModifyDate,
iNContactId);
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `avz_mrc_spRetrieveOwnerbyMerchant`(
iNmerchantId bigint

)
begin

select 
cnt.FirstName +','+ cnt.LastName as ownerName,
cnt.SSN as ssn,
adr.Address1 as address1,
adr.Address2  as address2,
adr.Phone1 as phone

from tb_owners own
join tb_contacts cnt 
on own.ContactId= cnt.ContactId
join tb_addresses adr 
on adr.AddressId=cnt.AddressId1

where tb_owners.MerchantId=iNmerchantId;
end$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spRetrieveOwner`(
iNownerId bigint

)
begin

select 
cnt.FirstName +','+ cnt.LastName as ownerName,
cnt.SSN as ssn,
adr.Address1 as address1,
adr.Address2  as address2,
adr.Phone1 as phone

from tb_owners own
join tb_contacts cnt 
on own.ContactId= cnt.ContactId
join tb_addresses adr 
on adr.AddressId=cnt.AddressId1

where tb_owners.OwnerId=iNownerId;
end$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spRetrieveMerchants`(
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
    RNCNumber,
    repsal.FirstName+',' +repsal.LastName as assignedSales
FROM
    tb_merchants mrc
    join tb_salesrep sal 
    on mrc.SalesRepId=sal.SalesRepId
    join tb_salesrep_contact repsal 
    on sal.Contactid=repsal.ContactId;
    end$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_mrc_spUpdateMerchant`(
iNmerchantId bigint,
iNcompanyId smallint,
iNlegalName varchar(200) ,
iNbusinessName varchar(200),
iNworkflowId smallint,
iNmerchantIdStatusId bigint,
iNstatusModifyDate datetime,
iNbusinesstype int,
iNbusinessStartDate datetime,
iNindustryId int,
iNsalesRepId bigint,
iNindustryTypeId int,
iNrncNumber varchar(200),
inownerName varchar(200)

)
BEGIN 
UPDATE tb_merchants
SET CompanyId=iNcompanyId,
LegalName=iNlegalName,
BusinessName=iNbusinessName,
WorkflowId=iNworkflowId,
StatusModifyDate=iNstatusModifyDate,
BusinessTypeId=iNbusinesstype,
BusinessStartDate=iNbusinessStartDate,
IndustryId=iNindustryId,
IndustryTypeId=iNindustryTypeId,
RNCNumber= iNrncNumber

where tb_merchants.merchantid=iNmerchantId;


END$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `avz_mrc_spUpdateOwner`(
iNOwnerId bigint,
iNMerchantId bigint,
iNBusinessStartDate datetime,
iNRentFlag tinyint,
iNRentAmount double,
iNMortgageAmount double,
iNInsertUserId bigint,
iNInsertDate datetime,
iNModifyUser varchar(500),
iNModifyDate datetime,
iNContactId bigint
)
begin

update `tb_owners`
set `OwnerId`=iNOwnerId,
`BusinessStartDate`=iNBusinessStartDate,
`RentFlag`=iNRentFlag,
`RentAmount`=iNRentAmount,
`MortgageAmount`=iNMortgageAmount,
`InsertUserId`=iNInsertUserId,
`InsertDate`=iNInsertDate,
`ModifyUser`=iNModifyUser,
`ModifyDate`=iNModifyDate,
`ContactId`=iNContactId
where `MerchantId`=iNMerchantId;
end$$
DELIMITER ;

DELIMITER $$
CREATE  PROCEDURE `avz_sp_RetrieveMerchantTemp`(
iNmerchantId bigint
)
begin
SELECT `tmp_tb_merchants`.`MerchantId_TMP`,
    `tmp_tb_merchants`.`CompanyId`,
    `tmp_tb_merchants`.`LegalName`,
    `tmp_tb_merchants`.`BusinessName`,
    `tmp_tb_merchants`.`WorkflowId`,
    `tmp_tb_merchants`.`AddressId`,
    `tmp_tb_merchants`.`AddressId2`,
    `tmp_tb_merchants`.`StatusId`,
    `tmp_tb_merchants`.`StatusModifyDate`,
    `tmp_tb_merchants`.`SalesRepId`,
    `tmp_tb_merchants`.`BusinessStartDate`,
    `tmp_tb_merchants`.`IndustryTypeId`,
    `tmp_tb_merchants`.`BusinessTypeId`,
    `tmp_tb_merchants`.`LastActivityDate`,
    `tmp_tb_merchants`.`InsertUserId`,
    `tmp_tb_merchants`.`InsertDate`,
    `tmp_tb_merchants`.`ModifyUserId`,
    `tmp_tb_merchants`.`ModifyDate`,
    `tmp_tb_merchants`.`OwnerId`,
    `tmp_tb_merchants`.`BankInfoId`,
    `tmp_tb_merchants`.`CNetProcessorId`,
    `tmp_tb_merchants`.`VNetProcessoIdd`,
    `tmp_tb_merchants`.`BusinessWebSite`,
    `tmp_tb_merchants`.`RentFlag`,
    `tmp_tb_merchants`.`LandlordName`,
    `tmp_tb_merchants`.`RentedAmount`,
    `tmp_tb_merchants`.`currencyId`,
    `tmp_tb_merchants`.`RNCNumber`,
    `tmp_tb_merchants`.`TelePhone1`,
    `tmp_tb_merchants`.`TelePhone2`,
    `tmp_tb_merchants`.`IsProcessed`
FROM `tmp_tb_merchants`
where `tmp_tb_merchants`.MerchantId_TMP=merchantId;
end$$
DELIMITER ;
