-- ================================================
-- Name: avz_usr_spAddUpdateUserTimeTable
-- 
-- Description : To add update user's time table
-- 
-- Parameters :  
-- 
-- Author  : Amit Kumar
-- 
-- Creation Date : 15-11-2014
-- ================================================
Use pecuniaus;
Drop procedure if exists avz_usr_spAddUpdateUserTimeTable;

DELIMITER $$
CREATE PROCEDURE avz_usr_spAddUpdateUserTimeTable
(
  `iNUserTimetableID`  bigint, 
  `iNUserID` bigint, 
  `iNWorkFlowID` int(11),
  `iNIsMondayWork` tinyint(1) ,
  `iNIsTuesdayWork` tinyint(1) ,
  `iNIsWednesdayWork` tinyint(1) ,
  `iNIsThursdayWork` tinyint(1) ,
  `iNIsFridayWork` tinyint(1) ,
  `iNIsSaturdayWork` tinyint(1) ,
  `iNIsSundayWork` tinyint(1) ,
  `iNMondayFromTime` varchar(45) ,
  `iNMondayToTime` varchar(50) ,
   `iNTuesdayFromTime` varchar(50) ,
  `iNTuesdayToTime` varchar(50) ,
   `iNWednesdayFromTime` varchar(50) ,
  `iNWednesdayToTime` varchar(50) ,
   `iNThursdayFromTime` varchar(50) ,
  `iNThursdayToTime` varchar(50) ,
  `iNFridayFromTime` varchar(50) ,
  `iNFridayToTime` varchar(50) ,
  `iNSaturdayFromTime` varchar(50) ,
  `iNSaturdayToTime` varchar(50) ,
  `iNSundayFromTime` varchar(50) ,
  `iNSundayToTime` varchar(50) ,
  `iNInsertUserID` bigint(20) ,
  `iNModifyUserID` bigint(20)

  )
  
Begin

     
    IF NOT EXISTS (SELECT timetable.UserID from tb_userstimetable as timetable
    where
        timetable.UserID= iNUserID
       and timetable.WorkFlowID= iNWorkFlowID and UsersTimeTableID=iNUserTimetableID)
THEN
INSERT INTO `Pecuniaus`.`tb_userstimetable`
(`UserID`,`WorkFlowID`,`AssignedDate`,`IsMondayWork`,`IsTuesdayWork`,`IsWednesdayWork`,`IsThursdayWork`,
`IsFridayWork`,`IsSaturdayWork`,`IsSundayWork`,`MondayFromTime`,`MondayToTime`,`TuesdayFromTime`,`TuesdayToTime`,`WednesdayFromTime`,
`WednesdayToTime`,`ThursdayFromTime`,`ThursdayToTime`,`FridayFromTime`,`FridayToTime`,`SaturdayFromTime`,`SaturdayToTime`,`SundayFromTime`,
`SundayToTime`,`InsertUserId`,`InsertDate`,`ModifyUserId`,`ModifyDate`)
 select iNUserID,iNWorkFlowID,now(),iNIsMondayWork,iNIsTuesdayWork,iNIsWednesdayWork,iNIsThursdayWork,iNIsFridayWork,iNIsSaturdayWork,iNIsSundayWork,
 REPLACE(iNMondayFromTime, "'", "") as MondayFromTime,
 REPLACE(iNMondayToTime, "'", "") as MondayToTime,
 REPLACE(iNTuesdayFromTime, "'", "") as TuesdayFromTime,
 REPLACE(iNTuesdayToTime, "'", "") as TuesdayToTime,
 REPLACE(iNWednesdayFromTime, "'", "") as WednesdayFromTime,
 REPLACE(iNWednesdayToTime, "'", "") as WednesdayToTime,
 REPLACE(iNThursdayFromTime, "'", "") as ThursdayFromTime,
 REPLACE(iNThursdayToTime, "'", "") as ThursdayToTime,
 REPLACE(iNFridayFromTime, "'", "") as FridayFromTime,
 REPLACE(iNFridayToTime, "'", "") as FridayToTime,
 REPLACE(iNSaturdayFromTime, "'", "") as SaturdayFromTime,
 REPLACE(iNSaturdayToTime, "'", "") as SaturdayToTime,
 REPLACE(iNSundayFromTime, "'", "") as SundayFromTime,
 REPLACE(iNSundayToTime, "'", "") as SundayToTime,
`iNInsertUserId`,now(),`iNModifyUserId`,now();
set iNUserTimetableID=LAST_INSERT_ID();

 
	
ELSE 

SET SQL_SAFE_UPDATES=0;
    UPDATE `tb_userstimetable`
SET
`UserID` = iNUserID ,
`WorkFlowID` = iNWorkFlowID ,
`AssignedDate` = now() ,
`IsMondayWork` =    REPLACE(iNIsMondayWork , "'", "")   ,
`IsTuesdayWork` =   REPLACE(iNIsTuesdayWork, "'", "")   ,
`IsWednesdayWork` = REPLACE(iNIsWednesdayWork, "'", "") ,
`IsThursdayWork` =  REPLACE(iNIsThursdayWork, "'", "")  ,
`IsFridayWork` =    REPLACE(iNIsFridayWork, "'", "")    ,
`IsSaturdayWork` =  REPLACE(iNIsSaturdayWork, "'", "")  ,
`IsSundayWork` =    REPLACE(iNIsSundayWork, "'", "")    ,
`MondayFromTime` =  REPLACE(iNMondayFromTime, "'", "")  ,
`MondayToTime` =    REPLACE(iNMondayToTime, "'", "")    ,
`TuesdayFromTime` = REPLACE(iNTuesdayFromTime , "'", ""),
`TuesdayToTime` = REPLACE(iNTuesdayToTime , "'", ""),
`WednesdayFromTime` = REPLACE(iNWednesdayFromTime , "'", ""),
`WednesdayToTime` = REPLACE(iNWednesdayToTime , "'", ""),
`ThursdayFromTime` = REPLACE(iNThursdayFromTime , "'", ""),
`ThursdayToTime` = REPLACE(iNThursdayToTime, "'", ""),
`FridayFromTime` = REPLACE(iNFridayFromTime , "'", ""),
`FridayToTime` = REPLACE(iNFridayToTime, "'", ""),
`SaturdayFromTime` = REPLACE(iNSaturdayFromTime , "'", ""),
`SaturdayToTime` = REPLACE(iNSaturdayToTime, "'", ""),
`SundayFromTime` = REPLACE(iNSundayFromTime , "'", ""),
`SundayToTime` = REPLACE(iNSundayToTime, "'", ""),
`InsertUserId` = iNInsertUserId ,
`ModifyUserId` = iNModifyUserId ,
`ModifyDate` = now() 
WHERE `UsersTimeTableID` = iNUserTimeTableID;
  
END IF;


SELECT timetable.UsersTimeTableID from tb_userstimetable as timetable
    where
        timetable.UserID= iNUserID
       and timetable.WorkFlowID= iNWorkFlowID limit 1;


END;
$$
DELIMITER ;

