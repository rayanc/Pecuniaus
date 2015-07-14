drop procedure if exists avz_cc_spRetrieveOffers;
delimiter $$

create procedure avz_cc_spRetrieveOffers(iNmerchantId bigint, iNcontractId bigint)
begin
SELECT offerId,
    merchantId,
    contractId,
    turn,
    loanAmount,
    ownedAmount,
    proportion,
    retention,
    offerCreationDate,
    createdUserId as insertuserId,
    expirationDate   as offerexpirationDate,
	monthlypayment as monthlyPayment,
	yearly as yearly,
	salestaken as salestaken,
	case status
		when 190001 then true
	else false end as IsSelected,
    IsEmailSent
    FROM tb_offers
where merchantId=iNmerchantId and contractId=iNcontractId;




end;


