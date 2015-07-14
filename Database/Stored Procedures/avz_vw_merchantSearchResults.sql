CREATE 
 
VIEW `avz_vw_merchantSearchResults` AS
    select 
        `mrc`.`LegalName` AS `merchantName`,
        `mrc`.`LegalName` AS `legalName`,
        `mrc`.`BusinessName` AS `BusinessName`,
        `mrc`.`RNCNumber` AS `RNCNumber`,
        concat(`repsal`.`FirstName`,
                ' ',
                `repsal`.`LastName`) AS `assignedSalesRep`,
        concat(`owncont`.`FirstName`,
                ' ',
                `owncont`.`LastName`) AS `ownerName`,
        `mrc`.`SalesRepId` AS `salesRepId`,
        `tsktyp`.`TaskName` AS `taskName`,
        `tsktyp`.`TaskTypeId` AS `taskTypeId`,
        `wrk`.`WorkFlowName` AS `workFlowName`,
        `tsk`.`WorkflowId` AS `WorkflowId`,
        `tsk`.`ContractId` AS `contractid`,
        `tsk`.`AssignedUserId` AS `assigneduserId`,
        `mrc`.`MerchantId` AS `merchantId`,
        `tskst`.`StatusId` AS `taskStatusId`,
        `tskst`.`StatusName` AS `tasKStatus`
    from
        (((((((((`tb_merchants` `mrc`
        join `tb_salesrep` `sal` ON ((`mrc`.`SalesRepId` = `sal`.`SalesRepId`)))
        join `tb_salesrep_contact` `repsal` ON ((`sal`.`Contactid` = `repsal`.`ContactId`)))
        left join `tb_tasks` `tsk` ON ((`tsk`.`MerchantId` = `mrc`.`MerchantId`)))
        left join `tb_tasktypes` `tsktyp` ON ((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`)))
        left join `tb_workflows` `wrk` ON ((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`)))
        left join `tb_owners` `own` ON ((`own`.`OwnerId` = `mrc`.`OwnerId`)))
        left join `lkp_tb_entitytypes` `ent` ON ((`ent`.`EntitytypeID` = `mrc`.`BusinessTypeId`)))
        left join `lkp_tb_statuses` `tskst` ON ((`tsk`.`StatusId` = `tskst`.`StatusId`)))
        join `tb_contacts` `owncont` ON ((`owncont`.`ContactId` = `own`.`ContactId`)))