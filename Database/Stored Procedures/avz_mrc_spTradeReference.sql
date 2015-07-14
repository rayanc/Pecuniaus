drop procedure if exists avz_mrc_spTradeReference;
delimiter $$
create procedure avz_mrc_spTradeReference(iNprovidedXml text,iNmerchantId bigint,iNcontractId bigint)

BEGIN

	declare pRowIndex int unsigned default 1;   
	declare pRowCount int unsigned default 0;
	declare referenceid bigint;
	
	Create temporary table if not exists temptrade_ids
    (Id varchar(20), PRIMARY KEY (id)) ENGINE=MEMORY;

	truncate table temptrade_ids;

	set pRowCount  = extractValue(iNprovidedXml, 'count(//tradereference)');

	while pRowIndex <= pRowCount do
		set referenceid=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@referenceid'), ''),0);
			
		IF (referenceid=0) then 
			insert into tb_tradereferences
			(
				Name,
				phoneNumber,
				MerchantId,
				contractId
			)
			values
			(
				coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@referencename'), ''),0),
				coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@rphonenumber'), ''),0),
				iNmerchantId,
				iNcontractId
			);

			insert into temptrade_ids(id) values (last_insert_id());

		else
			update tb_tradereferences
			set 
			Name=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@referencename'), ''),0),
			phoneNumber=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@rphonenumber'), ''),0)
			where TradeRefId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@referenceid'), ''),0);

			insert into temptrade_ids(id) values (referenceid);

		end if;

		set pRowIndex = pRowIndex + 1;
	end while;

	delete from tb_tradereferences where MerchantId=iNmerchantId and TradeRefId not in (select id from temptrade_ids);

end$$
