drop procedure if exists avz_mrc_spUpdateSyncStatusMerchant;
delimiter $$
create procedure avz_mrc_spUpdateSyncStatusMerchant(iNaccountId varchar(200), iNmerchantId bigint)
begin

update tb_merchants set accountId= iNaccountId,isynced=1 where merchantid= iNmerchantId;

end;

