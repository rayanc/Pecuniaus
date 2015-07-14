using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pecuniaus.Utilities;
using RestSharp;

namespace Statements
{
    public class StatementsHelper
    {
        RestClient Client = null;
        string NFCStatementFileName = null;
        public StatementsHelper()
        {
        }
        public void SendAutomatedStatements(DateTime StatementsFrom, DateTime StatementsTo)
        {
            Client = new RestClient(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
            NFCStatementFileName = System.Configuration.ConfigurationManager.AppSettings["NCFStatementFileName"];

            string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + "Templates";

            SearchStatements(StatementsFrom, StatementsTo, Path, NFCStatementFileName);
        }
        private void SearchStatements(DateTime StatementsFrom, DateTime StatementsTo, string Path, string NFCStatementFileName)
        {
            string DestinationFileName=string.Empty;
            IList<MPMerchantStatementsDetailModel> objOutput = FilterStatements(StatementsFrom,StatementsTo);
            foreach (MPMerchantStatementsDetailModel obj in objOutput)
            {
                DestinationFileName = GenerateStatements(obj, Path, NFCStatementFileName);
                new Pecuniaus.Utilities.Email.Emailer().Send(obj.AddressDetail.Email, "NFC Statement", "NFC Statement", Path + "\\" + DestinationFileName);
            }
        }
        private string GenerateStatements(MPMerchantStatementsDetailModel objOutput, string Path, string NFCStatementFileName)
        {
            ExcelHelper EH = new ExcelHelper();
            string DestinationFileName = "NFC" + Guid.NewGuid() + ".xlsx";
            //string Path = @"D:\Development\DevelopmentWork\Projects2013\Pecuniaus\trunk\Pecuniaus\Pecuniaus.Web\Templates";
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, true, "E10", objOutput.TradeID.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E11", objOutput.FirmName);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E12", objOutput.RNC);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E13", objOutput.TradeName);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E14", objOutput.AddressDetail.Address);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E15", objOutput.AddressDetail.City);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E16", objOutput.AddressDetail.Phone1.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E17", objOutput.AddressDetail.Email);

            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G9", objOutput.StatementsFrom + " - " + objOutput.StatementsTo);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G11", objOutput.TotalCashRecieved.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G12", objOutput.TotalAdjustmentApplied.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G13", objOutput.TotalCashApplied.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G14", objOutput.TotalPriceApplied.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G15", objOutput.OutstandingBalance.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G16", objOutput.RightstoRecieve.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G17", objOutput.AdminCharges.ToString());

            int RowIndex = 22;

            foreach (MPMerchantStatementModel stmt in objOutput.StatementsDetail)
            {
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 4, stmt.TransactionDate.ToShortDateString());
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 5, stmt.ProcessorName);
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 6, stmt.TotalTransaction.ToString());
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 7, stmt.RetentionPercentage.ToString());
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 8, stmt.PaymentsReceived.ToString());

                RowIndex++;
            }
            return DestinationFileName;
        }

        private IList<MPMerchantStatementsDetailModel> FilterStatements(DateTime StatementsFrom, DateTime StatementsTo)
        {
            var request = new RestRequest(string.Format("merchantprofile/{0}/allstatements?StatementsFrom=" + StatementsFrom + "&StatementsTo=" + StatementsTo, 0), Method.GET);
            IRestResponse<List<MPMerchantStatementsDetailModel>> response = Client.Execute<List<MPMerchantStatementsDetailModel>>(request);
            IList<MPMerchantStatementsDetailModel> objOutput = response.Data;
            return objOutput;
        }
    }
}
