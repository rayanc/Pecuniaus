drop PROCEDURE if exists avz_cnt_spDeclineContract;

delimiter $$

CREATE PROCEDURE `avz_cnt_spDeclineContract`(iNmerchantId bigint,iNcontractId bigint,iNdeclinereasonId bigint,iNworkFlowId bigint,iNdeclineNotes text, iNevaluateAgain bit, iNScreen varchar(200))
begin 
declare _merchantId bigint;
SELECT merchantId
INTO _merchantId FROM
    tb_contracts
WHERE
    contractid = iNcontractId;
start transaction;
UPDATE tb_contracts 
SET 
    statusid = 21002
WHERE
    contractId = iNcontractId;
# Add decline reason and notes 
insert into tb_declinereasons(contractId,DeclineReasonId,workFlowId,declineNotes) values (iNcontractId,iNdeclinereasonId,iNworkFlowId,iNdeclineNotes);
UPDATE tb_merchants 
SET 
    statusid = 10001, ReevaluateAgain=iNevaluateAgain
WHERE
    merchantid = iNmerchantId;

call avz_notes_spInsertNotes(540001,iNmerchantId,iNcontractId,iNdeclineNotes,iNworkFlowId, iNScreen);



if exists(select 1 from tb_merchants where merchantId=iNmerchantId) then
update tb_tasks set statusId=22005 where merchantId=iNmerchantId and contractId=iNcontractId;
else 
if exists(select 1 from tmp_tb_merchants where MerchantId_TMP=iNmerchantId) then
update tb_tasks set statusId=22005 where MerchantIDTMP=iNmerchantId;
end if;
end if;
commit;
end