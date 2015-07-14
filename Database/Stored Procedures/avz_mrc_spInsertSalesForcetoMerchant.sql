delimiter $$


create procedure `avz_mrc_spInsertSalesForcetoMerchant`(iNaccountId varchar(200), iNmerchantid_tmp bigint)
begin

update tmp_tb_merchants set accountId= iNaccountId where merchantid_tmp= iNmerchantid_tmp;

end;

