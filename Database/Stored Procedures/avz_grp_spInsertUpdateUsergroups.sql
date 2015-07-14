drop procedure if exists avz_grp_spInsertUpdateUsergroups;
DELIMITER $$

CREATE  PROCEDURE `avz_grp_spInsertUpdateUsergroups`(
iNuserGroupId bigint,
iNuserId bigint,
iNgroupId bigint,
iNisActive bool,
iNinsertUserId bigint,
iNinsertDate datetime,
iNmodifyUserId bigint,
iNmodifyDate datetime
)
begin
if iNuserGroupId!=0 then
begin
INSERT INTO `tb_usergroups`
(
`UserID`,
`GroupID`,
`IsActive`,
`InsertUserID`,
`InsertDate`,
`ModifyUserID`,
`ModifyDate`)
VALUES
(
iNuserId,
iNgroupId,
iNisActive,
iNinsertUserId,
iNinsertDate,
iNmodifyUserId,
iNmodifyDate);
end;

else
begin
update tb_usergroups
set 
`UserID`=iNuserId,
`GroupID`=iNgroupId,
`IsActive`=iNisActive,
`InsertUserID`=iNinsertUserId,
`InsertDate`=iNinsertDate,
`ModifyUserID`=iNmodifyUserId,
`ModifyDate`=iNmodifyDate
where `UserGroupID`=iNuserGroupId;
end;
end if;
end