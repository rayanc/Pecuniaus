drop procedure avz_notes_spInsertNotes;
delimiter $$
create procedure avz_notes_spInsertNotes
(
pNoteTypeId varchar(20),
pMerchantId bigint(20),
pContractId bigint(20),
pNote longtext,
pWorkFlowId smallint(20),
pScreenName varchar(50)
)
BEGIN
  INSERT INTO tb_notes(NoteTypeId,MerchantId,ContractId,Note,WorkFlowId,ScreenName)
 VALUES(pNoteTypeId,pMerchantId,pContractId,pNote,pWorkFlowId,pScreenName);
END $$

delimiter $$



-- ********************* UPDATE NOTES DETAILS ****************************


drop procedure avz_notes_spupdateNoteDetails;
delimiter //
CREATE PROCEDURE avz_notes_spupdateNoteDetails
     (
       pNoteID BIGINT(20),
pNoteTypeId CHAR(20),
pMerchantId bigint(20),
pContractId bigint(20),
pNote longtext,
pWorkFlowId smallint(20),
pScreenName varchar(50)
     )
BEGIN

    UPDATE tb_Notes SET
NoteTypeId = pNoteTypeId,
MerchantId = pMerchantId,
ContractId = pContractId,
Note = pNote,
WorkFlowId = pWorkFlowId,
ScreenName = pScreenName
WHERE NoteID = pNoteID;

END //
delimiter;


-- ********************* Retrieve NOTES DETAILS ****************************
drop procedure avz_notes_spRetrieveNotes;
DELIMITER //
CREATE PROCEDURE avz_notes_spRetrieveNotes
()
BEGIN
SELECT NoteID,NoteTypeId,MerchantId,ContractId,Note,WorkFlowId,ScreenName FROM tb_Notes ORDER BY NoteID;
END //
DELIMITER ;

-- ********************* Retrieve Matched NOTES ****************************
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

