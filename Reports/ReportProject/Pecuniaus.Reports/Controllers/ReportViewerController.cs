using System.Web.Mvc;

namespace Pecuniaus.Controllers
{
    public class ReportViewerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowReport(string id )
        {
            return PartialView("_ViewReport", id);
        }

        public ActionResult DocumentViewerPartial(string id)
        {
            switch (id)
            {
                case "0":
                   // ViewData["Report"] = new Pecuniaus.Reports.Monthly_MCA();
                    break;
                case "1":
                    ViewData["Report"] = new Pecuniaus.Reports.Collection();
                    break;
                case "2":
                    ViewData["Report"] = new Pecuniaus.Reports.Contracts_Contract();
                    break;
                case "3":
                    ViewData["Report"] = new Pecuniaus.Reports.Contracts_DeclinedByAvanzame();
                    break;
                case "4":
                    ViewData["Report"] = new Pecuniaus.Reports.Contracts_DeclinedByClient();
                    break;
                case "5":
                    ViewData["Report"] = new Pecuniaus.Reports.Contracts_Pending();
                    break;
                case "6":
                    ViewData["Report"] = new Pecuniaus.Reports.Investigation();
                    break;
                case "7":
                    ViewData["Report"] = new Pecuniaus.Reports.Monthly_MCA();
                    break;
                case "8":
                    ViewData["Report"] = new Pecuniaus.Reports.Notes();
                    break;
                case "9":
                    ViewData["Report"] = new Pecuniaus.Reports.PAFReport();
                    break;
                case "10":
                    ViewData["Report"] = new Pecuniaus.Reports.Prequalification();
                    break;
                case "11":
                    ViewData["Report"] = new Pecuniaus.Reports.Prequalification_DeclinedByAvanzame();
                    break;
                case "12":
                    ViewData["Report"] = new Pecuniaus.Reports.Prequalification_DeclinedByClient();
                    break;
                case "13":
                    ViewData["Report"] = new Pecuniaus.Reports.Prequalification_Offers();
                    break;
                case "14":
                    ViewData["Report"] = new Pecuniaus.Reports.Prequalifications_Pending();
                    break;
                case "15":
                    ViewData["Report"] = new Pecuniaus.Reports.Prequalifications_AcceptedOffers();
                    break;
                case "16":
                    ViewData["Report"] = new Pecuniaus.Reports.Refund_Done();
                    break;
                case "17":
                    ViewData["Report"] = new Pecuniaus.Reports.Refund_Pending();
                    break;
                case "18":
                    ViewData["Report"] = new Pecuniaus.Reports.Renewals();
                    break;
                case "19":
                    ViewData["Report"] = new Pecuniaus.Reports.Renewals_Pending();
                    break;
                case "20":
                    ViewData["Report"] = new Pecuniaus.Reports.Renewals_DeclinedByAvanzeMe();
                    break;
                case "21":
                    ViewData["Report"] = new Pecuniaus.Reports.Renewals_DeclinedByClient();
                    break;
                case "22":
                    ViewData["Report"] = new Pecuniaus.Reports.ScoringReport();
                    break;
                case "23":
                    ViewData["Report"] = new Pecuniaus.Reports.WorkFlowReport();
                    break;
                case "24":
                    ViewData["Report"] = new Pecuniaus.Reports.WrittenOffContracts();
                    break;
                case "25":
                    ViewData["Report"] = new Pecuniaus.Reports.WSF_Report();
                    break;
                case "26":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_BadDebtRecoveryAccount();
                    break;
                case "27":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_Collection();
                    break;
                case "28":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_Contract_Activity();
                    break;
                case "29":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_DAR_New_Renewalcs();
                    break;
                case "30":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_Processing_Assets();
                    break;
                case "31":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_Progress_New_Renewal();
                    break;
                case "32":
                    ViewData["Report"] = new Pecuniaus.Reports.Author_RefundsDone();
                    break;
                case "33":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_RefundsEarrings();
                    break;
                case "34":
                    ViewData["Report"] = new Pecuniaus.Reports.Auditor_Research();
                    break;
                case "35":
                    ViewData["Report"] = new Pecuniaus.Reports.Monthly_Portfolio_del_Avance();
                    break;
                case "36":
                    ViewData["Report"] = new Pecuniaus.Reports.Monthly_Ingreso();
                    break;
                case "37":
                    ViewData["Report"] = new Pecuniaus.Reports.Monthly_Saldos_de_avance();
                    break;
                case "38":
                    ViewData["Report"] = new Pecuniaus.Reports.Monthly_DAR_Pendiente();
                    break;
                case "39":
                    ViewData["Report"] = new Pecuniaus.Reports.Monthly_Procesamiento_Saldo_del_avance();
                    break;
                case "40":
                    ViewData["Report"] = new Pecuniaus.Reports.Monthly_Procesamiento_DAR_PENDIENTE();
                    break;
                case "41":
                    ViewData["Report"] = new Pecuniaus.Reports.Daily_DER_DAR_PENDIENTE();
                    break;
                case "42":
                    ViewData["Report"] = new Pecuniaus.Reports.Daily_RPT_DER_SALDOS_DE_AVANCE();
                    break;
                case "43":
                    ViewData["Report"] = new Pecuniaus.Reports.Daily_Portafolio_del_avance();
                    break;
                case "44":
                    ViewData["Report"] = new Pecuniaus.Reports.Daily_Ingresso();
                    break;
                case "45":
                    ViewData["Report"] = new Pecuniaus.Reports.Daily_Procesamiento_Saldo_del_avance();
                    break;
                case "46":
                    ViewData["Report"] = new Pecuniaus.Reports.Daily_Procesamiento_DAR_PENDIENTE();
                    break;
            }
            return PartialView("_ViewReport");
        }
    }
}
