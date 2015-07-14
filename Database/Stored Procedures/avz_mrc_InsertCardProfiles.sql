DROP procedure IF EXISTS `avz_mrc_InsertCardProfiles`;

DELIMITER //


CREATE PROCEDURE avz_mrc_InsertCardProfiles
(iNprovidedXml LONGTEXT )
BEGIN

    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned; 
    declare _setFlag int unsigned default 0;
	declare _processedDate date ;
	declare _contractId bigint default 0 ;
    declare _merchantId bigint default 0;
	declare _processorID bigint default 0;
     
    -- calculate the number of row elements.   
     set pRowCount  = extractValue(iNprovidedXml, 'count(//monthlyprofile)');
    
    while pRowIndex <= pRowCount do
   
  set  _processedDate= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processedDate'), ''),'1900-01-01');
  set _merchantId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@merchantId'), ''),0);
   set _processorID=0;
   Select p.ProcessorId into _processorID from lkp_tb_processorlist pl JOIN tb_processor p on pl.processorID=p.ProcessorTypeId 
where pl.processorcode =extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorCode') and p.merchantId=_merchantId 
and p.processorNumber=nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@pMerchantNumber'), '');

#if exists (select 1 from  tb_creditcardactivity where ProcessedDate=_processedDate)=0 then 
select contractid into _contractid from tb_contracts where merchantid=_merchantId order by CreationDate desc limit 1;

insert into  tb_creditcardactivity
(MerchantId,
 ProcessorMerchantId,
 ProcessorId,
 RetrievalSource,
 CurrencyId, 
 TotalAmount,
 TotalTickets,
 StartDate,
 EndDate,
 ProcessedDate,
 MerchantStatusId,
 month,
 year,contractid,AcctivityTypeId,legalName,rnc,bankAccountNumber,authorizedOwnerName,insertdate,IsAutomated)

      select 
	    _merchantId
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@pMerchantNumber'), ''),0)
        ,coalesce(nullif(_processorID,0),0)
		, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@retrievalSource'), ''),0)
        , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@currencyId'), ''),0)
		, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@totalAmount'), ''),0)
		, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@totalTickets'), ''),0)
		, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@startDate'), ''),0)
		, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@endDate'), ''),0)
		, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processedDate'), ''),'1900-01-01')
        #, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorMerchantCategory'), ''),0)
        , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorMerchantStatus'), ''),0)
        #, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorMerchantBusinessStartDate'), ''),0)
        #, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@legalName'), ''),0)
        #, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@rnc'), ''),0)
        #, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@bankAccountNumber'), ''),0)
        #, coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@authorizedOwnerName'), ''),0)
         , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@month'), ''),0)
         , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@year'), ''),0)
         ,_contractid
         , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@activityType'), ''),0)
         , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@legalName'), ''),'')
         , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@rnc'), ''),'')
         , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@bankAccountNumber'), ''),'')
         , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@authorizedOwnerName'), ''),'')
		 , current_date(),1;

   
/*   else 

   update tb_creditcardactivity
   set 
   MerchantId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@merchantId'), ''),0),
 ProcessorMerchantId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@pMerchantNumber'), ''),0),
 ProcessorId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorCode'), ''),0),
 RetrievalSource=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@retrievalSource'), ''),0),
 CurrencyId=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@currencyId'), ''),0), 
 TotalAmount=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@totalAmount'), ''),0),
 TotalTickets=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@totalTickets'), ''),0),
 StartDate=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@startDate'), ''),'1900-01-01'),
 EndDate=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@endDate'), ''),'1900-01-01'),
 ProcessedDate=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processedDate'), ''),'1900-01-01'),
 MerchantStatusId= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorMerchantStatus'), ''),0),
 year=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@year'), ''),0),
 month=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@month'), ''),0)
   where ProcessedDate=_processedDate;*/
   
   
   
  # end if;

      set pRowIndex = pRowIndex + 1;

	 end while;

CALL avz_cc_spUpdateQueueStatus();

END;

