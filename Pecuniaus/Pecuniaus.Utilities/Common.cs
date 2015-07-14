
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace Pecuniaus.Utilities
{
    public class Common
    {
        public static IEnumerable<SelectListItem> GetMonthNames()
        {
            var months = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
                });
            }
            return months;
            //return DateTimeFormatInfo.CurrentInfo.MonthNames;
        }

        public static IEnumerable<SelectListItem> GetYearNames()
        {
            var years = new List<SelectListItem>();
            long currentYear = System.DateTime.Now.Year;
            for (int i = 1; i <= 10; i++)
            {
                years.Add(new SelectListItem
                {
                    Value = currentYear.ToString(),
                    Text = currentYear.ToString()
                });
                currentYear = currentYear - 1;
            }
            return years;
            //return DateTimeFormatInfo.CurrentInfo.MonthNames;
        }
        
        public static string GetMonthName(int id)
        {
            if (id > 0 && id <= 12)
            {
                return DateTimeFormatInfo.CurrentInfo.GetMonthName(id);
            }
            return string.Empty;
        }
    }

}
