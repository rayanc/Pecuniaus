DROP TRIGGER IF EXISTS avz_mrc_trgOffers;
DELIMITER $$
CREATE TRIGGER avz_mrc_trgOffers AFTER UPDATE ON tb_offers
FOR EACH ROW
  BEGIN
  
  IF(NEW.turn!=OLD.turn) then
  
   INSERT INTO tb_offerhistory(MERCHANTID,contractid,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,new.contractid,now(),'turn',old.turn,new.turn,'');
    END IF;
    
   IF(NEW.LoanAmount!=OLD.LoanAmount) then
   
   INSERT INTO tb_offerhistory(MERCHANTID,contractid,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,new.contractid,now(),'LoanAmount',old.LoanAmount,new.LoanAmount,'');
   
    END IF;
    
   IF(NEW.OwnedAmount!=OLD.OwnedAmount) then
   
   INSERT INTO tb_offerhistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'OwnedAmount',old.OwnedAmount,new.OwnedAmount,'');
   
    END IF;
    
  IF(NEW.Proportion!=OLD.Proportion) then
  
   INSERT INTO tb_offerhistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'Proportion',old.Proportion,new.Proportion,'');
   
    END IF;  
    
    IF(NEW.Retention!=OLD.Retention) then
  
   INSERT INTO tb_offerhistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'Retention',old.Retention,new.Retention,'');
   
    END IF;  
    
     IF(NEW.monthlyPayment!=OLD.monthlyPayment) then
  
   INSERT INTO tb_offerhistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'monthlyPayment',old.monthlyPayment,new.monthlyPayment,'');
   
    END IF;  
  
   IF(NEW.salestaken!=OLD.salestaken) then
  
   INSERT INTO tb_offerhistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'salestaken',old.salestaken,new.salestaken,'');
   
    END IF;  
  
   IF(NEW.yearly!=OLD.yearly) then
  
   INSERT INTO tb_offerhistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'yearly',old.yearly,new.yearly,'');
   
    END IF;  
  
  END $$
DELIMITER ;