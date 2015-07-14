#drop procedure avz_gen_spRetrieveTaskStatus;
delimiter $$
CREATE PROCEDURE `avz_gen_spRetrieveTaskStatus`()
BEGIN
select StatusId as keyId, StatusName as description from lkp_tb_statuses where statustypeid='TSK';
END