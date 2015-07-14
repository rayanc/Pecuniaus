#call avz_cc_spCalculateAverageMCCV();
drop procedure if exists avz_cc_spCalculateAverageMCCV;
delimiter $$ 

create procedure avz_cc_spCalculateAverageMCCV( )
begin

DECLARE _finished INTEGER DEFAULT 0;
DECLARE _iNmerchantId BIGINT DEFAULT 0;
DECLARE _avgmmccv NUMERIC(18,2) DEFAULT 0.00;

DECLARE RetrieveAggregateVolume CURSOR FOR 
/*SELECT 
    CAST(AVG(totalAmount) AS DECIMAL (18 , 4 )), merchantid,avg(totaltickets)
FROM
    tb_CreditCardVolumes
GROUP BY merchantid;*/

SELECT 
   cast((sum(totalAmount) /sum(totaltickets)) as decimal(18,4)) as mccv ,merchantid
FROM
    tb_CreditCardVolumes
GROUP BY merchantid;

DECLARE CONTINUE HANDLER FOR NOT FOUND SET _finished = 1;

OPEN RetrieveAggregateVolume;

GETDATA: loop
fetch RetrieveAggregateVolume into _avgmmccv,_iNmerchantId;
SELECT _iNmerchantId, _avgmmccv;

#IF NO DATA IS FOUND THEN QUIT
IF _finished = 1 THEN 
LEAVE GETDATA;
END IF;

SET SQL_SAFE_UPDATES = 0;

UPDATE tb_contracts 
SET 
    AverageMCCV = _avgmmccv
WHERE
    merchantid = _iNmerchantId;

END LOOP GETDATA;

 CLOSE RetrieveAggregateVolume;

end;