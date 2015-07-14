drop procedure avz_notes_spRetrieveMatchedNotes;
DELIMITER $$
CREATE PROCEDURE avz_notes_spRetrieveMatchedNotes
(
  pSearchString VARCHAR(500)
)
BEGIN
SELECT NoteID,NoteTypeId,MerchantId,ContractId,Note,WorkFlowId,ScreenName FROM tb_Notes WHERE Note LIKE CONCAT('%', pSearchString ,'%');
END $$
DELIMITER ;