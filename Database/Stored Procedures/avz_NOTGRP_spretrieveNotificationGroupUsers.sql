
drop procedure if exists avz_NOTGRP_spretrieveNotificationGroupUsers;

delimiter $$

create procedure avz_NOTGRP_spretrieveNotificationGroupUsers(
iNNotificationGroupID bigint
)
begin

Select 
NGRPUSR.NotificationGroupID,NGRPUSR.NotificationUserID,NGRPUSR.NotificationGroupUserID UserID
from tb_notificationgroupusers NGRPUSR Where NGRPUSR.NotificationGroupID=iNNotificationGroupID;
end;