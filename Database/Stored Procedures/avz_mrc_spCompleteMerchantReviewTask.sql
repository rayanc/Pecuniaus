
#call avz_mrc_spCompleteMerchantReviewTask(100011,2,@merchantId);
drop procedure if exists avz_mrc_spCompleteMerchantReviewTask;
delimiter $$
CREATE  PROCEDURE `avz_mrc_spCompleteMerchantReviewTask`(
iNmerchantId bigint,
insertbyUserId bigint,
out merchantId bigint 

)
begin

DECLARE _address1, _address2,_email1,_email2, _email3,_jobTitle,_firstName,_lastName,_middleName_companyName,_cNetProcessorId,_vNetProcessoIdd,
_businessName,_legalName,_rncNumber,_accountId varchar(200);
DECLARE _phone1,_phone2,_phone3,_middleName varchar(12);

DECLARE _ssn varchar(9);
DECLARE _country,_city,_state varchar(100) ;
DECLARE _rentFlag int;
DECLARE _businessStartDate date;

DECLARE _zipCodeId,_insertUserId,
_contactTypeId,_addressId,
_addressTypeId,
_workFlowId,_salesRepId,_industryTypeId,_businessTypeId,
_ownerId,_statusId, _merchantId bigint;
DECLARE  iOaddressId BIGINT DEFAULT 0;
DECLARE _dateofBirth date;
declare _noofMerchant bigint default 0;
DECLARE _rentedAmount double;
 DECLARE `_rollback` BOOL DEFAULT 0;
DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET `_rollback` = 1;


# Set the merchant as inactive
set _statusId=20002;
-- If exists a record with the same business name,merchant name rnc number then dont create a new merchant
SET SQL_SAFE_UPDATES = 0;
START TRANSACTION;


SELECT 
    IFNULL(addressline1, ''),
    IFNULL(addressline2, ''),
    IFNULL(country, ''),
    IFNULL(city, ''),
    IFNULL(province, ''),
    1,
    IFNULL(telephone1, ''),
    IFNULL(telePhone2, ''),
    '',
    '',
    '',
    '',
    IFNULL(legalName,''),
    IFNULL(businessName,''),
    SalesRepId,
    IndustryTypeId,
    BusinessTypeId,
    IFNULL(CNetProcessorNbr, ''),
    IFNULL(VNetProcessorNbr, ''),
    IFNULL(RNCNumber, ''),
    BusinessStartDate,
    accountId
INTO _address1 , _address2 , _country , _city , _state , _zipCodeId , _phone1 , _phone2 , _phone3 , _email1 , _email2 , _email3 , _legalName , _businessName , _salesRepId , _industryTypeId , _businessTypeId , _cNetProcessorId , _vNetProcessoIdd , _rncNumber , _businessStartDate , _accountId FROM
    tmp_tb_merchants
WHERE
    MerchantId_TMP = iNmerchantId;

SELECT 
    IFNULL(contactTypeId, 0),
    jobTitle,
    firstName,
    middleName,
    lastName,
    dateofBirth,
    ssn
INTO _contactTypeId , _jobTitle , _firstName , _middleName , _lastName , _dateofBirth , _ssn FROM
    tmp_tb_contacts
WHERE
    merchantid = iNmerchantId; 
    
if (_legalName <> '' and _businessName <> '' and _rncNumber <> '')  then
#insert the address 
begin
#@addressTypeId=1
CALL avz_gen_spinsertAddress(1, _address1 , _address2 ,
 _country , _city , _state , _zipCodeId , 
 _phone1 , _phone2 , _phone3 , 
 _email1 , _email2 , _email3 , insertbyUserId ,@iOaddressId );
 
# Create a merchant
insert into `tb_merchants`
(
`CompanyId`,`LegalName`,`BusinessName`,`WorkflowId`,`AddressId`,`AddressId2`,`StatusId`,`StatusModifyDate`,`SalesRepId`,`BusinessStartDate`,
`industryTypeId`,`BusinessTypeId`,`insertUserId`,`insertDate`,
`CNetProcessorId`,`VNetProcessoIdd`,`RentFlag`,`RentedAmount`,`RNCNumber`,
`TelePhone1`,`TelePhone2`,accountid)
VALUES
(
5,_legalName,_businessName,1,@_iOaddressId,@_iOaddressId,_statusId,now(),_SalesRepId,_businessStartDate,
_industryTypeId,_businessTypeId,insertbyUserId,now(),_CNetProcessorId,
_vNetProcessoIdd,_rentFlag,_rentedAmount,_rncNumber,_phone1,_phone2,_accountId);



set _merchantId=last_insert_id();



UPDATE tmp_tb_merchants 
SET 
    MerchantId = _merchantId
WHERE
    MerchantId_TMP = iNmerchantId; 

 /*
 Create the owners to the system that are imported from the Sales force
 */
 
call avz_mrc_spInsertOwners(iNmerchantId,_merchantId,@iOaddressId);

end;
else
set _merchantId=iNmerchantId;
end if;


call avz_sp_InsertProcessorInformation (_merchantId,iNmerchantId);


call avz_sp_InsertMerchantTradReference(_merchantId,iNmerchantId);
/*
Complete the task for the temp mrchant
*/
call CompleteReviewTask(iNmerchantId,_merchantId,1,1);

/*
Assign all the tasks that were related  to the temp merchant to main merchant created 
*/
UPDATE tb_tasks 
SET 
    merchantId = _merchantId
WHERE
    MerchantIDTMP = iNmerchantId;
    
    
/*call AVZ_TSK_spAssignTasks(); */

 /*IF `_rollback` THEN
        ROLLBACK;
    ELSE
        COMMIT;
    END IF;*/
set merchantId=_merchantId;

 /*call avz_sp_InsertProcessorInformation (_merchantId,iNmerchantId);
 

 Create the owners to the system that are imported from the Sales force 
 
 
 
call avz_mrc_spInsertOwners(iNmerchantId,_merchantId,@iOaddressId);


Complete the task for the temp mrchant

call CompleteReviewTask(iNmerchantId,_merchantId,1,1);

/*
Assign all the tasks that were related  to the temp merchant to main merchant created 

UPDATE tb_tasks 
SET 
    merchantId = _merchantId
WHERE
    MerchantIDTMP = iNmerchantId;
    
    
/*call AVZ_TSK_spAssignTasks(); 

 /*IF `_rollback` THEN
        ROLLBACK;
    ELSE
        COMMIT;
    END IF;
set merchantId=_merchantId; */
end