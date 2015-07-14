DROP procedure IF EXISTS `avz_mrc_spInsertProcessorResponsesetup`;

DELIMITER //
CREATE PROCEDURE avz_mrc_spInsertProcessorResponsesetup
(iNprovidedXml text)
BEGIN

    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned; 
    declare _setFlag int unsigned default 0;
	declare _processedDate date ;

     
    -- calculate the number of row elements.   
     set pRowCount  = extractValue(iNprovidedXml, 'count(//processoractivity)');
    
    while pRowIndex <= pRowCount do
   
  set  _processedDate= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processedDate'), ''),'1900-01-01');
if exists (select 1 from  tb_creditcardactivity where ProcessedDate=_processedDate)=0 then 


insert into  tb_MerchantRSF
(
MerchantID,ProcessedDate,ProcessorCode,ProcessorMID,Balance,SpecifiedRate,Terminal,SetupStatus,INFO
)
      select 
	    coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@pMerchantId'), ''),0)
        , coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processedDate'), ''),'1900-01-01')
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processorCode'), ''),0)
       ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@pMerchantNumber'), ''),0)
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@balance'), ''),0)
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@rate'), ''),0)
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@terminal'), ''),0)
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@setupstatus'), ''),0)
        ,coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@info'), ''),0);
      
   
   else 

UPDATE tb_MerchantRSF 
SET 
    MerchantId = coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@pMerchantNumber'), ''),0),

    ProcessedDate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processedDate'),
                    ''),
            0),
    ProcessorCode = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processorCode'),
                    ''),
            0),
    ProcessorMID = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@pMerchantNumber'),
                    ''),
            0),
    Balance = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@balance'),
                    ''),
            0),
    SpecifiedRate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@rate'),
                    ''),
            0),
    Terminal = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@terminal'),
                    ''),
            0),
    SetupStatus = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@setupstatus'),
                    ''),
            0),
    INFO = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@info'),
                    ''),
            0)
WHERE
    ProcessedDate = _processedDate;
   end if;
      set pRowIndex = pRowIndex + 1;
	 end while;
END;