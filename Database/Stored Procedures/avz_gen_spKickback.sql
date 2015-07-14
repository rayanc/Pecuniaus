drop procedure if exists avz_gen_spKickback;
delimiter $$
create procedure avz_gen_spKickback(iNtasktypeId bigint,iNmerchantId bigint,iNcontractid bigint,iNworkflowId bigint)
begin 

SET SQL_SAFE_UPDATES = 0;
UPDATE tb_tasks 
SET 
    statusid = 22005
WHERE
    merchantId = iNmerchantId
        AND statusid in( 22002,22001);

# Create a new task for the stage where the deal is sent to be kicked back
call avz_tsk_spCreateTask(iNtasktypeId,22001,1,now(),iNmerchantId,iNcontractid,1,null,iNworkflowId);

end;
