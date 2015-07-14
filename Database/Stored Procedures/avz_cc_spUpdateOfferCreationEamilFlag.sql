
drop procedure if exists avz_cc_spUpdateOfferCreationEamilFlag;
delimiter $$

create procedure avz_cc_spUpdateOfferCreationEamilFlag
( 
	iNIsEmailSent bit, 
	iNmerchantId bigint, 
	iNcontractId bigint
)
begin
	Update tb_contracts set 
		IsOffersSent = iNIsEmailSent
		where merchantId=iNmerchantId and contractId=iNcontractId;

end;

