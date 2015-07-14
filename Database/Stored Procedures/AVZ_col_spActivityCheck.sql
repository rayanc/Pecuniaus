-- ================================================
-- Name: AVZ_col_spActivityCheck
-- 
-- Description : To verify the activity before saving notes
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 01-11-2014
-- ================================================
Drop procedure if exists AVZ_col_spActivityCheck;

DELIMITER $$
CREATE PROCEDURE AVZ_col_spActivityCheck
(iNMerchantId bigint,
 iNContractId bigint
)
BEGIN

declare pNotifyPayment boolean;
declare pPaymentAgreement boolean;

SELECT IF( EXISTS(
             SELECT 1
             FROM tb_documents
             WHERE DocumentTypeId =  20 AND MerchantId = iNMerchantId AND ContractId = iNContractId
			 and (FileName is not null or FileName <> '') ), 1, 0) into pNotifyPayment;

SELECT IF( EXISTS(
             SELECT 1
             FROM tb_documents
             WHERE DocumentTypeId =  12 AND MerchantId = iNMerchantId AND ContractId = iNContractId
			 and (FileName is not null or FileName <> '') ), 1, 0) into pPaymentAgreement;

select pNotifyPayment,pPaymentAgreement;

END$$
DELIMITER ;