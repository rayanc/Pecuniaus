drop procedure if exists avz_grp_spRetrieveModulesWithRights;
delimiter $$
create procedure avz_grp_spRetrieveModulesWithRights
(
 iNModuleID int,
 iNGroupID int
)
begin
select grp.GroupID, grp.ModuleID, grp.PageID, modu.modulename as ModuleName, grp.`Read`, grp.`Write`, grp.`Edit`, grp.GroupRightID
from tb_grouprights grp
inner join lkp_tb_modules modu on modu.moduleid=grp.PageID
where grp.moduleid=iNModuleID and grp.GroupId=iNGroupID

union

select '0' as GroupID ,  modu.parentmodule as ModuleID, modu.moduleid as PageID,modu.ModuleName, 0 as `Read`,0 as `Write`,0 as `Edit` , 0 as GroupRightID
from lkp_tb_modules modu where  modu.moduleid not in(select pageid from tb_grouprights where moduleid=2 and GroupId=9)
and modu.parentmodule=iNModuleID;

end;