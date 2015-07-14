drop procedure if exists avz_mrc_spInsertAffiliation;
Delimiter $$
create procedure avz_mrc_spInsertAffiliation(iNmerchantId bigint,iNRNCNumber varchar(20),iNOwnerPassport varchar(20))
Begin
	declare tCount bigint;
    select count(*) into tCount from tb_merchantaffiliations where MerchantID=iNmerchantId and (RNCNumber=iNRNCNumber or OwnerID=iNOwnerPassport);

    if(tCount = 0) then
	begin
		insert into tb_merchantaffiliations
		(
		MerchantID,
		RNCNumber,
		OwnerID,
		AffiliationNumber
		)
		values
		(
		iNmerchantId,
		iNRNCNumber,
		iNOwnerPassport,
		FLOOR(RAND() * 999999)
		);
		end;
	end if;
end;


