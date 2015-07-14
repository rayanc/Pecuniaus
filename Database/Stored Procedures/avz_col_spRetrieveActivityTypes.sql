-- ================================================
-- Name: avz_col_spRetrieveActivityTypes
-- 
-- Description : To Retrieve collection activity list
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 01-11-2014
-- ================================================
Drop procedure if exists avz_col_spRetrieveActivityTypes;

delimiter $$
create procedure avz_col_spRetrieveActivityTypes()
begin

select  ActivityTypeId as keyId, Name as value from tb_collectionactivitylist where isActive=1;


END$$
DELIMITER ;