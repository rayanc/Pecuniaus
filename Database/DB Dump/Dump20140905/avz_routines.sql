CREATE DATABASE  IF NOT EXISTS `avz` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `avz`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win32 (x86)
--
-- Host: localhost    Database: avz
-- ------------------------------------------------------
-- Server version	5.6.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping routines for database 'avz'
--
/*!50003 DROP PROCEDURE IF EXISTS `AVZ_TSK_spAssignTasks` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AVZ_TSK_spAssignTasks`()
BEGIN

	DECLARE pTaskID INT;
	DECLARE pTaskTypeID INT;
	DECLARE pWorkflowID INT;
	DECLARE pMerchantID INT;
	DECLARE pContractID INT;
	DECLARE pUserID INT;

CREATE TEMPORARY TABLE Tasks (
    TaskID bigint, 
    TaskTypeID int,
	WorkflowID int,
	MerchantID int,
	ContractID int
);
	Insert Into Tasks
	SELECT TaskID
		,TaskTypeID
		,WorkflowID
		,MerchantID
		,ContractID
	FROM tb_Tasks
	WHERE statusID = 210000 -- unassigned
	ORDER BY InsertDate;

	SET pTaskID = NULL;

	SELECT TaskID
			, TaskTypeID
			 INTO pTaskID, pTaskTypeID
		FROM Tasks limit 1;
		   
	WHILE pTaskID IS NOT NULL DO	
		SELECT UserID INTO pUserID
		FROM tb_PrequalUsers
		WHERE TaskTypeID = pTaskTypeID
		ORDER BY AssignedDate limit 1;

		UPDATE tb_Tasks
		SET AssignedUserID = pUserID
			,AssignedDate = NOW()
			,ModifyDate = NOW()
			,ModifyUserID = 2
			,StatusID = 210010 -- Assigned            
		WHERE TaskID = pTaskID;

		UPDATE tb_PrequalUsers
		SET AssignedDate = NOW()
		WHERE UserID = pUserID;

		DELETE
		FROM Tasks
		WHERE TaskID = pTaskID;

		SET pTaskID = NULL;
		SET pUserID = NULL;

		SELECT TaskID
			, TaskTypeID
			 INTO pTaskID, pTaskTypeID
		FROM Tasks limit 1;

	END while;
Drop Table Tasks;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-09-05 14:56:59
