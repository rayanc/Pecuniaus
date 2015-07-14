using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Pecuniaus.Contract.Controllers
{
    public class CreditReportController : BaseController
    {
        public ActionResult GetReports()
        {
            var apiMethodProcessor = string.Format("creditreport/{0}/{1}", CurrentMerchantID, ContractID);
            var listcreditreport = BaseApiData.GetAPIResult<List<CreditReport>>(apiMethodProcessor, () => new List<CreditReport>());

            List<CreditReport> volumelistCompany = listcreditreport.Where(c => c.Type == "C").ToList();
            List<CreditReport> volumelistOwner = listcreditreport.Where(c => c.Type == "O").ToList();
            if (listcreditreport != null && listcreditreport.Count > 0)
            {
                if (volumelistCompany.Count > 0)
                    listcreditreport[0].IsCompany = "1";
                else
                    listcreditreport[0].IsCompany = "";
                if (volumelistOwner.Count > 0)
                    listcreditreport[0].IsOwner = "1";
                else
                    listcreditreport[0].IsOwner = "";
                for (int k = 0; k < listcreditreport.Count; k++)
                {
                    //listcreditreport[k].Timeofreport = DateTime.Parse(listcreditreport[k].Timeofreport).ToString("yyyy-MM-dd");
                    if (listcreditreport[k].Type == "C")
                        listcreditreport[k].Type = "Company";
                    else
                        listcreditreport[k].Type = "Owner";
                }
            }
            return PartialView("_CreditReports", listcreditreport);
        }

    }
}