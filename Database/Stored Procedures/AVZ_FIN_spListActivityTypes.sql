-- ================================================
-- Name: AVZ_FIN_spListActivityTypes
-- 
-- Description : To Retrieve values for actibity types
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 15-10-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_FIN_spListActivityTypes 
()
BEGIN

select activityId as keyId, activityName as value from lkp_tb_financeactivitylist where IsActive=1;

END;
//
DELIMITER ;
