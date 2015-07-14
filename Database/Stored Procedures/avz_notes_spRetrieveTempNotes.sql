drop procedure if exists avz_notes_spRetrieveTempNotes;
DELIMITER $$
#call avz_notes_spRetrieveTempNotes(134)
CREATE  PROCEDURE `avz_notes_spRetrieveTempNotes`(iNmerchantId bigint)
BEGIN
	SELECT n.noteID,n.noteTypeId,n.merchantId,n.note,n.screenName,u.UserName
	FROM
	tmp_tb_notes n
	Left join tb_users u on n.InsertUserID = u.ID
	where merchantId=iNmerchantId;
END$$
DELIMITER ;
