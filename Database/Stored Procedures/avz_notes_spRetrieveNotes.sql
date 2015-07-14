drop procedure avz_notes_spRetrieveNotes;
DELIMITER //
#call avz_notes_spRetrieveNotes(134)
CREATE PROCEDURE avz_notes_spRetrieveNotes
(iNmerchantId bigint)
BEGIN
SELECT 
noteID,NT.noteTypeId,NTY.Description as noteTypeDescription,merchantId,contractId,note,workFlowId,screenName,u.UserName
FROM Pecuniaus.tb_notes as NT
Inner Join Pecuniaus.lkp_tb_notetypes NTY on NTY.NoteTypeId=NT.NoteTypeId
Left join tb_users u on NT.InsertUserID = u.ID
where merchantId=iNmerchantId;
END //
DELIMITER ;
