drop procedure if exists azn_ren_spCompleteFinalValidation;
delimiter $$
#call azn_ren_spCompleteFunding(42,0,'',100,200,22,1,0,0)
CREATE PROCEDURE azn_ren_spCompleteFinalValidation
(
iNContractId int
)

BEGIN


declare _merchantId bigint;
SELECT merchantId
INTO _merchantId FROM
    tb_contracts
WHERE
    contractid = iNContractId;


update tb_tasks 
set 
    StatusId = 22004
where
		tasktypeId = 26
        and merchantId = _merchantId
		and contractId = iNContractId;

end 
$$
DELIMITER ;
