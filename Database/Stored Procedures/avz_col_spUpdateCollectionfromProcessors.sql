#call avz_col_spUpdateCollectionfromProcessors();
drop procedure if exists avz_col_spUpdateCollectionfromProcessors;
delimiter $$

create procedure avz_col_spUpdateCollectionfromProcessors()
begin

DECLARE pmerchantId INT;
DECLARE pcontractID INT;
DECLARE pretrievalRate double;
DECLARE pactivityDays BIGINT;
DECLARE pcollectionAmount,pRealTurn double;

DECLARE _finished INTEGER DEFAULT 0;
declare _exists int;

DECLARE Collections CURSOR FOR 
SELECT 
    C.contractID,C.MerchantId,
     ((DATEDIFF(DATE_ADD(C.FundingDate, INTERVAL C.Turn MONTH),C.FundingDate))-(C.Ownedamount / (-1*avg(PA.AppliedAmount))))/(DATEDIFF(DATE_ADD(C.FundingDate, INTERVAL C.Turn MONTH),C.FundingDate))*100,
    CASE
        WHEN
            (DATEDIFF(CURDATE(),
                    (SELECT 
                            MAX(PA.processeddate)
                        FROM
                            tb_processoractivities
                        WHERE
                            contractid = C.contractid
                        ORDER BY processeddate DESC
                        LIMIT 1))) = 1
        THEN
            0
        ELSE (DATEDIFF(CURDATE(),
                (SELECT 
                        MAX(PA.processeddate)
                    FROM
                        tb_processoractivities
                    WHERE
                        contractid = C.contractid
                    ORDER BY processeddate DESC
                    LIMIT 1)))
    END,
    C.ownedAmount-C.paidamount,
	TIMESTAMPDIFF(MONTH,C.FundingDate,Date_Add(C.FundingDate, Interval (C.Ownedamount / (-1*avg(PA.AppliedAmount))) DAY))  as RealTurn
FROM
    tb_contracts C
        JOIN
    tb_processoractivities PA ON C.contractid = PA.contractid
WHERE
    C.FundingDate IS NOT NULL
GROUP BY C.contractID;


DECLARE CONTINUE HANDLER FOR NOT FOUND SET _finished = 1;

OPEN Collections;
 
GETDATA: loop
fetch Collections into pContractID,pmerchantId,pretrievalRate,pactivityDays,pcollectionAmount,pRealTurn;
select 
    pContractID,
    pmerchantId,
    pretrievalRate,
    pactivityDays,
    pcollectionAmount;
IF _finished = 1 THEN 
LEAVE GETDATA;
END IF;

IF EXISTS (SELECT 1 FROM tb_merchantretrievalratio WHERE MerchantID=pmerchantId and ContractID=pcontractID)=0 then
insert into tb_merchantretrievalratio(MerchantID,ContractID,RetrievalSpeed,DaysWithoutActivity)
 values (pmerchantId,pcontractID,pretrievalRate,pactivityDays);
else
SET SQL_SAFE_UPDATES = 0;
UPDATE tb_merchantretrievalratio 
SET 
    RetrievalSpeed = pretrievalRate,
    DaysWithoutActivity = pactivityDays
WHERE
    MerchantID = pmerchantId
        AND ContractID = pcontractID;
end if;

update tb_merchants set statusid=case when pactivityDays<4 and pretrievalRate<0 then 10004 when pactivityDays<4 and pretrievalRate>0 then 10003 else 10002 end
Where merchantid=pmerchantId;

if (pactivityDays>4) then 
IF EXISTS (SELECT 1 FROM tb_collections WHERE MerchantID=pmerchantId and ContractID=pcontractID and statusid  in (180001,180002))=0 then
insert into tb_collections(MerchantID,ContractID,CollectionTypeID,StatusID,DefaultedDate,AmountinCollection,insertUserId,Insertdate,startdate)
values(pmerchantId,pcontractID,400001,180001,now(),pcollectionAmount,1,now(),now());
update tb_merchants set statusid=case when pactivityDays>4 and pactivityDays<30  then 10005 when pactivityDays>30 and pretrievalRate>0 then 10006 end
Where merchantid=pmerchantId;
else
Select concat('udpating status',case when pactivityDays<30  then 10005 when pactivityDays>30 and pretrievalRate>0 then 10006 end);
update tb_merchants set statusid=case when pactivityDays<30  then 10005 when pactivityDays>30 and pretrievalRate>0 then 10006 end
Where merchantid=pmerchantId;
end if;
end if;
IF EXISTS (Select CC.CollectionID,CC.MerchantID,C.ContractID from tb_collections CC JOIN tb_merchantretrievalratio mr on CC.MerchantID=mr.MerchantID and CC.StatusID in (180001
,180002) and CC.CollectionTypeID=400001 JOIN tb_contracts C on C.ContractID=CC.ContractID and C.statusid=20007
where mr.DaysWithoutActivity=0 and CC.MerchantID=pmerchantId and C.ContractID=pcontractID)=1  then
#Select 'Update worked';
update tb_collections set statusid=180008 where MerchantID=pmerchantId and ContractID=pcontractID and collectiontypeid=400001 and statusid in (180001,180002);
#else
#Select 'Update not worked';
end if;

update tb_contracts set realturn=pRealTurn where contractID=pcontractID;

END LOOP GETDATA;
CLOSE Collections;
end;