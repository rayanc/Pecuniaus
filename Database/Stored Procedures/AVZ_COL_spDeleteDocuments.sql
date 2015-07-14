-- ================================================
-- Name: AVZ_COL_spDeleteDocuments
-- 
-- Description : To delete documents from Collection screen
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spDeleteDocuments;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spDeleteDocuments
(iNDocumentIds varchar(500)
)
BEGIN

SET @sql_text:=concat(
   'delete 
      from tb_documents
    WHERE 
      documentId IN 
      (',iNDocumentIds,')');
PREPARE stmt from @sql_text;
EXECUTE stmt ;
DEALLOCATE PREPARE stmt;

END$$
DELIMITER ;