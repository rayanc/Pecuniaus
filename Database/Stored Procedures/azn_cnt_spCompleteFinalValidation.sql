drop procedure if exists azn_cnt_spCompleteFinalValidation;
delimiter $$
#call azn_ren_spCompleteFunding(42,0,'',100,200,22,1,0,0)
CREATE PROCEDURE azn_cnt_spCompleteFinalValidation 
(
iNContractId int,
iNAction varchar(50)
)

BEGIN


declare _merchantId bigint;
SELECT merchantId
INTO _merchantId FROM
    tb_contracts
WHERE
    contractid = iNContractId;


if (iNAction = 'complete')then 
update tb_tasks 
set StatusId = 22004
where
		tasktypeId = 18
        and merchantId = _merchantId
		and contractId = iNContractId;
end if;
end 
$$
DELIMITER ;
