 
INSERT INTO `tb_addresses`
(
`AddressTypeId`,
`Address1`,
`Address2`,
`Country`,
`City`,
`State`,
`ZipCodeId`,
`Phone1`,
`Phone2`,
`Phone3`,
`email1`,
`email2`,
`email3`,
`InsertUserId`,
`InsertDate`)
VALUES
(1,'Address1','Address 2','USA','','',1,'','','','','','',1,NOW());

select LAST_INSERT_ID();

INSERT INTO `tb_contacts`
(`ContactTypeId`,`JobTitle`,`FirstName`,
`MiddleName`,`LastName`,AddressId1,`DateOfbirth`,`SSN`)
VALUES
( 1,'Sales Rep','F Name','M Name','L Name',LAST_INSERT_ID(),'1981-09-05','123456789');

INSERT INTO tb_merchants
(
CompanyId,
LegalName,
BusinessName,
WorkflowId,
SalesRepId,
BusinessStartDate,
IndustryTypeId,
BusinessTypeId,
InsertUserId,
InsertDate,
OwnerId,
RNCNumber,
TelePhone1,
statusid
)
VALUES
(

5,
'LegalName',
'BusinessName',
1,
5,
now(),
1,
1,
1,
now(),
1,
'123123213',
'9501024684',20001);