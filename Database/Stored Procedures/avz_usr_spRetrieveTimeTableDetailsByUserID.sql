use Pecuniaus;
drop procedure if exists avz_usr_spRetrieveTimeTableDetailsByUserID;
delimiter $$
create procedure avz_usr_spRetrieveTimeTableDetailsByUserID
(
  iNWorkFlowID int,
  iNUserID bigint(20)

)
begin
if exists(select UserId from tb_userstimetabledetails as TimeTableDetails
inner join  tb_tasktypes taskTypes on TimeTableDetails.TaskTypeID = taskTypes.TaskTypeID
 where TimeTableDetails.UserID= iNUserID and taskTypes.workflowId=iNWorkFlowID) then
Begin
#UserTimetableDetailsID,UserTimetableID,TaskTypeID

SELECT  
TimeTableDetails.UserTimetableDetailsID,
TimeTableDetails.UserTimetableID,
TimeTableDetails.UserID ,taskTypes.TaskTypeID ,
taskTypes.TaskName,
TimeTableDetails.Monday ,
TimeTableDetails.Tuesday,
TimeTableDetails.Wednesday,
TimeTableDetails.Thursday,
TimeTableDetails.Friday,
TimeTableDetails.Saturday,
TimeTableDetails.Sunday

FROM tb_userstimetabledetails as TimeTableDetails
 join  tb_tasktypes taskTypes on TimeTableDetails.TaskTypeID = taskTypes.TaskTypeID
and taskTypes.WorkFlowId = iNWorkFlowID
 join tb_userstimetable as Timetable on Timetable.UsersTimeTableID = TimeTableDetails.UserTimetableID
and TimeTableDetails.UserID=iNUserID ;
end;
else
begin
SELECT  
0 as UserTimetableDetailsID,0 as UserTimetableID, iNUserID as UserID ,taskTypes.TaskTypeID ,taskTypes.TaskName,
0 as Monday ,0 as Tuesday ,
0 as Wednesday ,0 as Thursday ,
0 as Friday ,0 as Saturday ,
0 as Sunday

 FROM tb_workflows Wrkflow
 join  tb_tasktypes taskTypes on Wrkflow.WorkFlowId = taskTypes.WorkFlowId
where Wrkflow.WorkFlowId= iNWorkFlowID;
end;
end if;
end;
