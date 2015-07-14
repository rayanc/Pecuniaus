-- ================================================
-- Name: AVZ_COL_spRetrieveLawyers
-- 
-- Description : To Retrieve lawyers list to assign them for collections
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spRetrieveLawyers;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spRetrieveLawyers
()
BEGIN

select lw.lawyerId, lw.firstName, lw.lastName, lw.companyName, merchantId
from tb_lawyers lw
order by lw.companyName;

END$$
DELIMITER ;