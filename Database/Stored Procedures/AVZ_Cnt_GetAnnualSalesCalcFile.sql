DROP procedure IF EXISTS `AVZ_Cnt_GetAnnualSalesCalcFile`;

DELIMITER $$

CREATE PROCEDURE AVZ_Cnt_GetAnnualSalesCalcFile
(
	iNMerchantID bigint,
	iNContractId bigint
)

	select Contractid,
		MerchantId,
		AnnualSalesCalcFile 
	from tb_contracts 
	where Contractid=iNContractId and MerchantId=iNMerchantID;

$$

DELIMITER ;

