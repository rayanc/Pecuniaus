drop procedure if exists avz_mrc_spInsertowner;
DELIMITER $$

CREATE  PROCEDURE `avz_mrc_spInsertowner`(
iNownerId bigint,
iNmerchantId bigint,
iNinsertUserId bigint,
iNcontactId bigint

)
begin
if iNownerId=0 then
begin
INSERT INTO `tb_owners`
(
`MerchantId`,
`InsertUserId`,
`InsertDate`,
`ContactId`)
VALUES
(
iNMerchantId,
iNinsertUserId,
now(),
iNContactId);
end;

else
begin
UPDATE tb_owners 
SET 
    `MerchantId` = iNMerchantId,
    `ModifyUser` = iNinsertUserId,
    `ModifyDate` = NOW()
WHERE
    `ownerId` = iNownerId;
end;
end if;
end;
