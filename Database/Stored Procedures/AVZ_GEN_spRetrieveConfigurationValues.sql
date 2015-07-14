-- ================================================
-- Name: AVZ_GEN_spRetrieveConfigurationValues
-- 
-- Description : To Retrieve values from configuration table
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 01-11-2014
-- ================================================
Drop procedure if exists AVZ_GEN_spRetrieveConfigurationValues;

DELIMITER $$
CREATE PROCEDURE AVZ_GEN_spRetrieveConfigurationValues
(iNConfig varchar(100),
iNConfigSystem varchar(45)
)
BEGIN

select configId as keyId,value from tb_configuration where Config=iNConfig and ConfigSystem=iNConfigSystem;

END$$
DELIMITER ;