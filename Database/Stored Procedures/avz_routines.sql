
-- Temporary table structure for view `avz_vw_merchantdetails`
--

DROP TABLE IF EXISTS `avz_vw_merchantdetails`;
/*!50001 DROP VIEW IF EXISTS `avz_vw_merchantdetails`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `avz_vw_merchantdetails` (
  `merchantName` tinyint NOT NULL,
  `legalName` tinyint NOT NULL,
  `businessName` tinyint NOT NULL,
  `businessStartDate` tinyint NOT NULL,
  `BusinessTypeId` tinyint NOT NULL,
  `businessUrl` tinyint NOT NULL,
  `RNCNumber` tinyint NOT NULL,
  `assignedSales` tinyint NOT NULL,
  `salesRepId` tinyint NOT NULL,
  `taskName` tinyint NOT NULL,
  `workFlowName` tinyint NOT NULL,
  `WorkflowId` tinyint NOT NULL,
  `contractid` tinyint NOT NULL,
  `assigneduserId` tinyint NOT NULL,
  `assignedDate` tinyint NOT NULL,
  `merchantId` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `avz_vw_merchantdetails`
--

/*!50001 DROP TABLE IF EXISTS `avz_vw_merchantdetails`*/;
/*!50001 DROP VIEW IF EXISTS `avz_vw_merchantdetails`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50001 VIEW `avz_vw_merchantdetails` AS select 'merchantName' AS `merchantName`,`mrc`.`LegalName` AS `legalName`,`mrc`.`BusinessName` AS `businessName`,`mrc`.`BusinessStartDate` AS `businessStartDate`,`mrc`.`BusinessTypeId` AS `BusinessTypeId`,`mrc`.`BusinessWebSite` AS `businessUrl`,`mrc`.`RNCNumber` AS `RNCNumber`,((`repsal`.`FirstName` + ',') + `repsal`.`LastName`) AS `assignedSales`,`mrc`.`SalesRepId` AS `salesRepId`,`tsktyp`.`TaskName` AS `taskName`,`wrk`.`WorkFlowName` AS `workFlowName`,`tsk`.`WorkflowId` AS `WorkflowId`,`tsk`.`ContractId` AS `contractid`,`tsk`.`AssignedUserId` AS `assigneduserId`,`tsk`.`AssignedDate` AS `assignedDate`,`tsk`.`MerchantId` AS `merchantId` from (((((`tb_merchants` `mrc` join `tb_salesrep` `sal` on((`mrc`.`SalesRepId` = `sal`.`SalesRepId`))) join `tb_salesrep_contact` `repsal` on((`sal`.`Contactid` = `repsal`.`ContactId`))) join `tb_tasks` `tsk` on((`tsk`.`MerchantId` = `mrc`.`MerchantId`))) join `tb_tasktypes` `tsktyp` on((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`))) join `tb_workflows` `wrk` on((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-10-01 16:37:57
