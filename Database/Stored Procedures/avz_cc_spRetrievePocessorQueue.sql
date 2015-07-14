drop procedure if exists avz_cc_spRetrievePocessorQueue;
delimiter $$
create procedure avz_cc_spRetrievePocessorQueue()
begin 
select distinct que.merchantId as merchantId,
pro.ProcessorTypeId, 
prol.name  as processorCode,
pro.processorNumber,
prol.ProcessorCode as ProcessorId
from tb_ccvolumesqueue  que
join tb_processor pro
on que.merchantId=pro.MerchantId 
and que.processorId=pro.ProcessorId 
join lkp_tb_processorlist prol
on pro.ProcessorTypeId= prol.ProcessorId and que.isprocessed is null;

SELECT 
    prol.ProcessorCode AS processorId, name
FROM
    lkp_tb_processorlist prol;
end 



