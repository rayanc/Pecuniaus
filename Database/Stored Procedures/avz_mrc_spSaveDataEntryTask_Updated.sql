drop procedure if exists avz_mrc_spSaveDataEntryTask;
DELIMITER $$
CREATE PROCEDURE `avz_mrc_spSaveDataEntryTask`(iNprovidedXml text, iNmerchantId bigint, iNisCompleted smallint )
BEGIN
declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned default 0;
    declare pAddressId int unsigned default 0;
    declare pContactId int unsigned default 0;
    declare pMerchantId int unsigned default 0;	
    declare loanAmount double unsigned default 0.0;

  	UPDATE `tb_merchants` 
SET 
    `rncNumber` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@rnc'),
                    ''),
            0),
    `businessName` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessName'),
                    ''),
            0),
    `legalName` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@legalName'),
                    ''),
            0),
    `businessStartDate` = Date(COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessStartDate'),
                    ''),
            0)), 
    `businessWebSite` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessWebSite'),
                    ''),
            0), 
    `businessTypeId` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessTypeId'),
                    ''),
            0),
    `rentFlag` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@propertyType'),
                    ''),
            0),
    `RentedAmount` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@rentAmount'),
                    ''),
            0),
    `industryTypeId` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@industryTypeId'),
                    ''),
            0),
    `cNetProcessorId` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@cNetProcessorId'),
                    ''),
            0),
    `vNetProcessoIdd` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@vNetProcessoId'),
                    ''),
            0),
    `grossannualSales` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@annualSales'),
                    ''),
            0)
WHERE
    merchantId = iNmerchantId;
	



call avz_mrc_spInserBankDetails(COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@bankId'),
                    ''),
            0),COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@accountNumber'),
                    ''),
            0),COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@accountName'),
                    ''),
            0),iNmerchantId,0);
/*Update Merchant Address  */
if coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@addressId'), ''),0) =0 then
        -- Enter the details in the address table     
		INSERT INTO `tb_addresses`
		(
		 `AddressTypeId`
		,`Address1`
		,`Address2`
		,`Country`
		,`City`
		,`State`
		,`ZipCodeId`
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
		 coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@addressLine1'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@addressLine2'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@country'), ''),0)
		 ,nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@city'), '')
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@state'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@zipId'), ''),0)
		 ,nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@phone1'), '')
		 ,nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@phone2'), '')
		 ,NULL
		 ,nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@email'), '')
		 ,NULL
		 ,NULL
		 ,1
		 ,NOW()
		 );
	    Set pAddressId = LAST_INSERT_ID();
update tb_merchants set AddressId=pAddressId WHERE
    merchantId = iNmerchantId;
else
update tb_addresses set Address1=coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@addressLine1'), ''),0)
		, Address2 =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@addressLine2'), ''),0)
		, Country =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@country'), ''),0)
		, City =nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@city'), '')
		, State= coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@state'), ''),0)
		, ZipCodeId =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@zipId'), ''),0)
		, Phone1 =nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@phone1'), '')
		, Phone2 =nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@phone2'), '')
		, Phone3 =null
		, email1 = nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@email'), '')
		, email2 =null
		, email3= null
where AddressId= coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@addressId'), ''),0);

end if;	

	Set pAddressId =0;
	-- Iterate through rows of Owners
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
		,`ZipCodeId`
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
		 ,nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@city'), '')
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@state'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@zipId'), ''),0)
		 ,nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone1'), '')
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
		, City =nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@city'), '')
		, State= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@state'), ''),0)
		, ZipCodeId =coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@zipId'), ''),0)
		, Phone1 =nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone1'), '')
		, Phone2 =nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone2'), '')
		, Phone3 =null
		, email1 = nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@email'), '')
		, email2 =null
		, email3= null
where AddressId= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressId'), ''),0);
	    
end if;	

if coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@contactId'), ''),0) =0 then

	  -- Enter the details in the Contact Table
		INSERT INTO `tb_contacts`
		(`ContactTypeId`
		,`JobTitle`
		,`FirstName`
		,`MiddleName`
		,`LastName`
		,`AddressId1`
		,`DateOfbirth`
		,`SSN`,`merchantid`)
		VALUES
		( 1
		,''
		,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerFirstName'), ''),0)
		,''
		,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerLastName'), ''),0)
		,pAddressId,
		'10/10/2104'
		/*,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerDOB'), ''),0) */
		,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ssn'), ''),0),iNmerchantId
		);
			  
		Set pContactId = LAST_INSERT_ID();
else

update tb_contacts set 
		JobTitle=''
		,FirstName=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerFirstName'), ''),0)
		,MiddleName=''
		,LastName=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerLastName'), ''),0)
		/*,DateOfbirth=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerDOB'), ''),0) */
		,SSN=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ssn'), ''),0)

where merchantid =iNmerchantId;

end if;

if coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerId'), ''),0) =0 then
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
		,`ContactId`)
		VALUES
		(iNmerchantId
		,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessStartDate'),
                    ''))
		,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@rentFlag'), ''),0)
		,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@rentAmount'), ''),0)
		,NULL
		,1
		,NOW()
        ,1
		,NOW()
		,pContactId);
	 
         Set pAddressId = 0;
		 Set pContactId = 0;
	     

else

update tb_owners set
		BusinessStartDate=COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessStartDate'),
                    ''))
		,RentFlag =coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@rentFlag'), ''),0)
		,RentAmount=coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@rentAmount'), ''),0)		
        ,ModifyUser =1
		,ModifyDate=NOW()
where ownerid=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerId'), ''),0);

end if;
		 set pRowIndex = pRowIndex + 1;
		 end while;
		 set pRowCount = 0;
		 set pRowIndex = 1;
	 
	 -- Iterate through rows of processor
	 set pRowCount  = extractValue(iNprovidedXml, 'count(//processor)');
     while pRowIndex <= pRowCount do

if (coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorId'), ''),0)) =0 then
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
else
update tb_processor set processorNumber=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorNumber'), ''),0),
firstProcessedDate= Date(coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@firstProcessedDate'), ''),0))
where ProcessorId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorId'), ''),0) and processorTypeId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorTypeId'), ''),0);

end if;
      	
      set pRowIndex = pRowIndex + 1;

	 end while;


# Add Bank Details 

/*
If the parameter says to complete the task then we need to create a contract on the system
*/
if iNisCompleted=0 then 
begin

 set loanAmount= COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@loanedAmount'),''),0);
# Insert contract to the system
call avz_cnt_spInsertContract(iNmerchantId,20001,1,1,now(),loanAmount,13100,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@contractId'),''),0)); 
end;

end if;
	
END$$
DELIMITER ;
