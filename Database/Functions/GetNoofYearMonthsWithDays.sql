drop function if exists Pecuniaus.GetNoofYearMonthsWithDays;
DELIMITER $$
 create FUNCTION Pecuniaus.GetNoofYearMonthsWithDays(FromDate Date,ToDate Date) RETURNS nvarchar(200)

    DETERMINISTIC
BEGIN

DECLARE years nvarchar(4);
DECLARE months nvarchar(2);
 DECLARE days nvarchar(50);
 declare result nvarchar(200);
 SELECT TIMESTAMPDIFF(YEAR
       , FromDate
       , ToDate
       ) into years;
       
   SELECT    TIMESTAMPDIFF(MONTH
       , FromDate
           + INTERVAL TIMESTAMPDIFF(YEAR, FromDate, ToDate) YEAR
       , ToDate
       ) into months;
       
     select TIMESTAMPDIFF(DAY
       , FromDate
           + INTERVAL TIMESTAMPDIFF(MONTH, FromDate, ToDate) MONTH
       ,  ToDate
       ) into days;
       
       
 set result= CONCAT( years , ' year(s) ' , months ,' month(s)', ' and ',days,'  day(s)') ;
 
 return (result);
 
END
