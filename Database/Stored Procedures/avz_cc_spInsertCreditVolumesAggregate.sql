#call avz_cc_spInsertCreditVolumesAggregate();
drop procedure if exists avz_cc_spInsertCreditVolumesAggregate;
delimiter $$ 

create procedure avz_cc_spInsertCreditVolumesAggregate( )
begin

DECLARE _finished INTEGER DEFAULT 0;
DECLARE _totalAmount decimal DEFAULT 0.00;
DECLARE _totalTickets BIGINT DEFAULT 0;
DECLARE _month INTEGER DEFAULT 0;
DECLARE _year INTEGER DEFAULT 0;
DECLARE _iNmerchantId BIGINT DEFAULT 0;
DECLARE _iNprocessorId BIGINT DEFAULT 0;
DECLARE _iNcontractId BIGINT DEFAULT 0;
DECLARE _exists BIGINT DEFAULT 0;

DECLARE RetrieveAggregateVolume CURSOR FOR 
select merchantid, month,year,convert(sum(totalamount),decimal),sum(totaltickets),ProcessorId,contractid 
from tb_creditcardactivity
Where insertdate >= current_date()
GROUP BY merchantId,MONTH,YEAR,ProcessorId,contractid;

DECLARE CONTINUE HANDLER FOR NOT FOUND SET _finished = 1;

OPEN RetrieveAggregateVolume;

GETDATA: loop
Select _finished;
fetch RetrieveAggregateVolume into _iNmerchantId,_month,_year,_totalAmount,_totalTickets,_iNprocessorId,_iNcontractId;

#IF NO DATA IS FOUND THEN QUIT
IF _finished = 1 THEN 
LEAVE GETDATA;
END IF;
 
SET SQL_SAFE_UPDATES = 0; 

#select id as existing from  tb_CreditCardVolumes where month=_month and year=_year and merchantId=_iNmerchantId and processorid=_iNprocessorId;

set _exists = EXISTS(
select id from  tb_CreditCardVolumes where month=_month and year=_year and merchantId=_iNmerchantId and processorid=_iNprocessorId);
select _exists;

IF ifnull(_exists,0)=0 THEN

select 'insert';
insert into tb_CreditCardVolumes
(merchantid,
month,
year,
totalamount,
totaltickets,
processorId,contractid
)
values (
_iNmerchantId,
_month,
_year,
_totalAmount,
_totalTickets,
_iNprocessorId,
_iNcontractId
);

ELSE
SELECT 'update';
 
UPDATE tb_CreditCardVolumes 
SET 
    totalamount = _totalAmount,
    totaltickets = _totalTickets
WHERE
    merchantid = _iNmerchantId and
    month = _month and
    year = _year 
    AND processorId = _iNprocessorId;

END IF;
END LOOP GETDATA;

 CLOSE RetrieveAggregateVolume;

end;