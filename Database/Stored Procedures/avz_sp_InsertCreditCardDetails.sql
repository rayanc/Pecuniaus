DELIMITER $$
CREATE PROCEDURE avz_sp_InsertCreditCardDetails
(
iCreditCardActivityId bigint(20),
	iNMerchantID bigint(20),
	iNProcessorId int(11),
	iNProcessorMerchantId bigint(20),
	iNAcctivityTypeId int(11),
	iNCurrencyId	int(11),
	iNTotalAmount double,
	iNTotalTickets int(11),
	iNStartDate datetime,
	iNEndDate datetime,
	iNProcessedDate datetime,
	iNIndustryId int(11),
	iNMerchantStatusId int(11),
	iNAuthorizedOwnerId int(20)
)
BEGIN
INSERT INTO avz.tb_creditcardactivity
(
CreditCardActivityId,
MerchantId,
ProcessorId,
ProcessorMerchantId,
AcctivityTypeId,
CurrencyId,
TotalAmount,
TotalTickets,
StartDate,
EndDate,
ProcessedDate,
IndustryId,
MerchantStatusId,
AuthorizedOwnerId
)VALUES
(
iNMerchantId,
iNMerchantId,
iNProcessorMerchantId,
iNAcctivityTypeId,
iNCurrencyId,
iNTotalAmount,
iNTotalTickets,
iNStartDate,
iNEndDate,
iNProcessedDate,
iNIndustryId,
iNMerchantStatusId,
iNAuthorizedOwnerId
);
END $$
DELIMITER ;