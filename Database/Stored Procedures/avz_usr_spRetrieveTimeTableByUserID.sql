use Pecuniaus;
drop procedure if exists avz_usr_spRetrieveTimeTableByUserID;
delimiter $$
create procedure avz_usr_spRetrieveTimeTableByUserID
(
 iNWorkFlowID int,
 iNUserID bigint
)
begin
if exists(select UserId from tb_userstimetable where UserID =iNUserID and WorkFlowID= iNWorkFlowID) then
Begin

SELECT  
userTimetable.UsersTimeTableID as UserTimeTableID,userTimetable.UserID ,userTimetable.WorkFlowID ,userTimetable.AssignedDate ,userTimetable.IsMondayWork ,
userTimetable.IsTuesdayWork ,userTimetable.IsWednesdayWork ,userTimetable.IsThursdayWork ,userTimetable.IsFridayWork 
,userTimetable.IsSaturdayWork ,userTimetable.IsSundayWork ,
ifnull(userTimetable.MondayFromTime  ,'')as MondayFromTime , ifnull(userTimetable.MondayToTime   ,'') as MondayToTime    ,
ifnull(userTimetable.TuesdayFromTime   ,'')as TuesdayFromTime  ,ifnull(userTimetable.TuesdayToTime  ,'') as TuesdayToTime   ,
ifnull(userTimetable.WednesdayFromTime ,'')as WednesdayFromTime ,ifnull(userTimetable.WednesdayToTime,'') as WednesdayToTime ,
ifnull(userTimetable.ThursdayFromTime  ,'')as ThursdayFromTime ,ifnull(userTimetable.ThursdayToTime ,'') as ThursdayToTime  ,
ifnull(userTimetable.FridayFromTime    ,'')as FridayFromTime ,ifnull(userTimetable.FridayToTime   ,'') as FridayToTime     ,
ifnull(userTimetable.SaturdayFromTime  ,'')as SaturdayFromTime ,ifnull(userTimetable.SaturdayToTime ,'') as SaturdayToTime   ,
ifnull(userTimetable.SundayFromTime    ,'')as SundayFromTime ,ifnull(userTimetable.SundayToTime   ,'') as SundayToTime 

FROM tb_userstimetable userTimetable 
where userTimetable.WorkFlowId = iNWorkFlowID and userTimetable.UserID=iNUserID
limit 1;



end;
else
begin
SELECT  
iNUserID as UserID ,iNWorkFlowID as WorkFlowID ,null as AssignedDate ,
0 as IsMondayWork ,0 as IsTuesdayWork ,
0 as IsWednesdayWork ,0 as IsThursdayWork ,
0 as IsFridayWork ,0 as IsSaturdayWork ,
 0 as IsSundayWork,
'' as MondayFromTime ,'' as MondayToTime ,
'' as TuesdayFromTime ,'' as TuesdayToTime ,
'' as WednesdayFromTime ,'' as WednesdayToTime ,
'' as ThursdayFromTime ,'' as ThursdayToTime ,
'' as FridayFromTime ,'' as FridayToTime ,
'' as SaturdayFromTime ,'' as SaturdayToTime ,
'' as SundayFromTime ,'' as SundayToTime 
limit 1
;
end;
end if;
end;
