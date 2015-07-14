DROP TRIGGER IF EXISTS avz_cnt_trgContracts;
DELIMITER $$
CREATE TRIGGER avz_cnt_trgContracts AFTER UPDATE ON tb_contracts
FOR EACH ROW
  BEGIN
  
  IF(NEW.fundingDate!=OLD.fundingDate) then
  
   INSERT INTO tb_contracthistory(ContractId,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.ContractId,now(),'fundingDate',old.fundingDate,new.fundingDate,'');
    END IF;
    
   IF(NEW.Retention!=OLD.Retention) then
   
   INSERT INTO tb_contracthistory(ContractId,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.ContractId,now(),'Retention',old.Retention,new.Retention,'');
   
    END IF;
    
   IF(NEW.SalesRepID!=OLD.SalesRepID) then
   
   INSERT INTO tb_contracthistory(ContractId,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.ContractId,now(),'OwnerId',old.OwnerId,new.OwnerId,'');
   
    END IF;
    
  IF(NEW.StatusId!=OLD.StatusId) then
  
   INSERT INTO tb_contracthistory(ContractId,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.ContractId,now(),'StatusId',old.StatusId,new.StatusId,'');
   
    END IF;  
    
    IF(NEW.ContractTypeId!=OLD.ContractTypeId) then
  
   INSERT INTO tb_contracthistory(ContractId,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.ContractId,now(),'ContractTypeId',old.ContractTypeId,new.ContractTypeId,'');
   
    END IF;  
  END $$
DELIMITER ;