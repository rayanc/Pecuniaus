-- ================================================
-- Name: AVZ_TSK_spAssignTasks
-- 
-- Description : Assigns all currently unassigned tasks. 
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 28-08-2014
-- ================================================
drop procedure if exists AVZ_TSK_spAssignTasks;
DELIMITER //

CREATE PROCEDURE AVZ_TSK_spAssignTasks
()
BEGIN

	DECLARE pTaskID INT;
	DECLARE pTaskTypeID INT;
	DECLARE pWorkflowID INT;
	DECLARE pMerchantID INT;
	DECLARE pContractID INT;
	DECLARE pUserID INT;

/*CREATE TEMPORARY TABLE Tasks (
    TaskID bigint, 
    TaskTypeID int,
	WorkflowID int,
	MerchantID int,
	ContractID int
);
	Insert Into Tasks
	SELECT TaskID
		,TaskTypeID
		,WorkflowID
		,MerchantID
		,ContractID
	FROM tb_tasks
	WHERE statusID = 22001 -- unassigned
	ORDER BY InsertDate;

	SET pTaskID = NULL;

	SELECT TaskID
			, TaskTypeID
			 INTO pTaskID, pTaskTypeID
		FROM Tasks limit 1;
		   
	WHILE pTaskID IS NOT NULL DO	
		/*SELECT UserID INTO pUserID
		FROM tb_PrequalUsers
		WHERE TaskTypeID = pTaskTypeID
		ORDER BY AssignedDate limit 1; 
set pUserID=2;

		UPDATE tb_tasks
		SET AssignedUserID = pUserID
			,AssignedDate = now()
			,ModifyDate = now()
			,ModifyUserID = 2
			,StatusID = 22002 -- Assigned            
		WHERE TaskID = pTaskID;

		UPDATE tb_PrequalUsers
		SET AssignedDate = now()
		WHERE UserID = pUserID;

		DELETE
		FROM tasks
		WHERE TaskID = pTaskID;

		SET pTaskID = NULL;
		SET pUserID = NULL;

		SELECT TaskID
			, TaskTypeID

			 INTO pTaskID, pTaskTypeID
		FROM Tasks limit 1; 
	END while;
Drop Table Tasks;*/

set pUserID=2;

		UPDATE tb_tasks
		SET AssignedUserID = pUserID
			,AssignedDate = now()
			,ModifyDate = now()
			,ModifyUserID = 2
			,StatusID = 22002 -- Assigned            
		WHERE StatusId = 22001;
END;
//

DELIMITER ;


