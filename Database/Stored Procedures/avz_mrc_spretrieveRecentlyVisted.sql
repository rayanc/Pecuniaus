
drop procedure if exists avz_mrc_spretrieveRecentlyVisted;
DELIMITER $$ 
create procedure avz_mrc_spretrieveRecentlyVisted(iNuserId bigint)
begin
SELECT distinct
    mrc.merchantId,
    userId,
    dateVisited,   
    mrc.businessName,
    mrc.LegalName AS legalName,
    CONCAT(cnt.FirstName, ' ', cnt.LastName) AS merchantName   
FROM
    tb_recentlyvisited rv
        JOIN
    tb_merchants mrc ON mrc.merchantid = rv.merchantId
        LEFT JOIN
    tb_owners tbo ON tbo.ownerId = mrc.ownerId
        LEFT JOIN
    tb_contacts cnt ON cnt.contactid = tbo.contactid
        LEFT JOIN
    tb_tasks tsk ON tsk.merchantId = mrc.merchantid
        LEFT JOIN
    tb_tasktypes tsktyp ON tsktyp.tasktypeid = tsk.tasktypeid
   left join tb_workflows wrk on tsk.WorkflowId=wrk.WorkFlowId
where rv.userId=iNuserId  order by dateVisited desc limit 5;

end $$ 
delimiter $$ 

