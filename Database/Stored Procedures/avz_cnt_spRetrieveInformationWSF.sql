drop procedure if exists avz_cnt_spRetrieveInformationWSF;

delimiter $$

create procedure avz_cnt_spRetrieveInformationWSF()
begin 

SELECT 
    proc.processorNumber,
    lst.ProcessorCode,
    proc.merchantid,
    lst.name,
    cnt.ownedAmount AS balance,
    IFNULL(cnt.buyrate, 0.0) AS rate

FROM
    tb_processor proc
        JOIN
    lkp_tb_processorlist lst ON proc.ProcessorTypeId = lst.ProcessorId
        JOIN
    tb_merchants mrc ON mrc.merchantid = proc.merchantid
        JOIN
    tb_contracts cnt ON proc.merchantid = cnt.merchantid
WHERE
    cnt.StatusId = 20007
        AND mrc.StatusId != 10001;




SELECT 
    prol.ProcessorCode AS processorId, name
FROM
    lkp_tb_processorlist prol;
end;
