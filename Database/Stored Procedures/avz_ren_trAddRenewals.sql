DROP TRIGGER IF EXISTS avz_ren_trAddRenewals;
DELIMITER $$
CREATE TRIGGER avz_ren_trAddRenewals 
AFTER UPDATE ON tb_contracts
FOR EACH ROW
  BEGIN		
		
		Declare loadAmount double;
		declare paidAmount double;
		declare paidPercentagePaid double;
	
		Set loadAmount = NEW.OwnedAmount;
		Set paidAmount = New.PaidAmount;		
		set paidPercentagePaid = (paidAmount*100)/loadAmount;
		#set paidPercentagePaid = 50;
	If ( NEW.StatusId = 20007 And paidPercentagePaid > 60) then
	Begin
	
		IF NOT EXISTS (SELECT 1 FROM tb_renewalslist WHERE ContractId = NEW.contractId) THEN
		Begin
		INSERT INTO `tb_renewalslist`
				(`ContractID`,
				`MerchantId`,
				`StatusId`,
				`RenewalEligibleDate`,
				`AssignedUserID`,	
				`InsertUserId`,
				`InsertDate`		
				)
				Values( New.ContractId,New.MerchantId,New.StatusId,now(),1,New.InsertUserId,New.InsertDate);
		End;	
		END IF;
		End;
	End if ; 

  END $$
DELIMITER ;