-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
drop procedure if exists avz_mrc_spInsertMerchantTemp;

DELIMITER $$

CREATE PROCEDURE `avz_mrc_spInsertMerchantTemp`(
iNLegalName varchar(200),
iNBusinessName varchar(200),
iNSalesRepId bigint,
iNBusinessStartDate date,
iNIndustryTypeId int,
iNBusinessTypeId int,
iNInsertUserId bigint,
iNInsertDate datetime,
iNrnc varchar(11),
iNisCompleted varchar(2),
out merchantId bigint
)
begin
declare iNmerchantId bigint;
start transaction;

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
`RNCNumber`
)
VALUES
(
5,
iNLegalName,
iNBusinessName,
iNSalesRepId,
iNBusinessStartDate,
iNIndustryTypeId,
iNBusinessTypeId,
iNInsertUserId,
now(),
iNrnc
);
set iNmerchantId=last_insert_id();
set merchantId=iNmerchantId;

call CreateMReviewTask(1 ,22002 ,1 ,now() ,null ,null ,0 ,iNmerchantId ,1,iNInsertUserId );

if(iNisCompleted='1')
then
begin

call avz_mrc_spCompleteMerchantReviewTask(iNmerchantId,1,merchantId);

end;
end if;
commit;
end