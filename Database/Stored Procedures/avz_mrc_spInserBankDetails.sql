drop procedure if exists avz_mrc_spInserBankDetails;
delimiter $$
create procedure avz_mrc_spInserBankDetails(
	iNprovidedXml text,
	iNbankid bigint,
	iNaccountnumber varchar(200),
	iNaccountname varchar(200),
	iNmerchantId bigint,
	iNcontractId bigint,
	iNbankcode varchar(50)
)

BEGIN

	declare bankdetailsid bigint;
	declare pRowIndex int unsigned default 1;   
	declare pRowCount int unsigned default 0;
	declare bstatementid bigint;

	if(iNcontractId > 0) then
		


		Create temporary table if not exists tempBank_ids
		(Id varchar(20), PRIMARY KEY (id)) ENGINE=MEMORY;

		IF exists(select 1 from tb_bankdetails where merchantId=iNmerchantId and contractId=iNcontractId) = 1 then 

			select BankDetailId into bankdetailsid from tb_bankdetails where merchantId=iNmerchantId and contractId=iNcontractId;

			update tb_bankdetails set accountnumber =iNaccountnumber,AccountName= iNaccountname, BankCode=iNbankcode, BankId=iNbankid 
			where merchantId=iNmerchantId and contractId=iNcontractId;
		else
			insert into tb_bankdetails(bankid,accountnumber, BankCode, merchantId,contractId,AccountName) 
			values (iNbankid,iNaccountnumber,iNbankcode,iNmerchantId,iNcontractId,iNaccountname);
			set bankdetailsid=LAST_INSERT_ID();   
		end if;

		TRUNCATE TABLE tempBank_ids;
	 
		set pRowIndex=1;
		set pRowCount=0;
		set pRowCount  = extractValue(iNprovidedXml, 'count(//bankstatement)');
		while pRowIndex <= pRowCount do

			set bstatementid=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@statementid'), ''),0);
			
			IF (bstatementid=0) then 
				insert into tb_BankStatements
				(
					BankDetailId,
					StatementMonthId,
					StatementYear,
					amount
				)
				values
				(
					bankdetailsid,
					coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@statementmonthid'), ''),0),
					coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@statementyear'), ''),0),
					coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@bsamount'), ''),0)
				);
				insert into tempBank_ids(id) values (last_insert_id());
			else
				update tb_BankStatements
				set 
					StatementMonthId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@statementmonthid'), ''),0),
					StatementYear=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@statementyear'), ''),0),
					amount=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@bsamount'), ''),0)
					where StatementId=bstatementid;
		
			insert into tempBank_ids(id) values (bstatementid);
			
			end if;

			set pRowIndex = pRowIndex + 1;
		end while;
	   

		delete from tb_BankStatements where StatementId not in (select id from tempBank_ids);
	end if;
end$$
