
-- ================================================
-- Name: avz_tsk_spCompleteTask
-- 
-- Description : To complete the tasks for Merchants and Contract
-- 
-- Parameters : None 
-- 
-- Author  : Tarun Bansal
-- 
-- Creation Date : 25-09-2014
--
-- Modified Date: 07-10-2014
-- Modified By: Sachin Verma
-- Added Contract Id 
-- ================================================
drop procedure if exists avz_tsk_spCompleteTask;
DELIMITER //
create procedure avz_tsk_spCompleteTask
(
	iNmerchantId bigint,
	iNtasktypeId bigint,
	iNworkflowId bigint,
	iNcontractId bigint
)
begin
declare nextTask bigint;
declare _sequence bigint;
start transaction;
select 
    tasktypeId
into nextTask from
    tb_tasktypes
where
    workflowid = iNworkflowId
        and sequence = (select 
            sequence
        from
            tb_tasktypes
        where
            workflowid = iNworkflowId
                and tasktypeid = iNtaskTypeId) + 1;
If iNcontractId <= 0 Then				
	update tb_tasks 
	set 
		EndDatte=date_format(now(),'%y-%m-%d'),
		StatusId = 22004
	where
		tasktypeId = iNtasktypeId
			and merchantId = iNmerchantId;
	call avz_tsk_spCreateTask(nextTask,22001,1,now(),iNmerchantId,0,1,null,iNworkflowId);
ELSE

	update tb_tasks 
	set 
		StatusId = 22004,
		EndDatte=date_format(now(),'%y-%m-%d')
	where
		tasktypeId = iNtasktypeId
			and merchantId = iNmerchantId;
			/*and contractId = iNcontractId*/
	call avz_tsk_spCreateTask(nextTask,22001,1,now(),iNmerchantId,iNcontractId,1,null,iNworkflowId);	
	
END IF;
if(iNtasktypeId in (6,7, 8,10,11)) then
begin

	update tb_contracts set statusId=case when iNtasktypeId=6 then 30002 
		when iNtasktypeId=7 then 20010  
		when iNtasktypeId=8 then 20002 
		when iNtasktypeId=10 then 20002 
		when iNtasktypeId=11 then 20004 
		else statusId
		end
	where contractId=iNcontractId;

	update tb_merchants set statusId=case when iNtasktypeId=6 then 10008 
		when iNtasktypeId=7 then 10009  
		else statusId
		end
	where merchantId=iNmerchantId;
end;
end if;
/* if(iNtasktypeId=8) then
	update tb_contracts set statusId=20003 where contractId=iNcontractId;

end if; */
if(iNtasktypeId=13) then
	update tb_contracts set statusId=20004 where contractId=iNcontractId;

end if;

if(iNtasktypeId=15) then
	update tb_contracts set statusId=20005 where contractId=iNcontractId;

end if;

if(iNtasktypeId=16) then
	update tb_contracts set statusId=20006 where contractId=iNcontractId;

end if;
if(iNtasktypeId=17) then
	update tb_contracts set statusId=20007 where contractId=iNcontractId;
	update tb_merchants set statusId=10010
	where merchantId=iNmerchantId;

end if;

if(iNtasktypeId=3) then
	call avz_cc_spInsertProcessorRequest(iNmerchantId,0);
end if;

commit;
end;
//
DELIMITER ;