drop procedure if exists avz_mrc_spInsertProcessorDetails;
-- ************************* INSERT PROCESSOR DETAILS ***************************

DELIMITER $$
CREATE PROCEDURE `avz_mrc_spInsertProcessorDetails`
(
	pMerchantId bigint(20),
	pProcessorTypeIds smallint(6)
)
BEGIN
	INSERT INTO tb_processor (MerchantID,ProcessorTypeIds) VALUES (pMerchantId,pProcessorTypeIds);
END $$
DELIMITER ;


