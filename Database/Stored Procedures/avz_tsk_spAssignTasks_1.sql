#CALL avz_tsk_spAssignTasks_1();
drop procedure if exists avz_tsk_spAssignTasks_1;
DELIMITER //

CREATE PROCEDURE avz_tsk_spAssignTasks_1()
BEGIN

DECLARE pTaskID INT;
DECLARE pTaskTypeID INT;
DECLARE pWorkflowID INT;
DECLARE pMerchantID INT;
DECLARE pContractID INT;
DECLARE pUserID INT;
Declare passignUserid bigint;
Declare passignedWorkflowid bigint;
DECLARE _finished INTEGER DEFAULT 0;
declare _exists int;

DECLARE Tasks CURSOR FOR 
SELECT TaskID
,TaskTypeID
,WorkflowID
,MerchantID
,ContractID
FROM tb_tasks
WHERE statusID = 22001 -- unassigned
ORDER BY InsertDate, tasktypeid;

DECLARE CONTINUE HANDLER FOR NOT FOUND SET _finished = 1;
/*
Insert the list of unassigned users to the  UnAssignedUsers temporary table
*/

DROP TEMPORARY TABLE IF exists UnAssignedUsers;
CREATE TEMPORARY TABLE IF NOT exists UnAssignedUsers
(
UserId bigint,
WorkflowId bigint,
tasktypeid int,
FromTime int,
FromTime1 int,
FromTime2 int);

INSERT INTO UnAssignedUsers
SELECT DISTINCT
usr.id, pqusr.workflowid,pqusrdetails.taskTypeId,
hour(STR_TO_DATE( pqusr.SaturdayFromTime, '%l:%i %p' )),hour(now() ),
hour(STR_TO_DATE( pqusr.SaturdayToTime, '%l:%i %p' ))
FROM
tb_users usr
JOIN
tb_usergroups usrgrp ON usr.id = usrgrp.userid
JOIN
tb_groups grp ON grp.groupid = usrgrp.GroupID
JOIN
tb_grouprights grprights ON grprights.groupid = grp.groupid
JOIN
tb_userstimetable pqusr ON pqusr.userid = usr.id
JOIN
tb_userstimetabledetails pqusrdetails ON pqusr.UsersTimetableID = pqusrdetails.UserTimetableID
WHERE 
1 = (CASE WEEKDAY(now())
WHEN 6 THEN pqusr.IsSundayWork
WHEN 0 THEN pqusr.IsMondayWork
WHEN 1 THEN pqusr.IsTuesdayWork
WHEN 2 THEN pqusr.IsWednesdayWork
WHEN 3 THEN pqusr.IsThursdayWork
WHEN 4 THEN pqusr.IsFridayWork
WHEN 5 THEN pqusr.IsSaturdayWork
END)
AND 
hour(now()) >= (CASE WEEKDAY(now())
WHEN 6 THEN hour(STR_TO_DATE(pqusr.SundayFromTime, '%l:%i %p' ))
WHEN 0 THEN hour(STR_TO_DATE( pqusr.MondayFromTime, '%l:%i %p' ))
WHEN 1 THEN hour(STR_TO_DATE( pqusr.TuesdayFromTime, '%l:%i %p' ))
WHEN 2 THEN hour(STR_TO_DATE( pqusr.WednesdayFromTime, '%l:%i %p' ))
WHEN 3 THEN hour(STR_TO_DATE( pqusr.ThursdayFromTime, '%l:%i %p' ))
WHEN 4 THEN hour(STR_TO_DATE( pqusr.FridayFromTime, '%l:%i %p' ))
WHEN 5 THEN hour(STR_TO_DATE( pqusr.SaturdayFromTime, '%l:%i %p' ))
END) 
AND 
hour(now()) <=
(CASE WEEKDAY(now())
WHEN 6 THEN hour(STR_TO_DATE( pqusr.SundayToTime, '%l:%i %p' ))
WHEN 0 THEN hour(STR_TO_DATE( pqusr.MondayToTime, '%l:%i %p' ))
WHEN 1 THEN hour(STR_TO_DATE( pqusr.TuesdayToTime, '%l:%i %p' ))
WHEN 2 THEN hour(STR_TO_DATE( pqusr.WednesdayToTime, '%l:%i %p' ))
WHEN 3 THEN hour(STR_TO_DATE( pqusr.ThursdayToTime, '%l:%i %p' ))
WHEN 4 THEN hour(STR_TO_DATE( pqusr.FridayToTime, '%l:%i %p' ))
WHEN 5 THEN hour(STR_TO_DATE( pqusr.SaturdayToTime, '%l:%i %p' ))
END
);


/*
Open cursor
*/
Drop temporary table  IF exists Probables;
create temporary table Probables
(
userid bigint,
taskTypeid bigint,
assignedtasks bigint
);
insert into Probables
SELECT 
    assigneduserId, t.taskTypeId,COUNT(taskid) AS assignedtasks
   
FROM
    tb_tasks t JOIN UnAssignedUsers uu on t.AssignedUserId=uu.userid and t.TaskTypeId=uu.tasktypeid and t.WorkflowId=uu.workflowid
