drop procedure if exists avz_mrc_spSaveDataEntryTask;
delimiter $$
CREATE  PROCEDURE `avz_mrc_spSaveDataEntryTask`(iNprovidedXml text, iNmerchantId bigint, iNisCompleted smallint)
BEGIN
    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned default 0;
    declare pAddressId int unsigned default 0;
    declare pContactId int unsigned default 0;
    declare pMerchantId int unsigned default 0;	
    declare loanAmount double unsigned default 0.0;
	declare pOwnerId int unsigned default 0;
	declare pOwnerIds varchar(50) default null;
	declare pcontractId bigint;
	declare pprocessorId bigint;
    declare pautorized bit(1) default 0;
	declare pRNCNumber varchar(20);
    declare pOwnerPassport varchar(20);
	declare iMerchantStatus int;
    Create temporary table if not exists temp_ids
    (Id varchar(20), PRIMARY KEY (id)) ENGINE=MEMORY;
	
set pRNCNumber= COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mrnc'),
                    ''),
            0);

Select StatusId into iMerchantStatus from tb_merchants where merchantId=iNmerchantId;

set sql_safe_updates=0;
  	UPDATE `tb_merchants` 
SET 
    `rncNumber` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mrnc'),
                    ''),
            0),
    `businessName` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mbusinessName'),
                    ''),
            0),
    `legalName` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mlegalName'),
                    ''),
            0),
    `businessStartDate` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mbusinessStartDate'),
                    ''),
            0), 
    `businessWebSite` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mbusinessWebSite'),
                    ''),
            0), 
    `businessTypeId` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mbusinessTypeId'),
                    ''),
            0),
    `rentFlag` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mrentFlag'),
                    ''),
            0),
    `RentedAmount` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mrentAmount'),
                    ''),
            0),
    `industryTypeId` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mindustryTypeId'),
                    ''),
            0),
    `cNetProcessorId` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mcNetProcessorId'),
                    ''),
            0),
    `vNetProcessoIdd` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mvNetProcessoId'),
                    ''),
            0),
    `grossannualSales` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mannualSales'),
                    ''),
            0),
	`RentFlag` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mpropertyType'),
                    ''),
            0),
	`salesRepId`=COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@mprimarySalesRepId'),
                    ''),
            0),
	`isynced`=0,
	`StatusId`=case when iMerchantStatus in (10002, 10003, 10004, 10005, 10006,10009) then StatusId else 10007 end

WHERE
    merchantId = iNmerchantId;

call avz_mrc_spUpdateLandlordInformation(iNprovidedXml,iNmerchantId);

/* Update Merchant Address Details  */
if coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@maddressId'), ''),0) =0 then

	INSERT INTO `tb_addresses`
		(
		 `AddressTypeId`
		,`Address1`
		,`Address2`
		,`Country`
		,`City`
		,`State`
		,`ZipCode`
		,`Phone1`
		,`Phone2`
		,`Phone3`
		,`email1`
		,`email2`
		,`email3`
		,`InsertUserId`
		,`InsertDate`)
		VALUES
		(		
		 1,
		 coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@maddressLine1'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@maddressLine2'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mcountry'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mcity'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mstateId'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mzipcode'), ''),0)
		 ,nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mphone1'), '')
		 ,nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mphone2'), '')
		 ,NULL
		 ,nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@memail'), '')
		 ,NULL
		 ,NULL
		 ,1
		 ,NOW());
		set pAddressId=LAST_INSERT_ID();
		update tb_merchants set AddressId=pAddressId where merchantId = iNmerchantId;
	else

		update tb_addresses set Address1=coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@maddressLine1'), ''),0)
				, Address2 =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@maddressLine2'), ''),0)
				, Country =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mcountry'), ''),0)
				, City =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mcity'), ''),0)
				, State= coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mstateId'), ''),0)
				, ZipCode =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mzipcode'), ''),0)
				, Phone1 = nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mphone1'), '')
				, Phone2 = nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@mphone2'), '')
				, Phone3 =null
				, email1 = nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@memail'), '')
				, email2 =null
				, email3= null
		where AddressId= coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo//@maddressId'), ''),0);

