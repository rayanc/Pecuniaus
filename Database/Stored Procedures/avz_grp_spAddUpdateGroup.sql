Use pecuniaus;
drop procedure if exists avz_grp_spAddUpdateGroup;

delimiter $$
 
create procedure avz_grp_spAddUpdateGroup
(
iNGroupID int,
iNGroupName varchar(100),
iNGroupTypeID int,
iNisActive bool,
iNInsertUserID int,
iNModifyUserID int
)
begin

/*start transaction;*/
if(iNGroupID=0) then
begin
INSERT INTO `tb_groups`(`GroupName`,`GroupTypeID`,`IsActive`,`InsertUserID`,`InsertDate`,`ModifyUserID`,`ModifyDate`)
select iNGroupName,iNGroupTypeID,iNisActive,iNInsertUserID,now(),iNModifyUserID,now();
/*commit;*/
end;
else 
begin
if exists(select groupid from tb_groups where groupid=iNGroupID) then
begin
UPDATE `tb_groups`
SET 
`GroupName` = iNGroupName,
`IsActive` = iNisActive,
`ModifyUserID` = iNModifyUserID,
`ModifyDate` = now(),
`GroupTypeID` = iNGroupTypeID
WHERE `GroupID` = iNGroupID;
end;
end if;

end;
end if;
end;