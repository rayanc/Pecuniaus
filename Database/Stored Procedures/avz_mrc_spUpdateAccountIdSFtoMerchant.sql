DROP PROCEDURE IF EXISTS avz_mrc_spUpdateAccountIdSFtoMerchant;

DELIMITER $$

CREATE PROCEDURE avz_mrc_spUpdateAccountIdSFtoMerchant(iNofferId bigint,iNaccountId varchar(200))
BEGIN 

UPDATE tb_offers SET SFID= iNaccountId, IsSynced=1 WHERE OfferId= iNofferId;

END;