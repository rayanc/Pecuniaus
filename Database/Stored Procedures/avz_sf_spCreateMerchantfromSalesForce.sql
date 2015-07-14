drop procedure if exists avz_sf_spCreateMerchantfromSalesForce;
delimiter $$

create procedure avz_sf_spCreateMerchantfromSalesForce(
iNdataxml text
)
begin
declare pRowIndex int unsigned default 1;   
declare pRowCount int unsigned default 0;
declare _accountId int unsigned default 0;	
declare _industrytype bigint default  0;
declare _businesstype bigint default  0;
declare _merchantId bigint;
declare _mainmerchantId bigint;
declare iNaccountId varchar(200) default '';
declare _salesrepid bigint default 0;
declare _ptaskid bigint default 0;
declare _merchantStatus bigint default 0;
declare _province bigint default 0;
DECLARE _iOaddressId bigint default 0;
declare _address varchar(200) default '';
declare _city varchar(200) default '';
declare _state varchar(200) default '';
declare _country varchar(200) default '';
declare _phone1 varchar(200) default '';
declare _phone2 varchar(200) default '';
declare _email varchar(200) default '';
declare _zipcode varchar(200) default '';
declare _mComercialReference1 varchar(200) default '';
declare _mComercialReference2 varchar(200) default '';
declare _mComercialReferenceTele1 varchar(200) default '';
declare _mComercialReferenceTele2 varchar(200) default '';

SET SQL_SAFE_UPDATES = 0;
SET iNaccountId=LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@accountId'),
                                    ''),
                            '')));
SELECT 
    industryTypeId
FROM
    lkp_tb_industrytypes
WHERE
    name = LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@industryType'),
                                    ''),
                            ''))) INTO _industrytype;

SELECT 
    EntitytypeID
FROM
    lkp_tb_entitytypes
WHERE
    name = LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@businessType'),
                                    ''),
                            ''))) INTO _businesstype;
                             
SELECT 
    id
FROM
    tb_users
WHERE
    email = LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@owneremail'),
                                    ''),
                            '')))
LIMIT 1 INTO _salesrepid;

SELECT 
    StatusId
FROM
    lkp_tb_statuses
WHERE
    StatusName = LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@merchantstatus'),
                                    ''),
                            '')))
LIMIT 1 INTO _merchantStatus;
                            

SELECT 
    StateID
FROM
    lkp_tb_province
WHERE
    State = LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@province'),
                                    ''),
                            ''))) INTO _province;
start transaction;
set _address= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@merchantaddress'),
                                    ''),
                            '')));

set _city= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@merchantcity'),
                                    ''),
                            '')));
set _country= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@merchantcountry'),
                                    ''),
                            '')));
set _email= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@owneremail'),
                                    ''),
                            '')));
set _phone1= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@telephone1'),
                                    ''),
                            '')));
set _phone2= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@telephone2'),
                                    ''),
                            '')));
set _zipcode= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@merchantzipcode'),
                                    ''),
                            '')));
set _mComercialReference1= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@mComercialReference1'),
                                    ''),
                            '')));
set _mComercialReference2= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@mComercialReference2'),
                                    ''),
                            '')));
                            
set _mComercialReferenceTele1= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@mComercialReferenceTele1'),
                                    ''),
                            '')));
set _mComercialReferenceTele2= LTRIM(RTRIM(COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                                            '//merchantinfo/merchantbasicinfo/@mComercialReferenceTele2'),
                                    ''),
                            '')));                            
IF EXISTS (
 SELECT merchantid_tmp FROM tmp_tb_merchants WHERE accountId = iNaccountId limit 1) =0 THEN 

begin 

INSERT INTO tmp_tb_merchants
(
`CompanyId`,
`LegalName`,
`BusinessName`,
`SalesRepId`,
`BusinessStartDate`,
`IndustryTypeId`,
`BusinessTypeId`,
`InsertUserId`,
`InsertDate`,
`RNCNumber`,
`CNetProcessorNbr`,
`VNetProcessorNbr`,
`TelePhone1`,
`TelePhone2`,
`City`,
`Province`,
`Country`,
`BusinessType`,
`IndustryType`,
`AccountId`,
`salesRepName`,
`firstCreditDate`,
`OtherProcessorNbr`
)
VALUES
(
5,
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@legalName'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@businessName'),''),''),
_salesrepid,
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@businessStartDate'),''),''),
_industrytype,
_businesstype,
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@insertUserId'),''),''),
now(),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@rnc'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@cardNetNbr'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@vNetnbr'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@telephone1'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@telephone2'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@city'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@province'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@country'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@businessType'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@industryType'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@accountId'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@salesRepName'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@creditDate'),''),''),
COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//merchantinfo/merchantbasicinfo/@otherProcessor'),''),'')

);

set  _merchantId= last_insert_id();

# Create a merchant review task for the newly inserted merchant.
insert into tb_tasks(TaskTypeId,StatusId,priority,startDate,merchantId,merchantIdTmp,workflowId ) 
values(1,22001,1,now(),0,_merchantId,1 );

 set _ptaskid=last_insert_id();
