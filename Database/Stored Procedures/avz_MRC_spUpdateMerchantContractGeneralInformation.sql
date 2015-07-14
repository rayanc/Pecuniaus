drop procedure if exists avz_MRC_spUpdateMerchantContractGeneralInformation;

delimiter $$

create procedure avz_MRC_spUpdateMerchantContractGeneralInformation
(
iNcontractId bigint,
iNRetentionPercentage double,
iNRetentionChangeReason varchar(100),
iNUserId bigint
)
begin
start transaction;
update tb_contracts set Retention=iNRetentionPercentage,RetentionChangeReason=iNRetentionChangeReason,
ModifyUserId=iNUserId,ModifyDate=now()
Where contractId=iNcontractId;
commit;
end;