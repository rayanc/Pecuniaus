#call avz_mrc_spretrieveMerchantEvaluation(130);
drop procedure if exists avz_mrc_spretrieveMerchantEvaluation;

delimiter $$

create procedure avz_mrc_spretrieveMerchantEvaluation(iNmerchantId bigint)
begin

#1
SELECT distinct
`mrc`.`MerchantId` as MerchantId,
`mrc`.`LegalName` AS `legalName`,
`mrc`.`BusinessName` AS `BusinessName`,
DATE_FORMAT(`mrc`.`BusinessStartDate`, '%Y-%m-%d')  AS `BusinessStartDate`,
`ind`.`IndustryTypeCode` as `IndustryTypeId`,
`ind`.`name` as IndustryType,
`mrc`.`BusinessTypeId` AS `businessTypeId`,
`mrc`.`BusinessWebSite` AS `BusinessWebSite`,
case when `mrc`.`RentFlag`=200001 Then 1 else 0 end as `RentFlag`,
IFNULL( `mrc`.`RentedAmount`,0)  `MRCRentAmount` ,    
mrc.CurrencyId as `CurrencyId`,        
`adr`.`Phone1` as `TelePhone1`,
`adr`.`Phone2` as `TelePhone2`,
`mrc`.`GrossAnnualSales` as `MRCGrossYearlySales`,
case when (ifnull(`cnrt`.`OwnedAmount`,0) - ifnull(`cnrt`.`PaidAmount`,`cnrt`.`OwnedAmount`))  > 0 then 1 
else 0
end
 as `IsActive`,
`adr`.`address1` as`BusinessAddress1`,
`adr`.`address2` as `BusinessAddress2`,
`adr`.`Phone1` as `BusinessPhone1`,
`adr`.`Phone2` as `BusinessPhone2`,
`adr`.`Phone3` as `BusinessPhone3`,
`CC`.`CountryName` as `BusinessCountry`,
`adr`.`city` as `BusinessCity`,
`ST`.`state` as `BusinessState`,
`adr`.`zipcode` as `BusinessZipCode`,
`adr`.`email1` as `email`,
`cnrt`.`ContractId` as `ContractID`,
case when `cnrt`.`ContractTypeId` =13100 then 'NEW'
WHEN 13200 THEN 'RENEWAL'
END  as `ContractType`,
`cnrt`.`loanedamount` as `LoanedAmount`,
`cnrtActive`.`ownedamount`as `OwedAmount`,
`cnrtActive`.`OwnedAmount`-`cnrt`.`loanedamount` as Price,
cnrtActive.paidamount as `PaidAmount`,
(cnrtActive.loanedamount-cnrtActive.paidamount) as  `AmountPendingtoPay`,
cnrtActive.turn as `Turn`,#contracts table
DATE_FORMAT(cnrtActive.fundingDate,'%Y-%m-%d') as `FundingDate`,
DATE_FORMAT(cnrtActive.LastPaymentDate,'%Y-%m-%d') as `LastPaymentDate`,#processor activity
cnrtActive.BuyRate as`BuyRate`,
cnrtActive.DailyPayment as `DailyPayment`,
cnrtActive.AdjustedTurn as `AdjustedTurn`,
stats.StatusName as `ContractStatus`,
DATE_FORMAT(max(pa.processeddate),'%Y-%m-%d') as `DateOfLastActivity`,
(avz_aff_fngetAffiliationIDs(`mrc`.`MerchantId`)) as `AffiliationID`,
(avz_aff_fngetAffiliationTypes(`mrc`.`MerchantId`)) as `AffliationType`,
(avz_aff_fngetAffiliationNames(`mrc`.`MerchantId`)) as `AffiliationBusinessName`,
 `mrc`.`SalesRepId` as `SalesRepID`,
 concat(`repsal`.`FirstName`, '  ',`repsal`.`LastName`) as `SalesRepName`,
 (select count(merchantId) from tb_collections where merchantid=`mrc`.`MerchantId`) as  `TimesInCollections`

