

drop procedure if exists avz_usr_spRetrieveTaskAssignmentUsers;

delimiter $$

create procedure avz_usr_spRetrieveTaskAssignmentUsers
(
iNUserID bigint
)
begin

Declare iNGID bigint;

Select GroupID INTO iNGID from tb_usergroups UG
inner join tb_users USR on USR.ID=UG.UserID
Where USR.ID=iNUserID;

Select USR.ID keyId,USR.UserName description from tb_usergroups UG
inner join tb_users USR on USR.ID=UG.UserID
Where GroupID=iNGID and USR.ID!=iNUserID;
end;
