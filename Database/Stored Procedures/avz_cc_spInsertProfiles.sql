delimiter $$
create procedure avz_cc_spInsertProfiles
(
iNmerchantId bigint,iNprocessorId bigint)

begin 

INSERT INTO `tb_creditcardprofiles`
(`CreditCardProfileId`,
`MerchantId`,
`ProcessorId`)
VALUES
(iNmerchantId,iNprocessorId);

end $$
delimiter $$ 
