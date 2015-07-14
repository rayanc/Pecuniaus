drop procedure if exists CreateMReviewTask;
delimiter $$
create procedure CreateMReviewTask
(
iNTaskTypeId bigint,
iNStatusId bigint,
iNpriority bigint,
iNstartDate datetime,
iNmerchantId bigint,
iNcontractId bigint,
iNaffiliationFlag smallint,
iNmerchantIdTmp bigint,
iNworkflowId bigint,
iNInsertUserId bigint

)
begin

insert into tb_tasks(TaskTypeId,StatusId,priority,startDate,merchantId,contractId,affiliationFlag,merchantIdTmp
,workflowId, assigneduserId, AssignedDate ) 
values(iNTaskTypeId,iNStatusId,iNpriority,iNstartDate,iNmerchantId,iNcontractId,iNaffiliationFlag,iNmerchantIdTmp
,iNworkflowId,iNInsertUserId, NOW() );

end