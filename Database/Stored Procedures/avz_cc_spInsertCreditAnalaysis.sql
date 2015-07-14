drop procedure if exists avz_cc_spInsertCreditAnalaysis;
delimiter $$

 create procedure avz_cc_spInsertCreditAnalaysis(
 iNreportId  bigint,
iNnumberofloans bigint,
iNloancredit double,
iNloanowedamount double,
iNfirstcreditdate datetime,
iNapprovedcredit double,
iNloanamountinlegal double,
iNloanlateamount double,
iNloanmonthlypayment double,
iNtype smallint,
iNcurrency varchar(45),
iNDate varchar(100)
)

begin 
declare _loanowedamount,_approvedcredit,_amountinlegal,_lateamount,_monthlypayment,_exchangeRate double default 0.0;

if(iNcurrency='U.S. $') then
begin 
select value into _exchangeRate from tb_configuration where config='exchangeRate';
set _loanowedamount=(iNloanowedamount * _exchangeRate);
set _approvedcredit=(iNapprovedcredit * _exchangeRate);
set _amountinlegal=(iNloanamountinlegal * _exchangeRate);
set _lateamount=(iNloanlateamount * _exchangeRate);
set _monthlypayment=(iNloanmonthlypayment * _exchangeRate);
end;

else

begin
set _loanowedamount=iNloanowedamount;
set _approvedcredit=iNapprovedcredit;
set _amountinlegal=iNloanamountinlegal;
set _lateamount=iNloanlateamount;
set _monthlypayment=iNloanmonthlypayment;
end;
end if;

/*if exists(select 1 from tb_creditreportAnalysis where reportid=iNreportId)=0 then
begin */

insert into tb_creditreportAnalysis(reportId, 
numberofaccounts,
owedamount,
firstcreditdate,
approvedcredit,
amountinlegal,
lateamount,
monthlypayment,
type,currency,monthconsolidate)
values 
(
 iNreportId  ,
iNnumberofloans ,
_loanowedamount ,
DATE_FORMAT(iNfirstcreditdate,'%Y-%m-%d') ,
_approvedcredit ,
_amountinlegal ,
_lateamount ,
_monthlypayment ,
iNtype ,iNcurrency, iNDate
);


/*end;
else
set sql_safe_updates=0;
update tb_creditreportAnalysis 
set 
numberofaccounts=iNnumberofloans,
owedamount=_loanowedamount,
firstcreditdate=DATE_FORMAT(iNfirstcreditdate,'%Y-%m-%d'),
approvedcredit=_approvedcredit,
amountinlegal=_amountinlegal,
lateamount=_lateamount,
monthlypayment=iNloanmonthlypayment,
type=iNtype,
currency=iNcurrency
where reportId=iNreportId;
end if; */

end $$

delimiter ;