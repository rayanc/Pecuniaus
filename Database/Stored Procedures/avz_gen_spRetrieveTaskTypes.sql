drop procedure if exists avz_gen_spRetrieveTaskTypes;
delimiter $$
CREATE PROCEDURE avz_gen_spRetrieveTaskTypes
(iNworkflowId bigint)
BEGIN
select tasktypeId as keyId, taskname as description from tb_tasktypes;

END
