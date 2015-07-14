
drop procedure if exists avz_NOTGRP_spretrieveNotificationGroups;

delimiter $$

create procedure avz_NOTGRP_spretrieveNotificationGroups(
)
begin

Select 
NGRP.NotificationGroupID,NGRP.NotificationGroupName
from tb_notificationgroups NGRP;
end;