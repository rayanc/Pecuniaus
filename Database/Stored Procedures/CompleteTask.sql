
DROP procedure IF EXISTS `CompleteTask`;

DELIMITER $$

CREATE PROCEDURE `CompleteTask`(
iNmerchantId bigint,
iNtasktypeId bigint,
iNworkflowId bigint

)
begin
declare nextTask bigint;
declare _sequence bigint;
start transaction;
/*
Pending- 1
Completed=2
Assgined=3
*/
select tasktypeId into nextTask from tb_tasktypes  where workflowid=iNworkflowId and sequence= (select sequence from tb_tasktypes  where workflowid=iNworkflowId and tasktypeid=iNtaskTypeId)+1;
update tb_tasks set statusid= 22004, EndDatte= date_format(now(),'%y-%m-%d') where tasktypeId=iNtasktypeId and merchantId= iNmerchantId;
call CreateTask(nextTask,22001,1,now(),iNmerchantId,0,1,null,iNworkflowId);
commit;
end$$

DELIMITER ;

