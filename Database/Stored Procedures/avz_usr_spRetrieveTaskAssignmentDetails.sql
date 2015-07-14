

drop procedure if exists avz_usr_spRetrieveTaskAssignmentDetails;

delimiter $$

create procedure avz_usr_spRetrieveTaskAssignmentDetails
(
iNUserID bigint
)
begin
Select TT.TaskID,TT.AssignedUserId UserID,USR.UserName,MRC.merchantId MerchantID,MRC.BusinessName MerchantName,TTY.TaskName,ST.StatusName,AssignedDate
from tb_tasks TT
Inner Join tb_tasktypes TTY on TTY.TaskTypeId=TT.TaskTypeId
Inner Join tb_users USR on USR.Id=TT.AssignedUserId
Inner Join tb_merchants MRC on MRC.MerchantId=TT.MerchantId
Inner Join lkp_tb_statuses ST on ST.StatusId=TT.StatusId
Where ST.StatusId=22002 and USR.id=iNUserID;
end;
