-- ================================================
-- Name: AVZ_FIN_spProcessorNames
-- 
-- Description : To Retrieve values of Processor
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 15-10-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_FIN_spProcessorNames 
()
BEGIN

select processorId as keyId, name as value from lkp_tb_processorlist where IsActive=1;

END;
//
DELIMITER ;
