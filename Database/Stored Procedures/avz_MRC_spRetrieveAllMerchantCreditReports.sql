drop procedure if exists avz_MRC_spRetrieveAllMerchantCreditReports;

delimiter $$

create procedure avz_MRC_spRetrieveAllMerchantCreditReports
(iNcontractId bigint)
begin 
SELECT CreditReportID as keyId,Name as description FROM tb_creditreports where contractId=iNcontractId;

end $$
delimiter $$