end if;
/* End merchant Address Details  */


	
	-- Iterate through rows of Owners

	set pRowCount  = extractValue(iNprovidedXml, 'count(//owner)');
    set pRowIndex=1;
    while pRowIndex <= pRowCount do

		set pOwnerId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerId'), ''),0);
		if(pOwnerId !=0) then
			if(pRowIndex=1) then
				set pOwnerIds = concat("'",pOwnerId, "'");
			else
				set pOwnerIds = concat(pOwnerIds, "," ,"'",pOwnerId, "'");
			end if;	
		end if;
		set pRowIndex = pRowIndex + 1;
     end while;

	set pOwnerId=0;
	set pRowCount=0;

	set pAddressId=0;
	-- Iterate through rows of Owners
	TRUNCATE TABLE temp_ids;
	set pRowCount  = extractValue(iNprovidedXml, 'count(//owner)');
    set pRowIndex=1;
    while pRowIndex <= pRowCount do
		if coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressId'), ''),0) =0 then
			-- Enter the details in the address table     
			INSERT INTO `tb_addresses`
			(
				 `AddressTypeId`
				,`Address1`
				,`Address2`
				,`Country`
				,`City`
				,`State`
				,`ZipCode`
				,`Phone1`
				,`Phone2`
				,`Phone3`
				,`email1`
				,`email2`
				,`email3`
				,`InsertUserId`
				,`InsertDate`)
			VALUES
			(		
				1,
				 coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressLine1'), ''),0)
				 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressLine2'), ''),0)
				 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@country'), ''),0)
				 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@city'), ''),0)
				 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@stateId'), ''),0)
				 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@zip'), ''),0)
				 , nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone1'), '')
				 ,nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone2'), '')
				 ,NULL
				 ,nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@email'), '')
				 ,NULL
				 ,NULL
				 ,1
				 ,NOW()
			);
			Set pAddressId = LAST_INSERT_ID();
		else
			update tb_addresses set Address1=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressLine1'), ''),0)
			, Address2 =coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressLine2'), ''),0)
			, Country =coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@country'), ''),0)
			, City =coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@city'), ''),0)
			, State= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@stateId'), ''),0)
			, ZipCode =coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@zip'), ''),0)
			, Phone1 =nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone1'), '')
			, Phone2 =nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone2'), '')
			, Phone3 =null
			, email1 = nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@email'), '')
			, email2 =null
			, email3= null
			where AddressId= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressId'), ''),0);
			Set pAddressId =coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressId'), ''),0);
		end if;	

		set pContactId =coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@contactId'), ''),0);
		set pOwnerPassport=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@passportNumber'), ''),0);
		if (pContactId =0 )then

			-- Enter the details in the Contact Table
			INSERT INTO `tb_contacts`
			(`ContactTypeId`
			,`JobTitle`
			,`FirstName`
			,`MiddleName`
			,`LastName`
			,`AddressId1`
			,`DateOfbirth`
			,`SSN`,`merchantid`,`PassportNbr`)
			VALUES
			( 1
			,''
			,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerFirstName'), ''),0)
			,''
			,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerLastName'), ''),0)
			,pAddressId
			,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerDOB'), ''),0) 
			,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ssn'), ''),0),iNmerchantId, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@passportNumber'), ''),0));
			 
			Set pContactId = LAST_INSERT_ID();
		
		else

			update tb_contacts set 
				JobTitle=''
				,FirstName=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerFirstName'), ''),0)
				,MiddleName=''
				,LastName=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerLastName'), ''),0)
				,DateOfbirth=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerDOB'), ''),0) 
				,SSN=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ssn'), ''),0)
				, PassportNbr=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@passportNumber'), ''),0)
			where ContactId =pContactId;

		end if;

		set pOwnerId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerId'), ''),0);
		if(coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@authorised'), ''),0)='True') then
			set pautorized=1;
		else
			set pautorized=0;
		end if;
		
		if (pOwnerId =0) then
			-- Enter details in the owner's table
			INSERT INTO `tb_owners`
			(`MerchantId`
			,`BusinessStartDate`
			,`RentFlag`
			,`RentAmount`
			,`MortgageAmount`
			,`InsertUserId`
			,`InsertDate`
			,`ModifyUser`
			,`ModifyDate`
			,`ContactId`,
			`IsAuthorized`)
			VALUES
			(iNmerchantId
			,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
								'//merchantinfo/merchantbasicinfo/@mbusinessStartDate'),
						''))
			,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mrentFlag'), ''),0)
			,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mrentAmount'), ''),0)
			,NULL
			,1
			,NOW()
			,1
			,NOW()
			,pContactId
			,pautorized
			);
	 
			Set pOwnerId = LAST_INSERT_ID();    
			update tb_merchants set OwnerId=pOwnerId where merchantId=iNmerchantId;
			insert into temp_ids (id) values (pOwnerId);
		else
			update tb_owners set
				BusinessStartDate=COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
									'//merchantinfo/merchantbasicinfo/@mbusinessStartDate'),
							''))
				,RentFlag =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mrentFlag'), ''),0)
				,RentAmount=coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@mrentAmount'), ''),0)		
				,ModifyUser =1
				,ModifyDate=NOW(),
				IsAuthorized =pautorized,
				ContactId=pContactId
				where OwnerId= pOwnerId;

				update tb_merchants set OwnerId=pOwnerId where merchantId=iNmerchantId;
			insert into temp_ids (id) values (pOwnerId);
		end if;

		set pOwnerId=0;
		Set pAddressId = 0;
		Set pContactId = 0;
		set pRowIndex = pRowIndex + 1;
	end while;
	set pRowCount = 0;
	set pRowIndex = 1;


	/* Delete from owners, contact and address table */		
	delete from tb_addresses where AddressId in (select cont.AddressId1 from tb_contacts cont
	inner join tb_owners own on own.ContactId=cont.ContactId where own.OwnerId not in(select id from temp_ids ) and own.merchantId=iNmerchantId);

	delete from tb_contacts where ContactID in (select own.ContactId 
	from tb_owners own where own.OwnerId not in(select id from temp_ids ) and own.merchantId=iNmerchantId);

	delete from tb_owners where OwnerId not in(select id from temp_ids ) and merchantId=iNmerchantId;
	
 -- Iterate through rows of processor*/

	set pRowCount=0;
	set pRowCount  = extractValue(iNprovidedXml, 'count(//processor)');

	TRUNCATE TABLE temp_ids;

	while pRowIndex <= pRowCount do

		set pprocessorId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorId'), ''),0);
		if (pprocessorId =0) then
			insert into `tb_processor`
			(
				`processorNumber`
				,`merchantId`
				,`processorTypeId`
				,`firstProcessedDate`
		  
			)
			Values
			(
				coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorNumber'), ''),0)
				,iNmerchantId
				,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorTypeId'), ''),0)
				,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@firstProcessedDate'), ''),0)
			);	  
			insert into temp_ids (id) values (LAST_INSERT_ID());
		else
			update tb_processor set 
			firstProcessedDate=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@firstProcessedDate'), ''),0),
			processorNumber=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorNumber'), ''),0)
			where merchantId=iNmerchantId and processorTypeId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorTypeId'), ''),0);
			insert into temp_ids (id) values (pprocessorId);
		end if;
		set pRowIndex = pRowIndex + 1;

	end while;
	/* Del processors that are not in XML */
	delete from tb_processor where merchantId=iNmerchantId and  ProcessorId not in (select id from temp_ids);
	
	set pcontractId=COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@mcontractId'),''),0);
	call avz_mrc_spTradeReference(iNprovidedXml,iNmerchantId,pcontractId);

# Add Bank Details 

Call avz_mrc_spInsertAffiliation(iNmerchantId,pRNCNumber,pOwnerPassport);

call avz_mrc_spInserBankDetails
(
iNprovidedXml
,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@mbankId'),''),0)
,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@maccountNumber'),''),0)
,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@maccountName'),''),0)
,iNmerchantId
,pcontractId
,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@mbankcode'),''),0)
);
	/*
	If the parameter says to complete the task then we need to create a contract on the system
	*/
	
		set loanAmount= COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@mloanAmountRequired'),''),0);
		# Insert contract to the system
		call avz_cnt_spInsertContract
		(
		iNmerchantId
		,30001
		,1
		,1
		,now()
		,loanAmount
		,13100
		,pcontractId
		, COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@mprimarySalesRepId'),''),0)
		,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@msecondarySalesRepId'),''),0)
		,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@mtypeofadvanceid'),''),0)
		,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@mAnnualSalesCalcFile'),''),0)
		);
	
	/*call avz_cc_spInsertProcessorRequest(iNmerchantId,0);*/

END