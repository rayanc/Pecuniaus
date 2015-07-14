use Pecuniaus;
drop procedure if exists avz_grp_spAddUpdateGroupRights;
delimiter $$
create procedure avz_grp_spAddUpdateGroupRights
(
iNGroupRightID bigint,
iNGroupID int,
iNModuleID int,
iNRead bool,
iNWrite bool,
iNEdit bool,
iNIsActive bool,
iNInsertUserID int,
iNModifyUserID int
)
begin
if(iNGroupRightID=0) then
begin
INSERT INTO `pecuniaus`.`tb_grouprights`
(`GroupRightID`,`GroupID`,`ModuleID`,`Read`,`Write`,`Edit`,`IsActive`,`InsertUserID`,
`InsertDate`,`ModifyUserID`,`ModifyDate`)
VALUES(iNGroupID,iNModuleID,iNRead,iNWrite,iNEdit,iNIsActive,iNInsertUserID,now(),iNModifyUserID,Now());
end;
else 
begin
UPDATE `pecuniaus`.`tb_grouprights`
SET
`GroupID` = iNGroupID,
`ModuleID` = iNModuleID,
`Read` = iNRead,
`Write` = iNWrite,
`Edit` = iNEdit,
`IsActive` = iNsActive,
`InsertUserID` = iNInsertUserID,
`InsertDate` = now(),
`ModifyUserID` = iNModifyUserID,
`ModifyDate` = now()
WHERE `GroupRightID` = iNGroupRightID;
end;
end if;
end;

