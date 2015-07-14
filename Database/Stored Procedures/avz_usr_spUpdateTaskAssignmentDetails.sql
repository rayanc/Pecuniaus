

drop procedure if exists avz_usr_spUpdateTaskAssignmentDetails;

delimiter $$

create procedure avz_usr_spUpdateTaskAssignmentDetails
(
iNUserID bigint,iNTaskID bigint
)
begin
update tb_tasks set AssignedUserId=iNUserID Where TaskID=iNTaskID;
end;
