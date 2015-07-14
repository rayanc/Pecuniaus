-- ================================================
-- Name: AVZ_COL_spAssignCollections
-- 
-- Description : To assign defaulter contracts and merchants to Collectors
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
drop procedure if exists AVZ_COL_spAssignCollections;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spAssignCollections
()
BEGIN	


DECLARE pCollectionId bigint;
DECLARE pCollectorId bigint;

CREATE TEMPORARY TABLE Defaulters
(CollectionId bigint primary key);

insert into Defaulters
	SELECT CollectionId
		FROM tb_collections
		WHERE (AssignedUserId is null or AssignedUserId = 0 or AssignedUserId ='')
		and StatusId <> 180008; 
-- select * from Defaulters;
/*
Insert the list of unassigned collectors to the  UnAssignedCollectors temporary table
*/

CREATE TEMPORARY TABLE UnAssignedCollectors
(
CollectorId bigint primary key,
FromTime int,
FromTime1 int,
FromTime2 int);

INSERT INTO UnAssignedCollectors
SELECT DISTINCT
cts.CollectorId,
hour(STR_TO_DATE( pqusr.SaturdayFromTime, '%l:%i %p' )),hour(now() ),
hour(STR_TO_DATE( pqusr.SaturdayToTime, '%l:%i %p' ))
FROM
tb_collectors cts
JOIN
tb_userstimetable pqusr ON pqusr.userid = cts.userid
WHERE
1 = (CASE WEEKDAY(NOW())
WHEN 6 THEN pqusr.IsSundayWork
WHEN 0 THEN pqusr.IsMondayWork
WHEN 1 THEN pqusr.IsTuesdayWork
WHEN 2 THEN pqusr.IsWednesdayWork
WHEN 3 THEN pqusr.IsThursdayWork
WHEN 4 THEN pqusr.IsFridayWork
WHEN 5 THEN pqusr.IsSundayWork
END)
AND 
hour(NOW()) >= (CASE WEEKDAY(NOW())
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
(CASE WEEKDAY(NOW())
WHEN 6 THEN hour(STR_TO_DATE( pqusr.SundayToTime, '%l:%i %p' ))
WHEN 0 THEN hour(STR_TO_DATE( pqusr.MondayToTime, '%l:%i %p' ))
WHEN 1 THEN hour(STR_TO_DATE( pqusr.TuesdayToTime, '%l:%i %p' ))
WHEN 2 THEN hour(STR_TO_DATE( pqusr.WednesdayToTime, '%l:%i %p' ))
WHEN 3 THEN hour(STR_TO_DATE( pqusr.ThursdayToTime, '%l:%i %p' ))
WHEN 4 THEN hour(STR_TO_DATE( pqusr.FridayToTime, '%l:%i %p' ))
WHEN 5 THEN hour(STR_TO_DATE( pqusr.SaturdayToTime, '%l:%i %p' ))
END
) order by cts.AssignedDate;

-- select * from UnAssignedCollectors;

SET pCollectionId = NULL;

	SELECT 
		CollectionId into pCollectionId
		FROM Defaulters limit 1;
		   
	WHILE pCollectionId IS NOT NULL DO	
		SELECT CollectorId INTO pCollectorId
		FROM UnAssignedCollectors
		limit 1; 

		-- insert into  tb_assignedcollections(CollectorID, ContractId, MerchantId, AssignedDate,ModifyDate)
		-- values (pCollectorId,pContractId,pMerchantId,now(),now());

		update tb_collections 
		set AssignedUserId=pCollectorId,
			AssignedDate = now()
		where CollectionId=pCollectionId;
		
		UPDATE tb_collectors
		SET  AssignedDate = now()			
		WHERE CollectorID = pCollectorId;		

		DELETE
		FROM Defaulters
		WHERE CollectionId = pCollectionId;

		DELETE
		FROM UnAssignedCollectors
		WHERE CollectorId = pCollectorId;

		SET pCollectionId = NULL;
		SET pCollectorId = NULL;

		SELECT 
		CollectionId into pCollectionId
		FROM Defaulters limit 1;

	END while;
Drop Table Defaulters;
Drop Table UnAssignedCollectors;

end