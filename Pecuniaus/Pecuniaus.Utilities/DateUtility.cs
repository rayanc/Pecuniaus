using System;

namespace Pecuniaus.Utilities
{
    public static class DateUtility
    {
        private static string[] _unitsMap = new[]
    {
    "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
    "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
    };

        private static string[] _tensMap = new[]
    {
    "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
    };

        private static string[] _days = new string[]
    {
    "First", "Second", "Thrid", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth",
    "Eleventh", "Twelfth", "Thirteenth", "Fourteenth", "Fifteenth", "Sixtenth", "Seventeenth",
    "Eighteenth", "Nineteenth", "Twentieth", "Twenty-First", "Twenty-Second", "Twenty-Third",
    "Twenty-Fourth", "Twenty-Fifth", "Twenty-Sixth", "Twenty-Seventh", "Twenty-Eighth",
    "Twenty-Ninth", "Thirtieth", "Thirty-First"
    };

        private static string[] _month = new string[]
    {
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
    };

        private static string[] _shortMonth = new string[]
    {
    "Jan", "Feb", "Mar", "Apr", "May", "Jun",
    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    };

        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0) return "Minus " + NumberToWords(Math.Abs(number)); string words = ""; if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20) words += _unitsMap[number];
                else
                {
                    words += _tensMap[number / 10]; if ((number % 10) > 0)
                        words += "-" + _unitsMap[number % 10];
                }
            }

            return words;
        }

        public static string GetDayWord(int day)
        {
            day--;

            if (day < 0 || day >= _days.Length)
                return string.Empty;

            return _days[day];
        }

        public static string GetMonthWord(int month)
        {
            month--;

            if (month < 0 || month >= _month.Length)
                return string.Empty;

            return _month[month];
        }

        public static string GetShortMonthWord(int month)
        {
            month--;

            if (month < 0 || month >= _shortMonth.Length)
                return string.Empty;

            return _shortMonth[month];
        }

        public static string GetYearWord(int year)
        {
            if (year < 0 || year > 9999)
            {
                return string.Empty;
            }
            else
            {
                return NumberToWords(year);
            }
        }

        public static string GetWord(DateTime dateTime)
        {
            return GetWord(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static string GetWord(int year, int month, int day)
        {
            string word = string.Empty;

            string dayWord = GetDayWord(day);
            string monthWord = GetMonthWord(month);
            string yearWord = GetYearWord(year);

            return string.Format(@"{0} of {1} {2}", dayWord, monthWord, yearWord);
        }
    }

}
