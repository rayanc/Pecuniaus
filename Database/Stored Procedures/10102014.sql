drop procedure if exists avz_mrc_spSaveDataEntryTask;
delimiter $$
CREATE  PROCEDURE `avz_mrc_spSaveDataEntryTask`(iNprovidedXml text, iNmerchantId bigint, iNisCompleted smallint )
BEGIN
declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned default 0;
    declare pAddressId int unsigned default 0;
    declare pContactId int unsigned default 0;
    declare pMerchantId int unsigned default 0;	
    declare loanAmount double unsigned default 0.0;
	declare _addressId int unsigned default 0;
	declare _ownerid bigint default 0;
    declare _contactid bigint default 0;
	
	
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
    `businessStartDate` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessStartDate'),
                    ''),
            0),
    `businessWebSite` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessWebSite'),
                    ''),
            0),
    `businessTypeId` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@businessTypeId'),
                    ''),
            0),
    `rentFlag` = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@rentFlag'),
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

/*
Add the logic to insert affiliations to the merchants
*/

if exists(select 1 from tb_merchants 
where 
legalName=COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@rnc'),
                    ''),
            0) 
and 
rncnumber=COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//merchantinfo/merchantbasicinfo/@rnc'),
                    ''),
            0))=0 then

call avz_mrc_spInsertAffiliation(iNmerchantId,0);
end if;


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
	
	-- Iterate through rows of Owners
     set pRowCount  = extractValue(iNprovidedXml, 'count(//owner)');
    
	
    while pRowIndex <= pRowCount do
        -- Enter the details in the address table     
		
		call avz_gen_spInsertAddress
		(	
		 1,
		 coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressLine1'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@addressLine2'), ''),0)
		 ,'USA'
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@city'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@state'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@zipId'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone1'), ''),0)
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@phone2'), ''),0)
		 ,NULL
		 ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@email'), ''),0)
		 ,NULL
		 ,NULL
		 ,_addressId
		 );
		 
	    Set pAddressId = _addressId();
	
	  -- Enter the details in the Contact Table
		call avz_gen_spInsertContact
		( 1
		,''
		,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerFirstName'), ''),0)
		,''
		,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerLastName'), ''),0)
		,pAddressId
		,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ownerDOB'), ''),0)
		,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@ssn'), ''),0),iNmerchantId
		);
			  
		Set pContactId = LAST_INSERT_ID();
	  
	    -- Enter details in the owner's table
		call avz_mrc_spInsertowner( iNownerId,
		iNmerchantId
		,COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@businessStartDate'),''))
		,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@rentFlag'), ''),0)
		,coalesce(nullif(extractvalue(iNprovidedXml, '//merchantinfo/merchantbasicinfo/@rentAmount'), ''),0)
		,0.0
		,1
		,NOW()
      	,pContactId);
	 
         Set pAddressId = 0;
		 Set pContactId = 0;
	     set pRowIndex = pRowIndex + 1;

		 end while;
		 set pRowCount = 0;
		 set pRowIndex = 1;
	 
	 -- Iterate through rows of processor
	 set pRowCount  = extractValue(iNprovidedXml, 'count(//processor)');
     while pRowIndex <= pRowCount do
     call avz_mrc_spInsertProcessorDetails
      (
	     coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorId'), ''),0)
         ,iNmerchantId
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorTypeId'), ''),0)
        
	  );	  
      	
      set pRowIndex = pRowIndex + 1;

	 end while;

/*If the parameter says to complete the task then we need to create a contract on the system
*/
if iscompleted=0 then 
begin

 set loanAmount= COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,'//merchantinfo/merchantbasicinfo/@loanedAmount'),''),0);
# Insert contract to the system
call avz_cnt_spInsertContract(iNmerchantId,20001,1,1,now(),loanAmount,13100,0);
end;

end if;
	
END