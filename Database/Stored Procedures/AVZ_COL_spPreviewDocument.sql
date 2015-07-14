-- ================================================
-- Name: AVZ_COL_spPreviewDocument
-- 
-- Description : To generate preview of documents
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spPreviewDocument;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spPreviewDocument
(iNDocumentId bigint
)
BEGIN
Select FileName,FileDetails from tb_documents where DocumentId=iNDocumentId;
END$$
DELIMITER ;