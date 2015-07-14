DROP TRIGGER IF EXISTS avz_cc_trgInsertcreditcardactivity;
DELIMITER $$
CREATE TRIGGER avz_cc_trgInsertcreditcardactivity AFTER INSERT ON tb_creditcardactivity
FOR EACH ROW
  BEGIN
  CALL avz_cc_spInsertCreditVolumesAggregate();
  END $$
DELIMITER ;

DROP TRIGGER IF EXISTS avz_cc_trgUpdatecreditcardactivity;
DELIMITER $$
CREATE TRIGGER avz_cc_trgUpdatecreditcardactivity AFTER  UPDATE ON tb_creditcardactivity
FOR EACH ROW
  BEGIN
 CALL avz_cc_spInsertCreditVolumesAggregate();
  END $$
DELIMITER ;
