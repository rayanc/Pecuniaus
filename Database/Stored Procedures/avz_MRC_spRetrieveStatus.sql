drop procedure if exists avz_MRC_spRetrieveStatus;

delimiter $$

create procedure avz_MRC_spRetrieveStatus
(iNstatusType varchar(200) )
begin 
SELECT StatusId as keyId,StatusName as description FROM Pecuniaus.lkp_tb_statuses where StatusTypeId=iNstatusType and IsActive=1;

end $$
delimiter $$
