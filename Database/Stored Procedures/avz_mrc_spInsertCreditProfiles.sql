drop procedure if exists avz_mrc_spInsertCreditProfiles;
delimiter $$
CREATE PROCEDURE `avz_mrc_spInsertCreditProfiles`(iNmerchantId bigint,iNprofileXml text,iNiscompleted smallint,iNcontractId bigint)
BEGIN

    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned;  
    declare _creditcardActivityId bigint default 0;
    declare _isactive smallint default 1;
	declare pMonthsCount int unsigned default 0;
    
    -- calculate the number of row elements.   
	set pRowCount  = extractValue(iNprofileXml, 'count(//monthlyprofile)');
    
    while pRowIndex <= pRowCount do
    
		set _creditcardActivityId=coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@creditcardActivityId'), ''),0);
		set  _isactive=coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@isactive'), ''),1);

		if exists (select 1 from  tb_CreditCardVolumes where id=_creditcardActivityId)=0 then 
          
		begin 

		   insert into  tb_CreditCardVolumes(MerchantId,processorId, TotalAmount,TotalTickets,Year,Month,contractid)
		   values(iNmerchantId,coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@processorId'), ''),0)
				, coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@totalAmount'), ''),0)
				, coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@totalTickets'), ''),0)
				,coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@year'), ''),0)
				,coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@month'), ''),0),iNcontractId);
 
		insert into  tb_creditcardactivity(MerchantId,processorId,processormerchantid, AcctivityTypeId, TotalAmount,TotalTickets,Year,Month,contractid)
		   values(iNmerchantId,coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@processorId'), ''),0),
				(Select processornumber from tb_processor where ProcessorId=coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@processorId'), ''),0))
				,70, coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@totalAmount'), ''),0)
				, coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@totalTickets'), ''),0)
				,coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@year'), ''),0)
				,coalesce(nullif(extractvalue(iNprofileXml, '//child::*[$pRowIndex]/@month'), ''),0),iNcontractId);
		end;
		else
  
			if(_isactive=0) then 
				DELETE FROM tb_CreditCardVolumes WHERE id = _creditcardActivityId;
			else 
	  
				UPDATE tb_CreditCardVolumes 
				SET 
					MerchantId = iNmerchantId,
					processorId = COALESCE(NULLIF(EXTRACTVALUE(iNprofileXml,
										'//child::*[$pRowIndex]/@processorId'),
								''),
						0),
					totalAmount = COALESCE(NULLIF(EXTRACTVALUE(iNprofileXml,
										'//child::*[$pRowIndex]/@totalAmount'),
								''),
						0),
					totaltickets = COALESCE(NULLIF(EXTRACTVALUE(iNprofileXml,
										'//child::*[$pRowIndex]/@totalTickets'),
								''),
						0),
					Year = COALESCE(NULLIF(EXTRACTVALUE(iNprofileXml,
										'//child::*[$pRowIndex]/@year'),
								''),
						0),
					Month = COALESCE(NULLIF(EXTRACTVALUE(iNprofileXml,
										'//child::*[$pRowIndex]/@month'),
								''),
						0),
						ContractId=iNcontractId
				WHERE
					id = _creditcardActivityId;

			end if;
		end if;
  
		set pRowIndex = pRowIndex + 1;
		set _creditcardActivityId=0;
		set _isactive=1;

	 end while;



 
	select count( *) into pMonthsCount from (
		select month FROM
				tb_CreditCardVolumes
			WHERE
				merchantId =  iNmerchantId
                group by month
		) as s;
                
	select pMonthsCount ;
	UPDATE tb_merchants 
	SET 
		avgmccv = (SELECT 
            (SUM(totalAmount) / pMonthsCount ) AS avgmmc
			FROM
				tb_CreditCardVolumes
			WHERE
				merchantId = iNmerchantId)
	WHERE
		merchantId = iNmerchantId;

	if(iNiscompleted=1) then
		call avz_tsk_spCompleteTask(iNmerchantId,4,1,iNcontractId);
	end if;

END