WHERE
    StatusId=22002 #and assigneduserId in (select userid from UnAssignedUsers) and tasktypeid in (select distinct tasktypeid from UnAssignedUsers where userid =t.assigneduserId)
GROUP BY assigneduserId,t.taskTypeId
order by assignedtasks,t.taskTypeId;

select userid from UnAssignedUsers;
Select 'Probables';
Select * from Probables;
# if there is no user to be assigned then take the list of the unassigned users and assign them irrespective of the time slot
#IF (SELECT COUNT(userid) FROM Probables)<=0 THEN 
insert into Probables(userid,tasktypeid)
SELECT DISTINCT
usr.id, pqusrdetails.tasktypeid#,pqusr.workflowid
FROM
tb_users usr
JOIN
tb_usergroups usrgrp ON usr.id = usrgrp.userid
JOIN
tb_groups grp ON grp.groupid = usrgrp.GroupID
JOIN
tb_grouprights grprights ON grprights.groupid = grp.groupid
JOIN
tb_userstimetable pqusr ON pqusr.userid = usr.id
JOIN
tb_userstimetabledetails pqusrdetails ON pqusr.UsersTimetableID = pqusrdetails.UserTimetableID  and (pqusrdetails.Sunday =1 OR pqusrdetails.Monday=1 or pqusrdetails.Tuesday=1
or pqusrdetails.Wednesday=1 or pqusrdetails.Thursday=1 or pqusrdetails.Friday=1 or pqusrdetails.Saturday=1 or pqusrdetails.Sunday=1)
WHERE
1 = (CASE WEEKDAY(now())
WHEN 6 THEN pqusr.IsSundayWork
WHEN 0 THEN pqusr.IsMondayWork
WHEN 1 THEN pqusr.IsTuesdayWork
WHEN 2 THEN pqusr.IsWednesdayWork
WHEN 3 THEN pqusr.IsThursdayWork
WHEN 4 THEN pqusr.IsFridayWork
WHEN 5 THEN pqusr.IsSaturdayWork
END)
AND usr.id in (select userid from UnAssignedUsers);

SELECT DISTINCT
usr.id, pqusrdetails.tasktypeid#,pqusr.workflowid
FROM
tb_users usr
JOIN
tb_usergroups usrgrp ON usr.id = usrgrp.userid
JOIN
tb_groups grp ON grp.groupid = usrgrp.GroupID
JOIN
tb_grouprights grprights ON grprights.groupid = grp.groupid
JOIN
tb_userstimetable pqusr ON pqusr.userid = usr.id
JOIN
tb_userstimetabledetails pqusrdetails ON pqusr.UsersTimetableID = pqusrdetails.UserTimetableID and (pqusrdetails.Sunday =1 OR pqusrdetails.Monday=1 or pqusrdetails.Tuesday=1
or pqusrdetails.Wednesday=1 or pqusrdetails.Thursday=1 or pqusrdetails.Friday=1  or pqusrdetails.Saturday=1 or pqusrdetails.Sunday=1)
WHERE
1 = (CASE WEEKDAY(now())
WHEN 6 THEN pqusr.IsSundayWork
WHEN 0 THEN pqusr.IsMondayWork
WHEN 1 THEN pqusr.IsTuesdayWork
WHEN 2 THEN pqusr.IsWednesdayWork
WHEN 3 THEN pqusr.IsThursdayWork
WHEN 4 THEN pqusr.IsFridayWork
WHEN 5 THEN pqusr.IsSaturdayWork
END)
AND usr.id in (select userid from UnAssignedUsers);

#END IF;

SELECT 
    *,'P'
  FROM
    Probables;

OPEN Tasks;
 
GETDATA: loop
fetch Tasks into pTaskID,pTaskTypeID,pWorkflowID,pMerchantID,pContractID;

#IF NO DATA IS FOUND THEN QUIT
IF _finished = 1 THEN 
LEAVE GETDATA;
END IF;
SET SQL_SAFE_UPDATES = 0;


/*
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Start manipulating the data in the tables
*/

set _exists = (SELECT EXISTS(
SELECT 
    UserId
 FROM
    Probables
LIMIT 1));

if(_exists=1) then
SELECT 
    UserId INTO pUserID
  FROM
    Probables
Where taskTypeid=pTaskTypeID
#order by assignedtasks
 limit 1;
 Select 'working1';

SELECT 
    *, 'Select'
  FROM
    Probables;
Select pTaskTypeID;

SELECT 
    UserId 
  FROM
    Probables
Where taskTypeid=pTaskTypeID
order by assignedtasks
 limit 1;
 
 Select 'working';

#Select pUserID as userIDAssign;

if(pUserID is not null) then
UPDATE tb_tasks 
SET 
    AssignedUserID = pUserID,
    AssignedDate = now(),
    ModifyDate = now(),
    ModifyUserID = 2,
    StatusID = 22002
WHERE
    TaskID = pTaskID;
end if;
Update Probables set assignedtasks = ifnull(assignedtasks,0)+1 where userid=pUserID;

/* DELETE FROM Probables 
WHERE
    UserId = pUserID; */

end if;

END LOOP GETDATA;

CLOSE Tasks;
Drop Table Probables;
Drop Table UnAssignedUsers;
END;
//

DELIMITER ;

