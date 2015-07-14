DROP procedure IF EXISTS `AVZ_NOT_spUpdateNotificationStatus`;

DELIMITER $$

CREATE PROCEDURE AVZ_NOT_spUpdateNotificationStatus
(
 iNNotificationQueueId bigint,
 iNNotificationStatus tinyint
 )

begin

update tb_notificationqueue set NotificationStatus=iNNotificationStatus where NotificationQueueId=iNNotificationQueueId;
end;
