DROP procedure IF EXISTS `AVZ_NOT_spAddNotification`;

DELIMITER $$

CREATE PROCEDURE AVZ_NOT_spAddNotification
(
 iNNotificationFileName varchar(1000)
 )

begin
insert into tb_notificationqueue(NotificationFileName,NotificationStatus)
Values(iNNotificationFileName,1);
end;
