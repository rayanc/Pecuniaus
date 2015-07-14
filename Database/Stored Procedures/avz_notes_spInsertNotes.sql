drop procedure if exists  avz_notes_spInsertNotes;
delimiter $$
CREATE  PROCEDURE `avz_notes_spInsertNotes`(
iNnoteTypeId varchar(20),
iNmerchantId bigint(20),
iNcontractId bigint(20),
iNNote longtext,
iNworkFlowId smallint(20),
iNscreenName varchar(50),
iNinsertUserId bigint(20)
)
BEGIN
  INSERT INTO tb_notes(NoteTypeId,MerchantId,ContractId,Note,WorkFlowId,ScreenName,InsertUserID,Insertdate)
  
 VALUES(iNnoteTypeId,iNmerchantId,iNcontractId,iNNote,iNworkFlowId,iNscreenName,iNinsertUserId,now());
END

