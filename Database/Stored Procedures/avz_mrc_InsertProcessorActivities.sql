DROP procedure IF EXISTS `avz_mrc_spInsertProcessorActivities`;

DELIMITER //
CREATE PROCEDURE avz_mrc_spInsertProcessorActivities
(iNprovidedXml text)
BEGIN

    declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned; 
    declare _setFlag int unsigned default 0;
	declare _processedDate date ;
      declare _activityType  int unsigned; 
    -- calculate the number of row elements.   
     set pRowCount  = extractValue(iNprovidedXml, 'count(//processoractivity)');
    
    while pRowIndex <= pRowCount do
   
  set  _processedDate= coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@processedDate'), ''),'1900-01-01');
set _activityType=coalesce(nullif(extractvalue(iNprovidedXml, '//child::*[$pRowIndex]/@activityType'), ''),0);
/*

[0]- Processor Processed date - processed date DONE
 [1]-AMI Processor Code - AMIProcessorCode
 [2]- Processor Merchant Number, - ProcessorMID DONE
 [3]-Retrieval source -  RetrievalSource DONE
[4]-Total Amount,- Total Amount DONE
[5]-Withdrawal Amount,-APPLIED AMOUNT DONE
[6]-Activity type,- ActivityTypeID
[7]-(Specified Percentage) Rate,- Rate DONE
 [8]-Start date- start date DONE
 [9]-End date,- startdatte DONE
*/  
if exists (select 1 from  tb_processoractivities where ProcessedDate=_processedDate and ActivityTypeId=_activityType)=0 then 

insert into  tb_processoractivities
(
MerchantId,
processeddate,
AMIProcessorCode,
ProcessorMID,
RetrievalSource,
TotalAmount,
AppliedAmount,
ActivityTypeId,
Rate,
StartDate,
EndDate,
InsertDate
)
SELECT 
18,
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processedDate'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processorCode'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@pMerchantNumber'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@retrievalSource'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@totalAmount'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@withdrawalAmount'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@activityType'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@specifiedRate'),
                    ''),
            0),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@startDate'),
                    ''),
            '1900-01-01'),
    COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@endDate'),
                    ''),
            '1900-01-01',now());
		
   
   else 

UPDATE tb_processoractivities 
SET 
    processeddate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processedDate'),
                    ''),
            0),
    AMIProcessorCode = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@processorCode'),
                    ''),
            0),
    ProcessorMID = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@pMerchantNumber'),
                    ''),
            0),
    RetrievalSource = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@retrievalSource'),
                    ''),
            0),
    TotalAmount = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@totalAmount'),
                    ''),
            0),
    AppliedAmount = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@withdrawalAmount'),
                    ''),
            0),
    ActivityTypeId = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@activityType'),
                    ''),
            0),
    StartDate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@specifiedRate'),
                    ''),
            '1900-01-01'),
    EndDate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@startDate'),
                    ''),
            '1900-01-01'),
    ProcessedDate = COALESCE(NULLIF(EXTRACTVALUE(iNprovidedXml,
                            '//child::*[$pRowIndex]/@endDate'),
                    ''),
            '1900-01-01')
WHERE
    ProcessedDate = _processedDate
        AND ActivityTypeId = _activityType;
   
   
   end if;

      set pRowIndex = pRowIndex + 1;

	 end while;


	 
END;

