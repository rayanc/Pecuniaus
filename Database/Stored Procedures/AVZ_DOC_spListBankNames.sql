-- ================================================
-- Name: AVZ_DOC_spListBankNames
-- 
-- Description : To retrieve list of all Bank Names 
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 17-09-2014
-- ================================================
drop procedure if exists AVZ_DOC_spListBankNames;
DELIMITER //
CREATE PROCEDURE AVZ_DOC_spListBankNames
()
BEGIN
select 
   bankId, bankName
from
    lkp_tb_banknames;
END;
//

DELIMITER ;
call AVZ_DOC_spListBankNames();