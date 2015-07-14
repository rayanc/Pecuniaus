using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statements
{
    class Program
    {
        static void Main(string[] args)
        {
            if (DateTime.Today.Day == 1 || DateTime.Today.Day == 16)
            {
                //January month
                if (DateTime.Today.Month == 1)
                {
                    if (DateTime.Today.Day == 1)
                        new StatementsHelper().SendAutomatedStatements(new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month - 1, 16), new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month - 1, DateTime.DaysInMonth(DateTime.Today.Year - 1, DateTime.Today.Month - 1)));
                    if (DateTime.Today.Day == 16)
                        new StatementsHelper().SendAutomatedStatements(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 15));
                }
                else
                {
                    if (DateTime.Today.Day == 1)
                        new StatementsHelper().SendAutomatedStatements(new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, 16), new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month - 1)));
                    if (DateTime.Today.Day == 16)
                        new StatementsHelper().SendAutomatedStatements(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), new DateTime(DateTime.Today.Year, DateTime.Today.Month, 15));
                }
            }
        }
    }
}
