-- ================================================
-- Name: avz_usr_spAddUpdateUserTimeTableDetails
-- 
-- Description : To add update workflow tasks  for users in usertimetabledetails table
-- 
-- Parameters : iNUserTimeTableDetailsXml 
-- 
-- Author  : Amit Kumar
-- 
-- Creation Date : 15-11-2014
-- ================================================
Use Pecuniaus;
Drop procedure if exists avz_usr_spAddUpdateUserTimeTableDetails;

DELIMITER $$
CREATE PROCEDURE avz_usr_spAddUpdateUserTimeTableDetails
(iNUserTimeTableDetailsXml text 
)
Begin
	declare  i int unsigned default 1;   
    declare pRowCount int unsigned;
	declare  j int unsigned default 1;   
    declare jRowCount int unsigned;
	Drop TEMPORARY TABLE if exists TusertimetableDetails;
     
   CREATE TEMPORARY TABLE `TusertimetableDetails` (
  `UserTimetableDetailsID` bigint NOT NULL, 
  `UserTimetableID` bigint(20),
  `UserID` bigint NOT NULL, 
   `TaskTypeID` int,
  `Monday` tinyint(1) NOT NULL,
  `Tuesday` tinyint(1) NOT NULL,
  `Wednesday` tinyint(1) NOT NULL,
  `Thursday` tinyint(1) NOT NULL,
  `Friday` tinyint(1) NOT NULL,
  `Saturday` tinyint(1) NOT NULL,
  `Sunday` tinyint(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL
  );

 
   
-- calculate the number of row elements.   
     set pRowCount  = extractValue(iNUserTimeTableDetailsXml, 'count(/TimeTable/Task)');
    select pRowCount;
	 while  i <= pRowCount do
      insert into  TusertimetableDetails 
       SELECT   
	   ExtractValue(iNUserTimeTableDetailsXml, '//Task[$i]/UserTimetableDetailsID'),
       ExtractValue(iNUserTimeTableDetailsXml, '//Task[$i]/UserTimetableID'),
	   ExtractValue(iNUserTimeTableDetailsXml, '//Task[$i]/UserID'),
	   ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/TaskTypeID'),
	   case ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/Monday') when 'true' then 1 when 'false' then 0 end,
	   case ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/Tuesday') when 'true' then 1 when 'false' then 0 end,
       case ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/Wednesday') when 'true' then 1 when 'false' then 0 end,
       case ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/Thursday') when 'true' then 1 when 'false' then 0 end,
	   case ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/Friday') when 'true' then 1 when 'false' then 0 end,
	   case ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/Saturday') when 'true' then 1 when 'false' then 0 end,
       case ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/Sunday') when 'true' then 1 when 'false' then 0 end,
       ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/InsertUserID'),
       ExtractValue(iNUserTimeTableDetailsXml,   '//Task[$i]/ModifyUserID');
       
  set i=i+1;
	 end while;
     # for inserting all the records that are not present in original table
  
  
	
    IF NOT EXISTS (SELECT timetableDetails.UserID from tb_userstimetabledetails
     as timetableDetails join  TusertimetableDetails on timetableDetails.UserTimetableID=TusertimetableDetails.UserTimetableID
       and timetableDetails.UserID= TusertimetableDetails.UserID
       and timetableDetails.taskTypeId= TusertimetableDetails.taskTypeId)
THEN

   INSERT INTO tb_userstimetabledetails
		  (UserTimetableID,UserID,TaskTypeID,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,InsertUserID,InsertDate,ModifyUserID,ModifyDate)
	
    select UserTimetableID, UserID,TaskTypeID,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,InsertUserID,now(),ModifyUserID,now() from TusertimetableDetails
     ;
	
ELSE
# for updating the records that are present in both temp table and original table
  
SET SQL_SAFE_UPDATES=0;
    UPDATE tb_userstimetabledetails
    INNER JOIN TusertimetableDetails as TimeTableDetails
    on TimeTableDetails.UserTimetableDetailsID = tb_userstimetabledetails.UserTimetableDetailsID   
    set tb_userstimetabledetails.UserID=TimeTableDetails.UserID
	,   tb_userstimetabledetails.TaskTypeID=TimeTableDetails.TaskTypeID
    ,   tb_userstimetabledetails.Monday=TimeTableDetails.Monday
    ,   tb_userstimetabledetails.`Tuesday`=TimeTableDetails.`Tuesday`
    ,   tb_userstimetabledetails.`Wednesday`=TimeTableDetails.`Wednesday`
	,   tb_userstimetabledetails.`Thursday`=TimeTableDetails.`Thursday`
    ,   tb_userstimetabledetails.`Friday`=TimeTableDetails.`Friday`
    ,   tb_userstimetabledetails.`Saturday`=TimeTableDetails.`Saturday`
	,   tb_userstimetabledetails.`Sunday`=TimeTableDetails.`Sunday`  
	,   tb_userstimetabledetails.`ModifyUserID`=TimeTableDetails.`ModifyUserID`
    ,   tb_userstimetabledetails.`ModifyDate`= now()
    where tb_userstimetabledetails.UserTimetableDetailsID=TimeTableDetails.UserTimetableDetailsID
  ;
  
  
    
END IF;

       begin
      
      INSERT INTO tb_userstimetabledetails
		  (UserTimetableID,UserID,TaskTypeID,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,InsertUserID,InsertDate,ModifyUserID,ModifyDate)
	
    select T.UserTimetableID, T.UserID,T.TaskTypeID,T.Monday,T.Tuesday,T.Wednesday,T.Thursday,T.Friday,T.Saturday,T.Sunday,T.InsertUserID,now(),T.ModifyUserID,now() from TusertimetableDetails as T
  where NOT EXISTS(
    SELECT * FROM tb_userstimetabledetails 
 WHERE tb_userstimetabledetails.UserID=T.UserID
   AND tb_userstimetabledetails.TaskTypeID=T.TaskTypeID
     );
       end;



Drop Table TusertimetableDetails;

END;
$$
DELIMITER ;

