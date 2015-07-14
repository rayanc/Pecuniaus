drop procedure if exists `avz_cc_InsertGeneratedOffers`;

-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
DELIMITER $$

CREATE  PROCEDURE `avz_cc_InsertGeneratedOffers`(
	iNdataxml text,
	iNmerchantid bigint,
	iNcontractid bigint,
	iNannualSales double,
	iNavgmccv double,
	iNreason text,
	iNismccvupdated smallint
)
begin
	declare pRowIndex int unsigned default 1;   
	declare pRowCount int unsigned default 0;
	declare pofferId int unsigned default 0;
	declare offernote text;	

	set pRowCount  = extractValue(iNdataxml, 'count(//offers)');
		
	while pRowIndex <= pRowCount do
		set pofferId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@offerId'),''),0);
		if (pofferId=0 ) then

			INSERT INTO tb_offers
			(

				MerchantId,
				ContractId,
				Turn,
				LoanAmount,
				OwnedAmount,
				Proportion,
				Retention,
				OfferCreationDate,
				CreatedUserId,
				expirationDate,
				monthlypayment,
				salestaken,
				yearly
			)
			VALUES
			(
				iNmerchantid,
				iNcontractid,
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@turn'),''),0),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@loanAmount'),''),0),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@ownedAmount'),''),0),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@proportion'),''),0),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@retention'),''),0),
				now(),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@insertuserId'),''),0),
				DATE_FORMAT(DATE_ADD(now(),INTERVAL 30 day),'%Y-%m-%d'),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@monthlyPayment'),''),0),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@salestaken'),''),0),
				COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@yearly'),''),0));
	/*
			set offernote= concat('Offer MCA Amount: ',COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@loanAmount'),''),0)
				,' Offer Owned Amount: ', COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@ownedAmount'),''),0), 
				'  Offer Price: ',COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@proportion'),''),0),
				' Retention: ', COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@retention'),''),0),
				' Turn: ',COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@turn'),''),0) );

			insert into tb_notes(NoteTypeId, merchantId, contractId, Note, WorkflowId, ScreenName, InsertDate)
			values(550001,iNmerchantId,iNcontractid,offernote,1,'Offer Creation',date_format(now(),'%y-%m-%d'));
	*/
		else

			update tb_offers
			set 
				Turn=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@turn'),''),0),
				LoanAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@loanAmount'),''),0),
				OwnedAmount=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@ownedAmount'),''),0),
				Proportion=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@proportion'),''),0),
				Retention=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@retention'),''),0),
				ModifyDate=DATE_FORMAT(now(),'%Y-%m-%d'),
				ModifyUserId=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@insertuserId'),''),0),
				expirationDate=DATE_FORMAT(DATE_ADD(now(),INTERVAL 30 day),'%Y-%m-%d'),
				monthlypayment=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@monthlyPayment'),''),0),
				salestaken=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@salestaken'),''),0),
				yearly=COALESCE(NULLIF(EXTRACTVALUE(iNdataxml,'//child::*[$pRowIndex]/@yearly'),''),0)
				where OfferId =pofferId;

		end if;
		set pofferId=0;
		set offernote='';
		set pRowIndex = pRowIndex + 1;
	end while;

	call avz_mrc_spAddManualMCCV(iNmerchantId,iNannualSales,iNavgmccv,iNreason,iNcontractid);


end