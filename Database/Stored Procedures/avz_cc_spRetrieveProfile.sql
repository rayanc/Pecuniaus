delimiter $$ 
create procedure avz_cc_spRetrieveProfile
(iNmerchantId bigint)
begin
SELECT creditCardProfileId,
    `merchantId,
    `processorId
FROM `tb_creditcardprofiles`
where merchantid=iNmerchantid;

end $$
delimiter $$
