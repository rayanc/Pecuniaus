drop procedure if exists avz_mrc_spCompleteReview;
delimiter $$ 
create procedure avz_mrc_spCompleteReview
(
iNcompanyId smallint,
iNlegalName varchar(200) ,
iNbusinessName varchar(200),
iNworkflowId smallint,
iNaddressTypeId bigint,
iNaddressTypeId2 bigint,
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
iNvNetProcessorId varchar(200) ,
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
declare address1, address2,iNemail1,iNemail2, iNemail3,iNjobTitle,iNfirstName,iNlastName varchar(200);
declare iNphone1,iNphone2,iNphone3,iNmiddleName varchar(12);
declare iNssn varchar(9);
declare iNcountry,iNcity,instate varchar(100) ;
declare iNzipCodeId,iNinsertUserId,iOaddressId,iNcontactTypeId,iNaddressId bigint;

/* Insert the address  */
CALL `avz`.`avz_gen_spInsertAddress`(iNaddressTypeId, iNaddress1 , iNAddress2 ,
 iNcountry , iNcity , instate , iNzipCodeId , 
 iNphone1 , iNphone2 , iNphone3 , 
 iNemail1 , iNemail2 , iNemail3 , iNinsertUserId , iOaddressId );

CALL `avz`.`avz_gen_spInsertContact`(iNcontactTypeId , iNjobTitle , iNfirstName , 
iNmiddleName , iNlastName , iOaddressId , iNdateofBirth ,
 iNssn );


INSERT INTO `avz`.`tb_merchants`
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
iOaddressId,
iOaddressId,
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
end $$ 
delimiter $$ 