drop procedure if exists avz_cnt_spCommericalOwnerDetails;
-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
DELIMITER $$

CREATE PROCEDURE `avz_cnt_spCommericalOwnerDetails`(
	iNMerchantId bigint
	,iNcontractId bigint
	,iNownerDetails text

)
begin

	declare pownerid bigint;
	declare pcontactid bigint;
	declare paddressid bigint;
	declare pRowIndex bigint;
	declare pRowCount bigint; 
    declare pautorized bit(1) default 0;
	declare pOwnerIds varchar(50) default null;

       Create temporary table if not exists temp_ids
    (Id varchar(20), PRIMARY KEY (id)) ENGINE=MEMORY;


	set sql_safe_updates=0;
    /*
	set pRowCount=0;
	set pRowIndex=1;
	set pRowCount  = extractValue(iNownerDetails, 'count(//owner)');


	while pRowIndex <= pRowCount do

		if(COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
											'//child::*[$pRowIndex]/@isAuthorized'),
									''),
							0)="True") then
			set authorised=1;
		else
			set authorised=0;
		end if;

		set pownerid=COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
									'//child::*[$pRowIndex]/@ownerId'),
							''),
					0);

		set pcontactid =COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
									'//child::*[$pRowIndex]/@contactId'),
							''),
					0);

		set paddressid =COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
									'//child::*[$pRowIndex]/@AddressId'),
							''),
					0);



		update tb_owners set
				IsAuthorized= authorised
			where ownerId=pownerid;

		update tb_contacts set
				FirstName = COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
											'//child::*[$pRowIndex]/@ownerName'),
									''),
							0)
					, LastName =COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
											'//child::*[$pRowIndex]/@ownerLastName'),
									''),
							0)
					, PassportNbr= COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
											'//child::*[$pRowIndex]/@passportNbr'),
									''),
							0)
			where ContactID=pcontactid and merchantId=iNMerchantId;


		update tb_addresses
			set Phone1=COALESCE(NULLIF(EXTRACTVALUE(iNownerDetails,
											'//child::*[$pRowIndex]/@telephone'),
									''),
							0)
			where AddressId=paddressid;

		set pRowIndex = pRowIndex + 1;
	end while;
*/

	-- Iterate through rows of Owners

	set pRowCount  = extractValue(iNownerDetails, 'count(//owner)');
    set pRowIndex=1;
    while pRowIndex <= pRowCount do

		set pOwnerId=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerId'), ''),0);
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
	set pRowCount  = extractValue(iNownerDetails, 'count(//owner)');
    set pRowIndex=1;
    while pRowIndex <= pRowCount do

		if coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@addressId'), ''),0) =0 then
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
				 coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@addressLine1'), ''),0)
				 ,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@addressLine2'), ''),0)
				 ,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@country'), ''),0)
				 ,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@city'), ''),0)
				 ,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@stateId'), ''),0)
				 ,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@zip'), ''),0)
				 ,nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@phone1'), '')
				 ,nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@phone2'), '')
				 ,NULL
				 ,nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@email'), '')
				 ,NULL
				 ,NULL
				 ,1
				 ,NOW()
			);
			Set pAddressId = LAST_INSERT_ID();
		else
			update tb_addresses set Address1=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@addressLine1'), ''),0)
			, Address2 =coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@addressLine2'), ''),0)
			, Country =coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@country'), ''),0)
			, City =coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@city'), ''),0)
			, State= coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@stateId'), ''),0)
			, ZipCode =coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@zip'), ''),0)
			, Phone1 =nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@phone1'), '')
			, Phone2 =nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@phone2'), '')
			, Phone3 =null
			, email1 = nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@email'), '')
			, email2 =null
			, email3= null
			where AddressId= coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@addressId'), ''),0);
			Set pAddressId =coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@addressId'), ''),0);
		end if;	

		set pContactId =coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@contactId'), ''),0);

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
			,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerFirstName'), ''),0)
			,''
			,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerLastName'), ''),0)
			,pAddressId
			,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerDOB'), ''),0) 
			,coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ssn'), ''),0),iNmerchantId, coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@passportNumber'), ''),0));
			 
			Set pContactId = LAST_INSERT_ID();
		
		else
			update tb_contacts set 
				JobTitle=''
				,FirstName=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerFirstName'), ''),0)
				,MiddleName=''
				,LastName=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerLastName'), ''),0)
				,DateOfbirth=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerDOB'), ''),0) 
				,SSN=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ssn'), ''),0)
				, PassportNbr=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@passportNumber'), ''),0)
			where ContactId =pContactId;

		end if;

		set pOwnerId=coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@ownerId'), ''),0);
		if(coalesce(nullif(extractvalue(iNownerDetails, '//child::*[$pRowIndex]/@authorised'), ''),0)='True') then
			set pautorized=1;
		else
			set pautorized=0;
		end if;

		if (pOwnerId =0) then
			-- Enter details in the owner's table
			INSERT INTO `tb_owners`
			(`MerchantId`
			,`MortgageAmount`
			,`InsertUserId`
			,`InsertDate`
			,`ModifyUser`
			,`ModifyDate`
			,`ContactId`,
			`IsAuthorized`)
			VALUES
			(iNmerchantId
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
				ModifyDate=NOW(),
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
/*No Delete here
	delete from tb_addresses where AddressId in (select cont.AddressId1 from tb_contacts cont
	inner join tb_owners own on own.ContactId=cont.ContactId where own.OwnerId not in(select id from temp_ids ) and own.merchantId=iNmerchantId);

	delete from tb_contacts where ContactID in (select own.ContactId 
	from tb_owners own where own.OwnerId not in(select id from temp_ids ) and own.merchantId=iNmerchantId);

	delete from tb_owners where OwnerId not in(select id from temp_ids ) and merchantId=iNmerchantId;
	*/
end