using System;
using System.Collections.Generic;
using System.Linq;
//using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary;
using System.Data.Common;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Bridge.DataAccess
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static void Main()
        {
           string str= new Program().GetCustomerList();
        }
        public string GetCustomerList()
        {

            // Create the Database object, using the default 
            // database service as described above.
            Database db = DatabaseFactory.CreateDatabase("AVZ");
            // Define SQL query to retrieve the data.
            string sqlCommand = "Select * From dbo_lkp_tb_addresstype";
            // Create ADO.NET DbCommand object
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            // Create intermediate data holder
            StringBuilder readerData = new StringBuilder();
            // DataReader that will hold the returned results
            // The ExecuteReader call will request the connection to be closed upon
            // the closing of the DataReader. The DataReader will be closed
            // automatically when it is disposed.
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                // Iterate through DataReader
                while (dataReader.Read())
                {
                    // Get the value of the 'Name' column in the DataReader
                    readerData.Append(dataReader["addresstype"]);
                    readerData.Append(Environment.NewLine);
                }
            }
            return readerData.ToString();
        }
    }
}
