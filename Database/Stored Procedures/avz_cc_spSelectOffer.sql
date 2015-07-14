drop procedure if exists avz_cc_spSelectOffer;
delimiter $$

create procedure avz_cc_spSelectOffer
( iNofferId bigint,iNmerchantId bigint,iNcontractId bigint)

begin 
update tb_offers set status=190002 where merchantid=iNmerchantId and contractid=iNcontractId;

UPDATE tb_offers 
SET 
    offerAcceptanceDate = NOW(),
    status=190001
WHERE
    offerId = iNofferId;

update tb_contracts set 
LoanedAmount=(select LoanAmount from tb_offers where offerId = iNofferId)
, ownedAmount=(select ownedAmount from tb_offers where offerId = iNofferId)
, Turn=(select Turn from tb_offers where offerId = iNofferId)
, CCAverageOffer= (select avgmccv from tb_merchants where merchantId=iNmerchantId limit 1)
, AverageMCCV =(select avgmccv from tb_merchants where merchantId=iNmerchantId limit 1)

where contractId =iNcontractId;

end;
