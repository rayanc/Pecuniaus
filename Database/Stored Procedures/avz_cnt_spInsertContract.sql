drop procedure if exists avz_cnt_spInsertContract;
delimiter $$
create procedure avz_cnt_spInsertContract(
iNmerchantId bigint,
iNstatusId bigint,
iNcurrencyId bigint,
iNinsertUserId bigint,
iNinsertDate datetime,
iNloanAmount double,
iNcontractTypeId bigint,
iNcontractId bigint,
iNpSalesRepId bigint,
iNsSalesRepId bigint,
iNtypeofadvanceid bigint,
iNannualSalCalcfile varchar(50)

)
begin
 declare prefixes varchar(20);
 declare newcontractid bigint;
 if iNcontractTypeId=13100 then 
 SET prefixes='CNT';
 else
 SET prefixes='REN';
 end if;
if ifnull(iNcontractId,0)=0 then 
begin
INSERT INTO `tb_contracts`
(`ContractNumber`,
`creationDate`,
`MerchantId`,
`StatusId`,
`currencyId`,
`InsertUserId`,
`InsertDate`,
`loanedamount`,
`ContractTypeId`,
`TypeOfAdvances`,
`AnnualSalesCalcFile`
)
VALUES
(
'',now(),iNmerchantId,iNstatusId,1,iNinsertUserId,now(),iNloanAmount,13100, iNtypeofadvanceid,iNannualSalCalcfile);

UPDATE tb_contracts 
SET 
    ContractNumber = CONCAT(prefixes, LAST_INSERT_ID())
WHERE
    contractid = LAST_INSERT_ID();
set newcontractid=LAST_INSERT_ID();

insert into tb_contractsalesrep
(contractId
,SalesRepId
, IsPrimary
)
values
(newcontractid
, iNpSalesRepId,
1);

if(iNsSalesRepId !=0) then
				insert into tb_contractsalesrep
				(contractId
				,SalesRepId
				, IsPrimary
				)
				values
				(newcontractid
				, iNsSalesRepId,
				0);
			end if;
end;
    else 
		update tb_contracts set  StatusId=iNstatusId, LoanedAmount=iNloanAmount,AnnualSalesCalcFile =iNannualSalCalcfile, TypeOfAdvances=iNtypeofadvanceid where contractid= iNcontractId;
		set newcontractid=iNcontractId;
if ((select count(*) from tb_contractsalesrep where contractid= iNcontractId) > 0) then
	update tb_contractsalesrep set SalesRepId=iNpSalesRepId where IsPrimary=1 and contractid= iNcontractId;
	update tb_contractsalesrep set SalesRepId=iNsSalesRepId where IsPrimary=0 and contractid= iNcontractId;
else
	insert into tb_contractsalesrep
	(contractId
	,SalesRepId
	, IsPrimary
	)
	values
	(newcontractid
	, iNpSalesRepId,
	1);
			if(iNsSalesRepId !=0) then
				insert into tb_contractsalesrep
				(contractId
				,SalesRepId
				, IsPrimary
				)
				values
				(newcontractid
				, iNsSalesRepId,
				0);
			end if;
end if;
end if;



end;

