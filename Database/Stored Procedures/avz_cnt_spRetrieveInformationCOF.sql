drop procedure if exists avz_cnt_spRetrieveInformationCOF;

delimiter $$

create procedure avz_cnt_spRetrieveInformationCOF()
begin 

SELECT 
    lst.ProcessorCode,
	proc.processornumber,
    proc.ProcessorTypeId,
    proc.merchantid,
    lst.name,
    ifnull(cnt.ownedAmount,0)-ifnull(cnt.paidAmount,0) AS balance,
    IFNULL(cnt.buyrate, 0.0) AS rate,
    cnt.ContractId as contract,
    case cnt.statusid 
    when 20007 then 1 
    else
    0 
    end as fundedflag

FROM
    tb_processor proc
        JOIN
    lkp_tb_processorlist lst ON proc.ProcessorTypeId = lst.ProcessorId
        JOIN
    tb_merchants mrc ON mrc.merchantid = proc.merchantid
        JOIN
    tb_contracts cnt ON proc.merchantid = cnt.merchantid
WHERE
(cnt.statusid in (20005,20006,20007) and  mrc.StatusId != 10001) OR (cnt.ContractTypeID=13200 and  mrc.StatusId != 10001) 
OR (cnt.statusid=20008 and datediff(current_date(),cnt.LastPaymentDate)<=1);


SELECT 
    prol.ProcessorCode AS processorId, name
FROM
    lkp_tb_processorlist prol;
end;
