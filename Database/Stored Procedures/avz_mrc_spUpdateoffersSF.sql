DROP PROCEDURE IF EXISTS avz_mrc_spUpdateoffersSF;
delimiter $$

create procedure avz_mrc_spUpdateoffersSF(
iNturn double,
iNownedAmount double,
iNloanAmount double,
iNproportion double,
iNretention double,
iNexpirationDate datetime,
iNyealy double)

UPDATE tb_offers 
SET 
    Turn = iNturn,
    LoanAmount = iNloanAmount,
    OwnedAmount = iNownedAmount,
    Proportion = iNproportion,
    Retention = iNretention,
    ModifyDate = NOW (),
    ModifyUserId = 1,
    expirationDate = iNexpirationDate,
    yearly = iNyealy
WHERE
    sfid = iNaccountid;


