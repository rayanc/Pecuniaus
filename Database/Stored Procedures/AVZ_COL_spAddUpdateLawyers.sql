-- ================================================
-- Name: AVZ_COL_spAddUpdateLawyers
-- 
-- Description : To add update lawyers for collections
-- 
-- Parameters : None 
-- 
-- Author  : Sachin Verma
-- 
-- Creation Date : 05-11-2014
-- ================================================
Drop procedure if exists AVZ_COL_spAddUpdateLawyers;

DELIMITER $$
CREATE PROCEDURE AVZ_COL_spAddUpdateLawyers
(iNLawyersXml text,
 iNDeletedLawyers varchar(1000)
)
Begin
	declare pRowIndex int unsigned default 1;   
    declare pRowCount int unsigned;

IF NULLIF(iNLawyersXml, '') IS Not NULL then

	CREATE TEMPORARY TABLE Lawyers (
    LawId bigint, 
    First varchar(5000),
	Last varchar(5000),
	Company varchar(5000),
	InsertUser bigint
    );

-- calculate the number of row elements.   
     set pRowCount  = extractValue(iNLawyersXml, 'count(//lawyers)');
    
	 while pRowIndex <= pRowCount do
      insert into  Lawyers
      select 
             coalesce(nullif(extractvalue(iNLawyersXml, '//child::*[$pRowIndex]/@LawyerId'), ''),0)
           , coalesce(nullif(extractvalue(iNLawyersXml, '//child::*[$pRowIndex]/@FirstName'), ''),0)
           , coalesce(nullif(extractvalue(iNLawyersXml, '//child::*[$pRowIndex]/@LastName'), ''),0)
		   , coalesce(nullif(extractvalue(iNLawyersXml, '//child::*[$pRowIndex]/@CompanyName'), ''),0)
		   , coalesce(nullif(extractvalue(iNLawyersXml, '//child::*[$pRowIndex]/@InsertUserId'), ''),0);
		   
      set pRowIndex = pRowIndex + 1;

	 end while;

	insert into  tb_lawyers(LawyerId,FirstName, LastName, CompanyName,InsertDate,InsertUserId)
	select LawId,First,Last,Company,now(),InsertUser from Lawyers
	on duplicate key update LawyerId = LawId, FirstName = First,  LastName = Last,  CompanyName = Company,  InsertDate = now(), InsertUserId=InsertUser;

	Drop Table Lawyers;

end if;

	IF NULLIF(iNDeletedLawyers, '') IS Not NULL then
		SET @SQL = CONCAT('DELETE FROM tb_lawyers WHERE LawyerId in (', iNDeletedLawyers, ')');
		PREPARE stmt FROM @SQL;
		EXECUTE stmt;
		DEALLOCATE PREPARE stmt;
	end if;
END;
$$
DELIMITER ;