FROM
    (((((`tb_merchants` `mrc`
   
    LEFT JOIN `tb_addresses` `adr` ON `adr`.`addressid` = `mrc`.`addressid`
	left JOIN `lkp_tb_province` `pro` on `pro`.`State`	=`adr`.`State`
	left join `tb_contracts` `cnrt` on `cnrt`.`MerchantId` =`mrc`.`MerchantId` and `cnrt`.`ContractId`=(Select max(contractid) from tb_contracts where merchantid=`mrc`.`MerchantId`)
	left join `tb_contracts` `cnrtActive` on `cnrtActive`.`MerchantId` =`mrc`.`MerchantId` and `cnrtActive`.`StatusId`=20007
    left JOIN `tb_salesrep` `sal` ON ((`mrc`.`SalesRepId` = `sal`.`SalesRepId`)))
    left JOIN `tb_contacts` `repsal` ON ((`sal`.`Contactid` = `repsal`.`ContactId`)))
    LEFT JOIN `tb_tasks` `tsk` ON ((`tsk`.`MerchantId` = `mrc`.`MerchantId`)))
    LEFT JOIN `tb_tasktypes` `tsktyp` ON ((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`)))
    LEFT JOIN `tb_workflows` `wrk` ON ((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`))
	left join `tb_bankdetails` `bnk` on ((`bnk`.`MerchantId` = `mrc`.`MerchantId`)))
    join lkp_tb_industrytypes ind on mrc.IndustryTypeId=ind.IndustryTypeId
    left join lkp_tb_statuses stats on stats.StatusId=cnrt.StatusId
    left join tb_collections col on col.merchantid=mrc.MerchantId
	left join lkp_tb_country CC on adr.Country=CC.CountryId
	LEFT JOIN tb_processoractivities  pa on cnrtActive.ContractId=pa.ContractId
	LEFT JOIN lkp_tb_province ST on adr.State=ST.StateID
    where `mrc`.`MerchantId`=iNmerchantId limit 1;
	
/*Owner Info*/	
SELECT DISTINCT
    Date_format(BusinessStartDate,'%Y-%m-%d') AS OwnerBusinessStartDate,
    '' AS OwnerJobTitle,
    CONCAT(cont.firstname, ' ', cont.lastname) AS OwnerName,
    Date_format(cont.dateofbirth,'%Y-%m-%d') AS OwnerDateOfBirth,
    ow.ownerId AS OwnerID,
    adr.Phone1 AS OwnerTelePhone,
    cont.contactId AS contactId,
    Date_format(ow.datebecameowner,'%Y-%m-%d') AS OwnerDateBecameOwner,
    cont.firstname AS ownerFirstName,
    cont.lastname AS ownerLastName,
    adr.Phone2 AS OwnerCellphone,
    ow.isauthorized AS OwnerIsAuthorized
FROM
    tb_owners ow
        LEFT JOIN
    tb_contacts cont ON cont.contactid = ow.contactid
        LEFT JOIN
    tb_addresses adr ON cont.addressid1 = adr.addressid
        LEFT JOIN
    lkp_tb_province pro ON pro.State = adr.State
WHERE
    ow.merchantid = iNmerchantId;
    
/*Processors*/
SELECT DISTINCT
    prlist.name AS ProcessorName,
    pro.processorNumber AS ProcessorNumber,
    Date_format(pro.firstProcessedDate,'%Y-%m-%d') AS FirstProcessedDate
FROM
    tb_processor pro
        JOIN
    lkp_tb_processorlist prlist ON pro.ProcessorTypeId = prlist.processorid
WHERE
    merchantid = iNmerchantId;


/* Owner credit info */
if (SELECT count(*) FROM tb_creditreports cr join tb_owners own on cr.MerchantID=own.MerchantID  join tb_contacts cnt 
    on own.contactid=cnt.ContactId WHERE cr.merchantid = iNmerchantId AND type = 'O' > 0) then

				SELECT DISTINCT
					CreditScore AS OwnerCreditScore,
					cnt.PassportNbr AS OwnerPassport,
					Occupation AS OwnerOccupation,
					Nationality AS OwnerNationality,
					errors AS OwnerErrors,
					RiskCategory AS OwnerRiskCategory,
					ProbabilityOfDefault AS OwnerProbabilityOfDefault,
					DATE_FORMAT(TimeofReport, '%Y-%m-%d') AS OwnerTimeOfReport,
					MONTHNAME(STR_TO_DATE(Month(MonthEvaualted), '%m')) AS OwnerMonthEvaluated
				FROM
					tb_creditreports cr 
					join 
					tb_owners own on 
					cr.MerchantID=own.MerchantID
					join tb_contacts cnt 
					on own.contactid=cnt.ContactId
				WHERE
					cr.merchantid = iNmerchantId AND type = 'O'  and cr.creditreportid = (Select creditreportid from tb_creditreports where merchantId=iNmerchantId order by creditreportid desc limit 1)
                     order by cr.creditreportid desc  limit 1 ;
else

/* Show blank values  */
				SELECT 	'' AS OwnerCreditScore,
					'' AS OwnerPassport,
					'' AS OwnerOccupation,
					'' AS OwnerNationality,
					'' AS OwnerErrors,
					'' AS OwnerRiskCategory,
					'' AS OwnerProbabilityOfDefault,
					'' AS OwnerTimeOfReport,
					'' AS OwnerMonthEvaluated;
end if;
/*
Owner Loan info
Price=OwedAmount-Loaned Amount
Buy rate - OwedAmount-Loaned Amount/100 #5
*/
if(SELECT count(*) FROM tb_creditreportAnalysis ana JOIN tb_creditreports cr ON cr.CreditReportID = ana.reportid WHERE
    ana.type = 1 AND cr.type = 'O' AND cr.merchantid = iNmerchantId > 0) then

			SELECT DISTINCT
				numberofLoans AS OwnerLoanNumberOfLoans,
				(sum(approvedcredit)-sum(owedamount)) AS OwnerLoanCreditAvailable,
				sum(owedamount) AS OwnerLoanOwedAmount,
				DATE_FORMAT(firstcreditdate, '%Y-%m-%d') AS OwnerLoanDateOfFirstCredit,
				sum(approvedcredit) AS OwnerLoanApprovedCredit,
				sum(amountinlegal) AS OwnerLoanAmountInLegal,
				sum(lateamount) AS OwnerLoanLateAmount,
				sum(monthlypayment) AS OwnerLoanMonthlyPayment
			FROM
				tb_creditreportAnalysis ana
					JOIN
				tb_creditreports cr ON cr.CreditReportID = ana.reportid
			WHERE
				ana.type = 1 AND cr.type = 'O'
					AND cr.merchantid = iNmerchantId and cr.creditreportid = (Select creditreportid from tb_creditreports where merchantId=iNmerchantId and type='o' order by creditreportid desc limit 1)
                     order by cr.creditreportid desc  limit 1 ;
else
SELECT 
				0 AS OwnerLoanNumberOfLoans,
				0.0 AS OwnerLoanCreditAvailable,
				0 AS OwnerLoanOwedAmount,
				'' AS OwnerLoanDateOfFirstCredit,
				0 AS OwnerLoanApprovedCredit,
				0 AS OwnerLoanAmountInLegal,
				0 AS OwnerLoanLateAmount,
				0 AS OwnerLoanMonthlyPayment;
end if;
   /* CreditCard*/

if(SELECT count(*) FROM tb_creditreportAnalysis ana JOIN tb_creditreports cr ON cr.CreditReportID = ana.reportid WHERE
			ana.type = 2 AND cr.type = 'O' AND cr.merchantid = iNmerchantId > 0) then

				   SELECT DISTINCT
					NumberofCreditCards AS OwnerCCLoanNumberOfLoans,
					(sum(approvedcredit)-sum(owedamount)) AS OwnerCCCreditAvailable,
					sum(owedamount) AS OwnerCCOwedAmount,
					DATE_FORMAT(firstcreditdate, '%Y-%m-%d') AS OwnerCCDateOfFirstCredit,
					sum(approvedcredit) AS OwnerCCApprovedCredit,
					sum(amountinlegal) AS OwnerCCAmountInLegal,
					sum(lateamount) AS OwnerCCLateAmount,
					sum(monthlypayment) AS OwnerCCMonthlyPayment
				FROM
					tb_creditreportAnalysis ana
						JOIN
					tb_creditreports cr ON cr.CreditReportID = ana.reportid
				WHERE
					ana.type = 2 AND cr.type = 'O'  and cr.creditreportid = (Select creditreportid from tb_creditreports where merchantId=iNmerchantId and type='o' order by creditreportid desc limit 1)
						AND cr.merchantid = iNmerchantId  order by cr.creditreportid desc  limit 1 ;
else
					SELECT 
					0 AS OwnerCCLoanNumberOfLoans,
					0.0 AS OwnerCCCreditAvailable,
					0 AS OwnerCCOwedAmount,
					'' AS OwnerCCDateOfFirstCredit,
					0 AS OwnerCCApprovedCredit,
					0 AS OwnerCCAmountInLegal,
					0 AS OwnerCCLateAmount,
					0 AS OwnerCCMonthlyPayment;

end if;
/*Other */

if(SELECT count(*) FROM tb_creditreportAnalysis ana JOIN tb_creditreports cr ON cr.CreditReportID = ana.reportid WHERE
					ana.type = 3 AND cr.type = 'O' 	AND cr.merchantid = iNmerchantId > 0) then
	
				SELECT DISTINCT
						numberofOthers AS OwnerOtherNumberOfLoans,
						(sum(approvedcredit) -sum(owedamount)) AS OwnerOtherCreditAvailable,
						sum(owedamount) AS OwnerOtherOwedAmount,
						DATE_FORMAT(firstcreditdate, '%Y-%m-%d') AS OwnerOtherDateOfFirstCredit,
						sum(approvedcredit) AS OwnerOtherApprovedCredit,
						sum(amountinlegal) AS OwnerOtherAmountInLegal,
						sum(lateamount) AS OwnerOtherLateAmount,
						sum(monthlypayment) AS OwnerOtherMonthlyPayment
					FROM
						tb_creditreportAnalysis ana
							JOIN
						tb_creditreports cr ON cr.CreditReportID = ana.reportid
					WHERE
						ana.type = 3 AND cr.type = 'O' and cr.creditreportid = (Select creditreportid from tb_creditreports where merchantId=iNmerchantId and type='o' order by creditreportid desc limit 1)
							AND cr.merchantid = iNmerchantId  order by cr.creditreportid desc  limit 1 ;
else
					SELECT 
					0 AS OwnerOtherNumberOfLoans,
					0.0 AS OwnerOtherCreditAvailable,
					0 AS OwnerOtherOwedAmount,
					'' AS OwnerOtherDateOfFirstCredit,
					0 AS OwnerOtherApprovedCredit,
					0 AS OwnerOtherAmountInLegal,
					0 AS OwnerOtherLateAmount,
					0 AS OwnerOtherMonthlyPayment;

end if;
/*CompanyCreditInfo*/

if(SELECT count(*) FROM  tb_creditreports WHERE  merchantid = iNmerchantId AND type = 'C'> 0 ) then
			SELECT DISTINCT
				CreditScore AS CompanyCreditScore,
				rnc AS CompanyCreditRNC,
				errors AS CompanyCreditErrors,
				RiskCategory AS CompanyCreditRiskCategory,
				ProbabilityOfDefault AS CompanyCreditProbabilityOfDefault,
				DATE_FORMAT(TimeofReport, '%Y-%m-%d') AS CompanyCreditTimeOfReport,
				DATE_FORMAT(MonthEvaualted, '%Y-%m-%d') AS CompanyCreditMonthEvaluated
			FROM
				tb_creditreports
			WHERE
				merchantid = iNmerchantId AND type = 'C'  
				order by creditreportid desc  limit 1 ;
else
			SELECT 
				'' AS CompanyCreditScore,
				'' AS CompanyCreditRNC,
				'' AS CompanyCreditErrors,
				'' AS CompanyCreditRiskCategory,
				'' AS CompanyCreditProbabilityOfDefault,
				'' AS CompanyCreditTimeOfReport,
				'' AS CompanyCreditMonthEvaluated;
end if;
     /* Loans */
if(SELECT count(*) FROM tb_creditreportAnalysis ana JOIN tb_creditreports cr ON cr.CreditReportID = ana.reportid WHERE
    ana.type = 1 AND cr.type = 'C' AND cr.merchantid = iNmerchantId > 0) then
			SELECT DISTINCT
				numberofLoans AS CompanyLoansNumberOfLoans,
				(sum(approvedcredit)-sum(owedamount)) AS CompanyLoansCreditAvailable,
				sum(owedamount) AS CompanyLoansOwedAmount,
				DATE_FORMAT(firstcreditdate, '%Y-%m-%d') AS CompanyLoansDateOfFirstCredit,
				sum(approvedcredit) AS CompanyLoansApprovedCredit,
				sum(amountinlegal) AS CompanyLoansAmountInLegal,
				sum(lateamount) AS CompanyLoansLateAmount,
				sum(monthlypayment) AS CompanyLoansMonthlyPayment
			FROM
				tb_creditreportAnalysis ana
					JOIN
				tb_creditreports cr ON cr.CreditReportID = ana.reportid
			WHERE
				ana.type = 1 AND cr.type = 'C'
					AND cr.merchantid = iNmerchantId   and cr.creditreportid = (Select creditreportid from tb_creditreports where merchantId=iNmerchantId   and type='C' order by creditreportid desc limit 1)
					order by cr.creditreportid desc  limit 1 ;
else
			SELECT 
			0 AS CompanyLoansNumberOfLoans,
			0.0 AS CompanyLoansCreditAvailable,
			0 AS CompanyLoansOwedAmount,
			'' AS CompanyLoansDateOfFirstCredit,
			0 AS CompanyLoansApprovedCredit,
			0 AS CompanyLoansAmountInLegal,
			0 AS CompanyLoansLateAmount,
			0 AS CompanyLoansMonthlyPayment;
end if;    
    /*CompanyCreditCard*/

if(SELECT count(*) FROM tb_creditreportAnalysis ana JOIN tb_creditreports cr ON cr.CreditReportID = ana.reportid
WHERE  ana.type = 2 AND cr.type = 'C' AND cr.merchantid = iNmerchantId > 0) then
			SELECT DISTINCT
				NumberofCreditCards AS CompanyCCCreditCard,
				0 AS CompanyCCNumberOfLoans,
				(sum(approvedcredit)-sum(owedamount))  AS CompanyCCCreditAvailable,
				sum(owedamount) AS CompanyCCOwedAmount,
				DATE_FORMAT(firstcreditdate, '%Y-%m-%d') AS CompanyCCDateOfFirstCredit,
				sum(approvedcredit) AS CompanyCCApprovedCredit,
				sum(amountinlegal) AS CompanyCCAmountInLegal,
				sum(lateamount) AS CompanyCCLateAmount,
				sum(monthlypayment) AS CompanyCCMonthlyPayment
			FROM
				tb_creditreportAnalysis ana
					JOIN
				tb_creditreports cr ON cr.CreditReportID = ana.reportid
			WHERE
				ana.type = 2 AND cr.type = 'C'
					AND cr.merchantid = iNmerchantId   and cr.creditreportid = (Select creditreportid from tb_creditreports where merchantId=iNmerchantId  and type='C' order by creditreportid desc limit 1)
					order by cr.creditreportid desc  limit 1 ;
else

			SELECT 
				0 AS CompanyCCCreditCard,
				0 AS CompanyCCNumberOfLoans,
				0.0 AS CompanyCCCreditAvailable,
				0 AS CompanyCCOwedAmount,
				'' AS CompanyCCDateOfFirstCredit,
				0 AS CompanyCCApprovedCredit,
				0 AS CompanyCCAmountInLegal,
				0 AS CompanyCCLateAmount,
				0 AS CompanyCCMonthlyPayment;
				
				
end if;
    /*CompanyOthers*/
if(SELECT count(*) FROM tb_creditreportAnalysis ana  JOIN    tb_creditreports cr ON cr.CreditReportID = ana.reportid
WHERE    ana.type = 3 AND cr.type = 'C'  AND cr.merchantid = iNmerchantId > 0) then

			SELECT DISTINCT
				numberofOthers AS CompanyOthersNumberOfLoans,
				(sum(approvedcredit)-sum(owedamount)) AS CompanyOthersCreditAvailable,
				sum(owedamount) AS CompanyOthersOwedAmount,
				DATE_FORMAT(firstcreditdate, '%Y-%m-%d') AS CompanyOthersDateOfFirstCredit,
				sum(approvedcredit) AS CompanyOthersApprovedCredit,
				sum(amountinlegal) AS CompanyOthersAmountInLegal,
				sum(lateamount) AS CompanyOthersLateAmount,
				sum(monthlypayment) AS CompanyOthersMonthlyPayment
			FROM
				tb_creditreportAnalysis ana
					JOIN
				tb_creditreports cr ON cr.CreditReportID = ana.reportid
			WHERE
				ana.type = 3 AND cr.type = 'C'
					AND cr.merchantid = iNmerchantId   and cr.creditreportid = (Select creditreportid from tb_creditreports where merchantId=iNmerchantId  and type='C' order by creditreportid desc limit 1)
					order by cr.creditreportid desc  limit 1 ;

else
			SELECT 
			0 AS CompanyOthersNumberOfLoans,
			0.0 AS CompanyOthersCreditAvailable,
			0 AS CompanyOthersOwedAmount,
			'' AS CompanyOthersDateOfFirstCredit,
			0 AS CompanyOthersApprovedCredit,
			0 AS CompanyOthersAmountInLegal,
			0 AS CompanyOthersLateAmount,
			0 AS CompanyOthersMonthlyPayment;

end if;
    /*BankStatements*/

SELECT DISTINCT
    StatementMonthId AS Month,
    Statementyear AS Year,
    amount AS Amount
FROM
    tb_BankStatements bnk
        JOIN
    tb_bankdetails bnkdt ON bnk.BankDetailId = bnkdt.BankDetailId
WHERE
    bnkdt.MerchantId = iNmerchantId;
    
    
    
/*CreditCardActivity*/
SELECT DISTINCT
    cca.MerchantId AS CCAMerchantId,
    cca.ProcessorId AS CCAProcessorId,
    cca.ProcessorMerchantId AS CCAProcessorMerchantId,
    cca.AcctivityTypeId AS CCAActivityTypeId,
    cca.CurrencyId AS CCACurrencyId,
    cca.TotalAmount AS CCACCVTotalAmount,
    cca.TotalTickets AS CCACCVTotalTickets,
    AVG(ccv.totalamount) AS CCAAverageMonthlyCreditCardSales,
    AVG(ccv.totaltickets) AS CCAAverageMonthlyTickets,
    cnt.AverageMCCV AS CCAAverageUsedInTheCalculator,
    0 AS CCACCVEstimatesCreditCardSales,
    ind.name AS CCAProcessorIndustryTypeId,
    cca.MerchantId
FROM
    tb_creditcardactivity cca
        JOIN
    tb_CreditCardVolumes ccv ON cca.MerchantId = ccv.merchantId
    join tb_contracts cnt on cnt.merchantid=cca.merchantid
   left join 
    lkp_tb_industrytypes ind on cca.IndustryId=ind.IndustryTypeId
WHERE
    cca.merchantid = iNmerchantId
GROUP BY cca.merchantid
Order by ccv.processorId,ccv.year,ccv.month;

SELECT DISTINCT
    @rownum := @rownum + 1 AS orders,# ccv.month AS orders,
    concat(MONTHNAME(STR_TO_DATE(ccv.month, '%m')),'-',ccv.year)AS CCACCVPeriod,
    sum(ccv.totalAmount) AS CCACCVAmount,
    sum(ccv.totaltickets) AS CCACCVTickets,
    ccv.merchantId
FROM
    (Select * from tb_CreditCardVolumes where merchantId= iNmerchantId order by year,month) ccv,  (SELECT @rownum := 0) r
WHERE
     ccv.contractId=(Select max(contractId) from tb_contracts where merchantId=ccv.merchantId)
group by Year,month
Order by ccv.processorId,ccv.year,ccv.month;

/*
SELECT DISTINCT
    @rownum := @rownum + 1 AS orders,# ccv.month AS orders,
    concat(MONTHNAME(STR_TO_DATE(ccv.month, '%m')),'-',ccv.year)AS CCACCVPeriod,
    sum(ccv.totalAmount) AS CCACCVAmount,
    sum(ccv.totaltickets) AS CCACCVTickets,
    ccv.merchantId
FROM
    tb_CreditCardVolumes ccv,  (SELECT @rownum := 0) r
WHERE
    ccv.merchantid = iNmerchantId AND ccv.contractId=(Select max(contractId) from tb_contracts where merchantId=ccv.merchantId)
group by Year,month
Order by ccv.processorId,ccv.year,ccv.month;   */
end;