DELIMITER $$
CREATE  PROCEDURE `avz_notes_spInsertTempNotes`(
iNnoteTypeId varchar(20),
iNmerchantId bigint(20),
iNNote longtext,
iNworkFlowId smallint(20),
iNscreenName varchar(50),
iNinsertUserId bigint(20)

)
BEGIN
  INSERT INTO tb_notes(NoteTypeId,MerchantId,Note,WorkFlowId,ScreenName,InsertUserID)
  
 VALUES(iNnoteTypeId,iNmerchantId,iNNote,iNworkFlowId,iNscreenName,iNinsertUserId);
END$$
DELIMITER ;
