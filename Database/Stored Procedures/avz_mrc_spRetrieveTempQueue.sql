drop procedure if exists avz_mrc_spRetrieveTempQueue;
delimiter $$

create procedure avz_mrc_spRetrieveTempQueue()

begin 

SELECT mrc.merchantid_tmp as merchantId,
mrc.businessName,
mrc.legalName,
mrc.RNCNumber as rnc,
mrc.legalName as merchantName,
typ.taskname as taskName,
tsk.statusid as statusId,
stat.StatusName as taskStatus
FROM tmp_tb_merchants mrc 
join tb_tasks tsk on 
mrc.MerchantId_TMP=tsk.merchantidtmp
join tb_tasktypes typ
on typ.TaskTypeId=tsk.tasktypeid
join lkp_tb_statuses stat on 
tsk.statusid=stat.statusid
 where mrc.InsertUserId=10001;

end $$
delimiter ;