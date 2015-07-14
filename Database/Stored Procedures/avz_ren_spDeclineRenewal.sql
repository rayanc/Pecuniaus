drop procedure if exists avz_ren_spDeclineRenewal;
delimiter $$ 
create procedure avz_ren_spDeclineRenewal (iNcontractId bigint,iNdeclinereasonId bigint,iNworkFlowId bigint,iNdeclineNotes text)
begin 

declare DeclineReasonId bigint;
declare _merchantId bigint;

start transaction;

SELECT _merchantId =  merchantId
INTO _merchantId FROM
    tb_contracts
WHERE
    contractid = iNcontractId;
start transaction;

#update Contract
UPDATE tb_contracts SET     statusid = 21002 WHERE     contractId = iNcontractId;

# Add decline reason and notes 
Insert into tb_declinereasons(contractId,DeclineReasonId,workFlowId,declineNotes) 
values (iNcontractId,iNdeclinereasonId,iNworkFlowId,iNdeclineNotes);

#Set DeclineReasonId = LAST_INSERT_ID();

#UPDATE tb_renewalslist
#SET DeclineReasonID =  DeclineReasonId
#WHERE
#ContractID=iNcontractId;


commit;
end $$ 

delimiter $$ 