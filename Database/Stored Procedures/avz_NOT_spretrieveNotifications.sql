
drop procedure if exists avz_NOT_spretrieveNotifications;

delimiter $$

create procedure avz_NOT_spretrieveNotifications(
)
begin

Select 
NotificationQueueId,NotificationFileName
from tb_notificationqueue where NotificationStatus in (1,3);
end;