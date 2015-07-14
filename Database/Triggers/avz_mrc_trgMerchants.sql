DROP TRIGGER IF EXISTS avz_mrc_trgMerchants;
DELIMITER $$
CREATE TRIGGER avz_mrc_trgMerchants AFTER UPDATE ON tb_merchants
FOR EACH ROW
  BEGIN
  
  IF(NEW.legalName!=OLD.legalName) then
  
   INSERT INTO tb_merchanthistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'legalName',old.legalname,new.legalname,'');
    END IF;
    
   IF(NEW.businessStartDate!=OLD.businessStartDate) then
   
   INSERT INTO tb_merchanthistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'businessStartDate',old.businessStartDate,new.businessStartDate,'');
   
    END IF;
    
   IF(NEW.OwnerId!=OLD.OwnerId) then
   
   INSERT INTO tb_merchanthistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'OwnerId',old.OwnerId,new.OwnerId,'');
   
    END IF;
    
  IF(NEW.StatusId!=OLD.StatusId) then
  
   INSERT INTO tb_merchanthistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'StatusId',old.StatusId,new.StatusId,'');
   
    END IF;  
    
    IF(NEW.industryTypeId!=OLD.industryTypeId) then
  
   INSERT INTO tb_merchanthistory(MERCHANTID,HistoryDate,Field,OldValue,NewValue,Reason)
   VALUES(new.merchantid,now(),'industryTypeId',old.industryTypeId,new.industryTypeId,'');
   
    END IF;  
  END $$
DELIMITER ;