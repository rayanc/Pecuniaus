drop procedure if exists AVZ_DOC_spListDocTypes;
-- ================================================
-- Name: AVZ_DOC_spListDocTypes
-- 
-- Description : To retrieve all documents types related to a WorkflowId 
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-09-2014
-- ================================================
DELIMITER //
CREATE PROCEDURE AVZ_DOC_spListDocTypes
(iNWorkflowId int)
BEGIN
select 
   Description as documentName, documentTypeId
from
    lkp_tb_documenttypes
where
    WorkflowId=iNWorkflowId
	and isActive = 1 and isRequired=1

union

select 
   Description as documentName, documentTypeId
from
    lkp_tb_documenttypes
where
    documentTypeId=11
	and isActive = 1 and isRequired=1;

END;
//

DELIMITER ;
