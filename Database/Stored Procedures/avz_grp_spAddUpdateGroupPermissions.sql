-- ================================================
-- Name: avz_grp_spAddUpdateGroupPermissions
-- 
-- Description : To add update permissions for groups
-- 
-- Parameters : iNGroupRightsXml 
-- 
-- Author  : Amit Kumar
-- 
-- Creation Date : 15-11-2014
-- ================================================
Use Pecuniaus;
Drop procedure if exists avz_grp_spAddUpdateGroupPermissions;

DELIMITER $$
CREATE PROCEDURE avz_grp_spAddUpdateGroupPermissions
(iNGroupRightsXml text 
)
Begin
	declare  i int unsigned default 1;   
    declare pRowCount int unsigned;
	declare uGroupRightID int unsigned default 0;   
	declare uGroupID int unsigned default 0;   
	declare uModuleID int unsigned default 0;   
	declare uPageId int unsigned default 0;  
	declare uRead tinyint unsigned default 0;   
	declare uWrite tinyint unsigned default 0;   
	declare uEdit tinyint unsigned default 0;   
	declare uIsActive tinyint unsigned default 0;   
	declare uInsertUserID bigint(20) unsigned default 0;   
	declare uModifyUserID bigint(20) unsigned default 0;   

-- calculate the number of row elements.   
set pRowCount  = extractValue(iNGroupRightsXml, 'count(/GroupRights/Right)');
set i=1;    
 while  i <= pRowCount do
    
    set uGroupRightID=   ExtractValue(iNGroupRightsXml, '//Right[$i]/pGroupRightID');
    set uGroupID=    ExtractValue(iNGroupRightsXml,   '//Right[$i]/pGroupID');
    set uModuleID=    ExtractValue(iNGroupRightsXml,   '//Right[$i]/pModuleID');
	set uRead=   case ExtractValue(iNGroupRightsXml,   '//Right[$i]/pRead') when 'true' then 1 when 'false' then 0 end;
	set uWrite=   case ExtractValue(iNGroupRightsXml,   '//Right[$i]/pWrite') when 'true' then 1 when 'false' then 0 end;
    set uEdit=   case ExtractValue(iNGroupRightsXml,   '//Right[$i]/pEdit') when 'true' then 1 when 'false' then 0 end;
    set uIsActive=   case ExtractValue(iNGroupRightsXml,   '//Right[$i]/pIsActive') when 'true' then 1 when 'false' then 0 end;
    set uInsertUserId=   ExtractValue(iNGroupRightsXml,   '//Right[$i]/pInsertUserId');
    set uModifyUserId=   ExtractValue(iNGroupRightsXml,   '//Right[$i]/pModifyUserId');
	set uPageId=   ExtractValue(iNGroupRightsXml,   '//Right[$i]/pPageId');  
     
IF (uGroupRightID=0 ) THEN

	   INSERT INTO tb_grouprights (GroupID,ModuleID,`Read`,`Write`,Edit,IsActive,InsertUserID,InsertDate,ModifyUserID,ModifyDate,PageId)
		values(uGroupID,uModuleID,uRead,uWrite,uEdit,uIsActive,uInsertUserId,DATE_FORMAT(now(),'%Y-%m-%d'),uModifyUserId,DATE_FORMAT(now(),'%Y-%m-%d'),uPageId);
	
ELSE

		    SET SQL_SAFE_UPDATES=0;

			UPDATE tb_grouprights			
			set `Read`=uRead
			,   `Write`=uWrite
			,   `Edit`=uEdit
			,   `IsActive`=uIsActive			
			,   `ModifyUserID`=uModifyUserId
			,   `ModifyDate`= DATE_FORMAT(now(),'%Y-%m-%d')
		
     		where GroupRightID =uGroupRightID;
    
    
END IF;
	set i=i+1;
	
end while;

END;
$$
DELIMITER ;

