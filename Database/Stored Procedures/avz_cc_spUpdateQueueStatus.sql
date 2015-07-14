drop procedure if exists avz_cc_spUpdateQueueStatus;
delimiter $$
 create procedure avz_cc_spUpdateQueueStatus()
 begin
 
 create temporary table ProcessedMerchants(merchantid bigint);
 insert into ProcessedMerchants
 SELECT DISTINCT
    CCV.merchantId
FROM
    tb_CreditCardVolumes CCV
        INNER JOIN
    tb_ccvolumesqueue CCQ ON CCV.merchantId = CCQ.merchantId
        AND CCV.processorId = CCQ.processorId;
 set sql_safe_updates=0;
update tb_ccvolumesqueue set isprocessed=2 where merchantid in(
SELECT merchantid from  ProcessedMerchants);
        
drop table ProcessedMerchants;
end;