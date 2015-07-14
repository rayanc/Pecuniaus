
DROP procedure IF EXISTS `CompleteReviewTask`;

DELIMITER $$

CREATE PROCEDURE `CompleteReviewTask`(
iNmerchantId bigint,
iNnewmerchantId bigint,
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
update tb_tasks set statusid= 22004, EndDatte= now() where tasktypeId=iNtasktypeId and (MerchantIDTMP= iNmerchantId or merchantid=iNmerchantId);
select tasktypeId into nextTask from tb_tasktypes  where workflowid=iNworkflowId and sequence= (select sequence from tb_tasktypes  where workflowid=iNworkflowId and tasktypeid=iNtaskTypeId)+1;
call CreateTask(nextTask,22001,1,now(),iNnewmerchantId,0,1,null,iNworkflowId);
commit;
end$$

DELIMITER ;

