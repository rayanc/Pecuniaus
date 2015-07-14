# Call AVZ_ren_spProcessRenewals
-- ================================================
drop procedure if exists AVZ_ren_spProcessRenewals;

DELIMITER $$
CREATE PROCEDURE AVZ_ren_spProcessRenewals()
BEGIN	

Declare _paidPercentage double default 0.0;

Select value into _paidPercentage from tb_configuration where Configsystem = 'RNW' and config='RenewalEligiblePercentage';	

drop table IF  exists FundedContracts;
CREATE TEMPORARY TABLE FundedContracts
(
LoanedAmount double,
paidAmount double,
paidPercentagePaid double,
ContractId bigint,
MerchantId bigint,
StatusId Int,
InsertUserId int,
CompanyID int default null
);


Insert Into FundedContracts (ContractId,MerchantId,LoanedAmount,PaidAmount,paidPercentagePaid,StatusId,InsertUserId,CompanyID)
Select C.ContractId,C.MerchantId,C.OwnedAmount,C.PaidAmount,(C.PaidAmount/C.OwnedAmount)*100 paidPercentagePaid,C.StatusId,C.InsertUserId,C.CompanyID 
From tb_contracts C Left JOIN tb_merchantretrievalratio mr on C.ContractId=mr.ContractID and C.MerchantId=mr.MerchantID #and ifnull(C.IsSpecialCase,0)=0
Where StatusId = 20007;
 #and mr.DaysWithoutActivity < 4 ;

#SELECT F.ContractId,F.MerchantId,F.StatusId,Now(),1,F.InsertUserId,Now() From  FundedContracts F
		#LEFT JOIN tb_renewalslist R on F.ContractID=R.ContractID And F.MerchantId=R.MerchantId
		#WHERE  R.MerchantId IS NULL And R.ContractID IS null And paidPercentagePaid >= _paidPercentage;

IF EXISTS (SELECT 1 FROM FundedContracts) THEN
Begin
INSERT INTO `tb_renewalslist`
				(`ContractID`,
				`MerchantId`,
				`StatusId`,
				`RenewalEligibleDate`,
				`AssignedUserID`,	
				`InsertUserId`,
				`InsertDate`,
				`CompanyID`
				)
	SELECT F.ContractId,F.MerchantId,F.StatusId,Now(),1,F.InsertUserId,Now(),F.companyID From  FundedContracts F
	LEFT JOIN tb_renewalslist R on F.ContractID=R.ContractID And F.MerchantId=R.MerchantId
	WHERE  R.MerchantId IS NULL And R.ContractID IS null AND paidPercentagePaid >= _paidPercentage;
End;	
END IF;
drop TEMPORARY TABLE FundedContracts;
End;
$$
DELIMITER ;