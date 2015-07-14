
drop procedure if exists avz_mrc_spretrieveMerchantDataEntry;
DELIMITER $$

CREATE PROCEDURE `avz_mrc_spretrieveMerchantDataEntry`(iNmerchantId bigint)
begin


SELECT 
    `mrc`.`LegalName` AS `merchantName`,
    `mrc`.`LegalName` AS `legalName`,
    `mrc`.`BusinessName` AS `businessName`,
     `mrc`.`BusinessStartDate`  AS `businessStartDate`,
    `mrc`.`BusinessTypeId` AS `businessTypeId`,
    `mrc`.`BusinessWebSite` AS `businessWebSite`,
    `adr`.`Phone1` as `phone1`,
    `adr`.`Phone2` as `phone2`,
     IFNULL( `mrc`.`RentedAmount`,0) as `rentAmount`,
    `mrc`.`RNCNumber` AS `rnc`,
	`mrc`.`RentFlag` as `propertyType`,
	`mrc`.`GrossAnnualSales` as `annualSales`,
    `mrc`.`IndustryTypeId` as `industryTypeId`,
    ((`repsal`.`FirstName` + ',') + `repsal`.`LastName`) AS `assignedSales`,
    `mrc`.`SalesRepId` AS `salesRepId`,
    `tsktyp`.`TaskName` AS `taskName`,
    `wrk`.`WorkFlowName` AS `workFlowName`,
    `tsk`.`WorkflowId` AS `workflowId`,  
     IFNULL( `tsk`.`AssignedUserId`,0) AS `assigneduserId`,
    `tsk`.`AssignedDate` AS `assignedDate`,
    `tsk`.`MerchantId` AS `merchantId`,
    `adr`.`address1` as `addressLine1`,
    `adr`.`address2` as `addressLine2`,
    `adr`.`country`,
    `adr`.`city`,
    `pro`.`State`,
    `adr`.`zipcode` as `zipId`,
    `adr`.`email1` as `email`,
	`bnk`.`BankId` as `bankId` ,
	`bnk`.`BankCode` as `bankcode` ,
	`bnk`.`AccountNumber` as `accountNumber`,
    `bnk`.`AccountName` as `accountName`,	
	`mrc`.`addressid` as `addressId`,
     `pro`.`StateID` AS `stateId`,
	 `cnrt`.`LoanedAmount` as `loanAmountRequired`,
	`cnrt`.`ContractId` as `contractId`,
	`mrc`.`SalesRepId` as `primarySalesRepId`,
	`cnrt`.`TypeOfAdvances` as `typeofadvances`,
	`cnrt`.`AnnualSalesCalcFile` as AnnualSalesCalcFile,
	(select salesRepId from tb_contractsalesrep where IsPrimary=0 and contractId= `cnrt`.`ContractId`  limit 1) as `secondarySalesRepId`	

FROM
    (((((`tb_merchants` `mrc`
    LEFT JOIN `tb_addresses` `adr` ON `adr`.`addressid` = `mrc`.`addressid`
	left JOIN `lkp_tb_province` `pro` on `pro`.`StateID`	=`adr`.`State`
	left join `tb_contracts` `cnrt` on `cnrt`.`MerchantId` =`mrc`.`MerchantId` and  `cnrt`.`StatusId`  not in (20007,21002)
    left JOIN `tb_salesrep` `sal` ON ((`mrc`.`SalesRepId` = `sal`.`SalesRepId`)))
    left JOIN `tb_salesrep_contact` `repsal` ON ((`sal`.`Contactid` = `repsal`.`ContactId`)))
    LEFT JOIN `tb_tasks` `tsk` ON ((`tsk`.`MerchantId` = `mrc`.`MerchantId`)))
    LEFT JOIN `tb_tasktypes` `tsktyp` ON ((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`)))
    LEFT JOIN `tb_workflows` `wrk` ON ((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`))
	left join `tb_bankdetails` `bnk` on ((`bnk`.`MerchantId` = `mrc`.`MerchantId`))
	left join `tb_BankStatements` `bnks` on ((`bnks`.`BankDetailId`=`bnk`.`BankDetailId`))
	left join `tb_contractsalesrep` `consal` on ((`consal`.`contractId` = `cnrt`.`ContractId`))
	left JOIN `tb_salesrep` `contsal` ON ((`contsal`.`SalesRepId` = `consal`.`SalesRepId`)))
    where `mrc`.`MerchantId`=iNmerchantId ;
	
	
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
    
    
SELECT 
    prlist.name AS processorname,
    prlist.processorid AS processorTypeId,
    pro.processorId AS processorId,
	pro.processorNumber as processorNumber,
	pro.firstProcessedDate as firstProcessedDate
FROM
    tb_processor pro
        JOIN
    lkp_tb_processorlist prlist ON pro.ProcessorTypeId = prlist.processorid
WHERE
    merchantid = iNmerchantId;

select bnks.StatementId as StatementId
,bnks.Statementyear as Statementyear
, bnks.StatementMonthId as StatementMonthId
, bnks.amount as amount
from tb_BankStatements bnks
left join tb_bankdetails bnk on bnk.BankDetailId=bnks.BankDetailId
where bnk.merchantId=iNmerchantId;

select    
		mrcl.BuildingName as landlordcompany
		, mrcl.LandlordId as landlordId
		, cnt.FirstName as firstname
		, cnt.LastName as lastname
		, adr.Phone1 as telephone
		
	from tb_merchants mrc
	left join tb_merchantlandlords mrcl on mrcl.merchantId=mrc.MerchantId
	left join tb_contacts cnt on cnt.ContactId =mrcl.ContactId
	left join tb_addresses adr on adr.AddressId=cnt.AddressId1

where mrc.merchantId=iNmerchantId;

select ref.TradeRefId, ref.Name, ref.PhoneNumber from tb_tradereferences ref
where ref.MerchantId=iNmerchantId;

end