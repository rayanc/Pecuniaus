Drop procedure if exists avz_notes_spUpdateNoteDetails;
delimiter $$
CREATE PROCEDURE avz_notes_spUpdateNoteDetails
     (
       iNNoteID BIGINT(20),
iNNoteTypeId CHAR(20),
iNMerchantId bigint(20),
iNContractId bigint(20),
iNNote longtext,
iNWorkFlowId smallint(20),
iNScreenName varchar(50)
     )
BEGIN

    UPDATE tb_Notes 
SET
NoteTypeId = iNNoteTypeId,
MerchantId = iNMerchantId,
ContractId = iNContractId,
Note = iNNote,
WorkFlowId = iNWorkFlowId,
ScreenName = iNScreenName
WHERE 
NoteID = pNoteID;

END ;
