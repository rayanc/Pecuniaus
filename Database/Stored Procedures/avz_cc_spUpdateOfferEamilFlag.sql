
drop procedure if exists avz_cc_spUpdateOfferEamilFlag;
delimiter $$

create procedure avz_cc_spUpdateOfferEamilFlag
( 
	iNIsEmailSent bit, 
    iNofferId bigint,
	iNmerchantId bigint, 
	iNcontractId bigint
)
begin
	Update tb_offers set 
		IsEmailSent = iNIsEmailSent
		where offerid=iNofferId and merchantId=iNmerchantId and contractId=iNcontractId;

end;

