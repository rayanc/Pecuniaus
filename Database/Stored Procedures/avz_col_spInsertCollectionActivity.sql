-- ================================================
-- Name: avz_col_spInsertCollectionActivity
-- 
-- Description : To save values for collection activity
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 01-11-2014
-- ================================================
Drop procedure if exists avz_col_spInsertCollectionActivity;

DELIMITER $$
CREATE PROCEDURE  avz_col_spInsertCollectionActivity 
(iNActivityTypeId bigint,
iNNotes longtext,
iNMerchantId bigint,
iNContractId bigint,
iNInsertUserId bigint
)
BEGIN
insert into tb_collectionactivities (ContractId,MerchantId,ActivityTypeId,CollectionComments,InsertUserId,InsertDate) 
values(iNContractId,iNMerchantId,iNActivityTypeId,iNNotes,iNInsertUserId,now());

INSERT INTO tb_notes(NoteTypeId,MerchantId,ContractId,Note,WorkFlowId,ScreenName,InsertUserID,Insertdate)  
VALUES(560001,iNMerchantId,iNContractId,iNNotes,6,'CollectionWorkflow',iNInsertUserId,now());

END$$
DELIMITER ;