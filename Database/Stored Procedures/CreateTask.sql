drop procedure if exists CreateTask;
delimiter $$
create procedure CreateTask
(
iNTaskTypeId bigint,
iNStatusId bigint,
iNpriority bigint,
iNstartDate datetime,
iNmerchantId bigint,
iNcontractId bigint,
iNaffiliationFlag smallint,
iNmerchantIdTmp bigint,
iNworkflowId bigint
)
begin

insert into tb_tasks(TaskTypeId,StatusId,priority,startDate,merchantId,contractId,affiliationFlag,merchantIdTmp,workflowId ) 
values(iNTaskTypeId,iNStatusId,iNpriority,iNstartDate,iNmerchantId,iNcontractId,iNaffiliationFlag,iNmerchantIdTmp,iNworkflowId );

end