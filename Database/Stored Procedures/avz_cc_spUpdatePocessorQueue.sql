drop procedure if exists avz_cc_spUpdatePocessorQueue;
delimiter $$
create procedure avz_cc_spUpdatePocessorQueue(iNmerchantId bigint, iNprocessorId varchar(100))
begin 

#Sent- 1
#processed 2
update tb_ccvolumesqueue set isprocessed=1, processedDate=now() where  merchantId=iNmerchantId and processornumber= iNprocessorId;

end 