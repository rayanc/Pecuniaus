drop view if exists avz_vw_merchantTempdetails;
delimiter $$
CREATE VIEW `avz_vw_merchantTempdetails` AS
    SELECT 
        `mrc`.`LegalName` AS `merchantName`,
        `mrc`.`LegalName` AS `legalName`,
        `mrc`.`BusinessName` AS `businessName`,
        `mrc`.`BusinessStartDate` AS `businessStartDate`,
        `mrc`.`BusinessTypeId` AS `BusinessTypeId`,
        '' AS `businessUrl`,
        `mrc`.`RNCNumber` AS `RNCNumber`,
        ((`repsal`.`FirstName` + ',') + `repsal`.`LastName`) AS `assignedSales`,
        `mrc`.`SalesRepId` AS `salesRepId`,
        `tsktyp`.`TaskName` AS `taskName`,
        `wrk`.`WorkFlowName` AS `workFlowName`,
        `tsk`.`WorkflowId` AS `WorkflowId`,
        `tsk`.`ContractId` AS `contractid`,
        `tsk`.`AssignedUserId` AS `assigneduserId`,
        `tsk`.`AssignedDate` AS `assignedDate`,
        `mrc`.`MerchantId_TMP` AS `merchantId`
    FROM
        (((((`tmp_tb_merchants` `mrc`
        JOIN `tb_salesrep` `sal` ON ((`mrc`.`SalesRepId` = `sal`.`SalesRepId`)))
        JOIN `tb_salesrep_contact` `repsal` ON ((`sal`.`Contactid` = `repsal`.`ContactId`)))
        LEFT JOIN `tb_tasks` `tsk` ON ((`tsk`.`MerchantId` = `mrc`.`MerchantId_TMP`)))
        LEFT JOIN `tb_tasktypes` `tsktyp` ON ((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`)))
        LEFT JOIN `tb_workflows` `wrk` ON ((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`)))