UPDATE tb_tasks 
SET 
    AssignedUserID = _salesrepid,
    AssignedDate = NOW(),
    ModifyDate = NOW(),
    ModifyUserID = _salesrepid,
    StatusID = 22002
WHERE
    TaskID = _ptaskid;
commit;
end;
else 

begin
declare merchantidtmp bigint;

SELECT 
    merchantid_tmp
INTO merchantidtmp FROM
    tmp_tb_merchants
WHERE
    `AccountId` = iNaccountId
LIMIT 1;


UPDATE tmp_tb_merchants 
SET 
    `CompanyId` = 5,
    `LegalName` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@legalName'),
                    ''),
            ''),
    `BusinessName` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@businessName'),
                    ''),
            ''),
    `SalesRepId` = _salesrepid,
    `BusinessStartDate` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@businessStartDate'),
                    ''),
            ''),
    `IndustryTypeId` = _industrytype,
    `BusinessTypeId` = _businesstype,
    `ModifyUserId` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@insertUserId'),
                    ''),
            ''),
    `Modifydate` = NOW(),
    `RNCNumber` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@rnc'),
                    ''),
            ''),
    `CNetProcessorNbr` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@cardNetNbr'),
                    ''),
            ''),
    `VNetProcessorNbr` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@vNetnbr'),
                    ''),
            ''),
    `TelePhone1` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@telephone1'),
                    ''),
            ''),
    `TelePhone2` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@telephone2'),
                    ''),
            ''),
    `City` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@city'),
                    ''),
            ''),
    `Province` = _province,
    `Country` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@country'),
                    ''),
            ''),
    `BusinessType` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@businessType'),
                    ''),
            ''),
    `IndustryType` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@industryType'),
                    ''),
    
    ''),
    `firstCreditDate` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@creditDate'),
                    ''),
            ''),
            `OtherProcessorNbr`=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@otherProcessor'),
                    ''),
            '')
WHERE
    merchantid_tmp = merchantidtmp;

set  _merchantId= merchantidtmp;
end;
end if;

SELECT 
    merchantid
INTO _mainmerchantId FROM
    tmp_tb_merchants
WHERE
    accountId = iNaccountId
LIMIT 1;
   # select _mainmerchantId;

IF  (ifnull(_mainmerchantId,0) )!=0 THEN 

begin
UPDATE tb_merchants 
SET 
    `CompanyId` = 5,
    `LegalName` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@legalName'),
                    ''),
            ''),
    `BusinessName` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@businessName'),
                    ''),
            ''),
    `SalesRepId` = _salesRepId,
    `BusinessStartDate` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@businessStartDate'),
                    ''),
            ''),
    `IndustryTypeId` = _industrytype,
	`BusinessTypeId` =_businesstype,
    `ModifyUserId` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@insertUserId'),
                    ''),
            ''),
    `Modifydate` = NOW(),
    `RNCNumber` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@rnc'),
                    ''),
            ''),
    `CNetProcessorId` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@cardNetNbr'),
                    ''),
            ''),
    `VNetProcessoIdd` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@vNetnbr'),
                    ''),
            ''),
    `TelePhone1` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@telephone1'),
                    ''),
            ''),
    `TelePhone2` = COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,
                            '//merchantinfo/merchantbasicinfo/@telephone2'),
                    ''),
            ''),
   `StatusId` = _merchantStatus
WHERE
    merchantid = _mainmerchantId;
    set  _merchantId=_mainmerchantId;
end;
end if;

set pRowCount  = extractValue(iNdataxml, 'count(//contacts)');
 while pRowIndex <= pRowCount do

            
call avz_sf_spCreateContactsSalesForce(
_merchantId,
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@jobtitle'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@firstname'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@lastname'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@isowner'), ''),0)='1',
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@cellPhone'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@homePhone'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@contactaddress'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@contactaddress'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@contactcountry'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@contactstate'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@contactcity'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@contactzipcode'), ''),0),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@email'), ''),''),
coalesce(nullif(extractvalue(iNdataxml, '//child::*[$pRowIndex]/@contactId'), ''),0)

);

	set pRowIndex = pRowIndex + 1;
end while;

#select _mComercialReference1;
IF EXISTS (
 SELECT tmp_merchantid FROM tmp_tb_tradereference WHERE tmp_merchantid = _merchantId limit 1)=0 THEN 

begin 
  insert into tmp_tb_tradereference(tradereferenceName1,tradereferencePhone1,tradereferenceName2,tradereferencePhone2,tmp_merchantid)
  values(_mComercialReference1,_mComercialReferenceTele1,_mComercialReference2,_mComercialReferenceTele2,_merchantId);
  end;
  else 
  begin
  update tmp_tb_tradereference
  set 
  tradereferenceName1=_mComercialReference1,
  tradereferencePhone1=_mComercialReferenceTele1,
  tradereferenceName2=_mComercialReference2,
  tradereferencePhone2=_mComercialReferenceTele2
  where tmp_merchantid=_merchantId;
  
  end;
end if;
end;