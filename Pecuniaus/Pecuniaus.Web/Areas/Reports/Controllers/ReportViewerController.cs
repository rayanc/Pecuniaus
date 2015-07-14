using System.Web.Mvc;
using DevExpress.XtraReports.UI;
using Pecuniaus.Reports.Models;
using System.Web;
using System.Linq;

namespace Pecuniaus.Reports.Controllers
{
    public class ReportViewerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowReport(string id, string CreateDate, string LastActivityDate, string StartDate, string EndDate, string Agent, string SalesRep, string Module,
            string ContractStatus, string Type, string Percent, string Merchant, string TaskStatus)
        {
            string QueryS = "id=" + id + "|CreateDate=" + CreateDate + "|LastActivityDate=" + LastActivityDate + "|StartDate=" + StartDate + "|EndDate=" + EndDate
                + "|Agent=" + Agent + "|SalesRep=" + SalesRep + "|Module=" + Module + "|ContractStatus=" + ContractStatus + "|Type=" + Type + "|Percent=" + Percent
                 + "|Merchant=" + Merchant + "|TaskStatus=" + TaskStatus;
            return PartialView("_ViewReport", QueryS);
        }

        public ActionResult DocumentViewerPartial(string QueryS)
        {
            string[] pars = QueryS.Split('|');
            string id = pars.Where(s => s.Split('=')[0].Equals("id")).FirstOrDefault().Split('=')[1];
            string CreateDate = pars.Where(s => s.Split('=')[0].Equals("CreateDate")).FirstOrDefault().Split('=')[1];
            string LastActivityDate = pars.Where(s => s.Split('=')[0].Equals("LastActivityDate")).FirstOrDefault().Split('=')[1];
            string StartDate = pars.Where(s => s.Split('=')[0].Equals("StartDate")).FirstOrDefault().Split('=')[1];
            string EndDate = pars.Where(s => s.Split('=')[0].Equals("EndDate")).FirstOrDefault().Split('=')[1];
            string Agent = pars.Where(s => s.Split('=')[0].Equals("Agent")).FirstOrDefault().Split('=')[1];
            string SalesRep = pars.Where(s => s.Split('=')[0].Equals("SalesRep")).FirstOrDefault().Split('=')[1];
            string Module = pars.Where(s => s.Split('=')[0].Equals("Module")).FirstOrDefault().Split('=')[1];
            string ContractStatus = pars.Where(s => s.Split('=')[0].Equals("ContractStatus")).FirstOrDefault().Split('=')[1];
            string Type = pars.Where(s => s.Split('=')[0].Equals("Type")).FirstOrDefault().Split('=')[1];
            string Percent = pars.Where(s => s.Split('=')[0].Equals("Percent")).FirstOrDefault().Split('=')[1];
            string Merchant = pars.Where(s => s.Split('=')[0].Equals("Merchant")).FirstOrDefault().Split('=')[1];
            string TaskStatus = pars.Where(s => s.Split('=')[0].Equals("TaskStatus")).FirstOrDefault().Split('=')[1];

            DevExpress.XtraReports.Parameters.Parameter prm1;
            DevExpress.XtraReports.Parameters.Parameter prm2;
            DevExpress.XtraReports.Parameters.Parameter prm3;

            switch (id)
            {
                case "0":
                    break;
                case "1":
                    ViewData["Report"] = new Pecuniaus.Reports.Collection();
                    break;
                case "2":
                    Pecuniaus.Reports.Contracts_Contract cnt = new Pecuniaus.Reports.Contracts_Contract();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = cnt.Parameters["Agent"];
                    prm2 = cnt.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    cnt.Parameters.Add(prm1);
                    cnt.Parameters.Add(prm2);
                    cnt.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = cnt;
                    break;
                case "3":
                    Pecuniaus.Reports.Contracts_DeclinedByAvanzame avz = new Pecuniaus.Reports.Contracts_DeclinedByAvanzame();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = avz.Parameters["Agent"];
                    prm2 = avz.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    avz.Parameters.Add(prm1);
                    avz.Parameters.Add(prm2);
                    avz.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = avz;
                    break;
                case "4":
                    Pecuniaus.Reports.Contracts_DeclinedByClient dc = new Pecuniaus.Reports.Contracts_DeclinedByClient();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = dc.Parameters["Agent"];
                    prm2 = dc.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    dc.Parameters.Add(prm1);
                    dc.Parameters.Add(prm2);
                    dc.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = dc;
                    break;
                case "5":
                    Pecuniaus.Reports.Contracts_Pending cp = new Pecuniaus.Reports.Contracts_Pending();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = cp.Parameters["Agent"];
                    prm2 = cp.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    cp.Parameters.Add(prm1);
                    cp.Parameters.Add(prm2);
                    cp.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = cp;
                    break;
                case "6":
                    Pecuniaus.Reports.Investigation rep = new Pecuniaus.Reports.Investigation();
                    prm1 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = rep.Parameters["Merchant"];
                    prm1.Value = Merchant;
                    rep.Parameters.Add(prm1);
                    rep.FilterString = "[Merchant] = [Parameters.Merchant]";
                    ViewData["Report"] = rep;
                    break;
                case "7":
                    Pecuniaus.Reports.Monthly_MCA mm = new Pecuniaus.Reports.Monthly_MCA();
                    prm1 = prm2 = prm3 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = mm.Parameters["ContractStatus"];
                    prm2 = mm.Parameters["Date"];
                    prm3 = mm.Parameters["Type"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    prm3.Value = Module;
                    mm.Parameters.Add(prm1);
                    mm.Parameters.Add(prm2);
                    mm.Parameters.Add(prm3);
                    mm.FilterString = "[ContractStatus] = [Parameters.ContractStatus] AND [Date] = [Parameters.Date] AND [Type]=[Parameters.Type]";
                    ViewData["Report"] = mm;
                    break;
                case "8":
                    Pecuniaus.Reports.Notes nt = new Pecuniaus.Reports.Notes();
                    prm1 = prm2 = prm3 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = nt.Parameters["Agent"];
                    prm2 = nt.Parameters["Date"];
                    prm3 = nt.Parameters["Module"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    prm3.Value = Module;
                    nt.Parameters.Add(prm1);
                    nt.Parameters.Add(prm2);
                    nt.Parameters.Add(prm3);
                    nt.FilterString = "[Agent] = [Parameters.Agent] AND [Date] = [Parameters.Date] AND [Module]=[Parameters.Module]";
                    ViewData["Report"] = nt;
                    break;
                case "9":
                    Pecuniaus.Reports.PAFReport av = new Pecuniaus.Reports.PAFReport();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = av.Parameters["StartDate"];
                    prm2 = av.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    av.Parameters.Add(prm1);
                    av.Parameters.Add(prm2);
                    av.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = av;
                    break;
                case "10":
                    Pecuniaus.Reports.Prequalification pq = new Pecuniaus.Reports.Prequalification();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = pq.Parameters["Agent"];
                    prm2 = pq.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    pq.Parameters.Add(prm1);
                    pq.Parameters.Add(prm2);
                    pq.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = pq;
                    break;
                case "11":
                    Pecuniaus.Reports.Prequalification_DeclinedByAvanzame pqavz = new Pecuniaus.Reports.Prequalification_DeclinedByAvanzame();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = pqavz.Parameters["Agent"];
                    prm2 = pqavz.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    pqavz.Parameters.Add(prm1);
                    pqavz.Parameters.Add(prm2);
                    pqavz.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = pqavz;
                    break;
                case "12":
                    Pecuniaus.Reports.Prequalification_DeclinedByClient pqdcb = new Pecuniaus.Reports.Prequalification_DeclinedByClient();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = pqdcb.Parameters["Agent"];
                    prm2 = pqdcb.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    pqdcb.Parameters.Add(prm1);
                    pqdcb.Parameters.Add(prm2);
                    pqdcb.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = pqdcb;
                    break;
                case "13":
                    Pecuniaus.Reports.Prequalification_Offers ofr = new Pecuniaus.Reports.Prequalification_Offers();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = ofr.Parameters["Agent"];
                    prm2 = ofr.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    ofr.Parameters.Add(prm1);
                    ofr.Parameters.Add(prm2);
                    ofr.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = ofr;
                    break;
                case "14":
                    Pecuniaus.Reports.Prequalifications_Pending pqpen = new Pecuniaus.Reports.Prequalifications_Pending();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = pqpen.Parameters["Agent"];
                    prm2 = pqpen.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    pqpen.Parameters.Add(prm1);
                    pqpen.Parameters.Add(prm2);
                    pqpen.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = pqpen;
                    break;
                case "15":
                    Pecuniaus.Reports.Prequalifications_AcceptedOffers acp = new Pecuniaus.Reports.Prequalifications_AcceptedOffers();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = acp.Parameters["Agent"];
                    prm2 = acp.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    acp.Parameters.Add(prm1);
                    acp.Parameters.Add(prm2);
                    acp.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = acp;
                    break;
                case "16":
                    Pecuniaus.Reports.Refund_Done refd = new Pecuniaus.Reports.Refund_Done();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = refd.Parameters["DatePaidInFull"];
                    prm2 = refd.Parameters["LastActvityDate"];
                    prm1.Value = System.Convert.ToDateTime(CreateDate);
                    prm2.Value = System.Convert.ToDateTime(LastActivityDate);
                    refd.Parameters.Add(prm1);
                    refd.Parameters.Add(prm2);
                    refd.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = refd;
                    break;
                case "17":
                    ViewData["Report"] = new Pecuniaus.Reports.Refund_Pending();
                    break;
                case "18":
                    Pecuniaus.Reports.Renewals ren = new Pecuniaus.Reports.Renewals();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = ren.Parameters["Agent"];
                    prm2 = ren.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    ren.Parameters.Add(prm1);
                    ren.Parameters.Add(prm2);
                    ren.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = ren;
                    break;
                case "19":
                    Pecuniaus.Reports.Renewals_Pending pen = new Pecuniaus.Reports.Renewals_Pending();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = pen.Parameters["Agent"];
                    prm2 = pen.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    pen.Parameters.Add(prm1);
                    pen.Parameters.Add(prm2);
                    pen.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = pen;
                    break;
                case "20":
                    Pecuniaus.Reports.Renewals_DeclinedByAvanzeMe dca = new Pecuniaus.Reports.Renewals_DeclinedByAvanzeMe();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = dca.Parameters["Agent"];
                    prm2 = dca.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    dca.Parameters.Add(prm1);
                    dca.Parameters.Add(prm2);
                    dca.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = dca;
                    break;
                case "21":
                    Pecuniaus.Reports.Renewals_DeclinedByClient dcb = new Pecuniaus.Reports.Renewals_DeclinedByClient();
                    prm1 = prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = dcb.Parameters["Agent"];
                    prm2 = dcb.Parameters["SalesRep"];
                    prm1.Value = Agent;
                    prm2.Value = SalesRep;
                    dcb.Parameters.Add(prm1);
                    dcb.Parameters.Add(prm2);
                    dcb.FilterString = "[Agent] = [Parameters.Agent] AND [SalesRep] = [Parameters.SalesRep]";
                    ViewData["Report"] = dcb;
                    break;
                case "22":
                    Pecuniaus.Reports.ScoringReport sc = new Pecuniaus.Reports.ScoringReport();
                    prm1 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = sc.Parameters["CreationDate"];
                    prm1.Value = System.Convert.ToDateTime(CreateDate);//System.Convert.ToDateTime("2015-03-16 20:02:28");
                    sc.Parameters.Add(prm1);
                    sc.FilterString = "[CreationDate] = [Parameters.CreationDate]";
                    ViewData["Report"] = sc;
                    break;
                case "23":
                    Pecuniaus.Reports.WorkFlowReport wf = new Pecuniaus.Reports.WorkFlowReport();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = wf.Parameters["StartDate"];
                    prm2 = wf.Parameters["TaskStatus"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = TaskStatus;
                    wf.Parameters.Add(prm1);
                    wf.Parameters.Add(prm2);
                    wf.FilterString = "[StartDate] = [Parameters.StartDate] AND [TaskStatus] = [Parameters.TaskStatus]";
                    ViewData["Report"] = wf;
                    break;
                case "24":
                    Pecuniaus.Reports.WrittenOffContracts wof = new Pecuniaus.Reports.WrittenOffContracts();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = wof.Parameters["StartDate"];
                    prm2 = wof.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    wof.Parameters.Add(prm1);
                    wof.Parameters.Add(prm2);
                    wof.FilterString = "[StartFundingDate] = [Parameters.StartDate] AND [EndFundingDate] = [Parameters.EndDate]";
                    ViewData["Report"] = wof;
                    break;
                case "25":
                    Pecuniaus.Reports.WSF_Report wsf = new Pecuniaus.Reports.WSF_Report();
                    prm1 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = wsf.Parameters["Date"];
                    prm1.Value = System.Convert.ToDateTime(CreateDate);
                    wsf.Parameters.Add(prm1);
                    wsf.FilterString = "[Date] = [Parameters.Date]";
                    ViewData["Report"] = wsf;
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
                    Pecuniaus.Reports.Monthly_Portfolio_del_Avance port = new Pecuniaus.Reports.Monthly_Portfolio_del_Avance();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = port.Parameters["StartDate"];
                    prm2 = port.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    port.Parameters.Add(prm1);
                    port.Parameters.Add(prm2);
                    port.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = port;
                    break;
                case "36":
                    Pecuniaus.Reports.Monthly_Ingreso ming = new Pecuniaus.Reports.Monthly_Ingreso();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = ming.Parameters["StartDate"];
                    prm2 = ming.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    ming.Parameters.Add(prm1);
                    ming.Parameters.Add(prm2);
                    ming.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = ming;
                    break;
                case "37":
                    Pecuniaus.Reports.Monthly_Saldos_de_avance ing = new Pecuniaus.Reports.Monthly_Saldos_de_avance();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = ing.Parameters["StartDate"];
                    prm2 = ing.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    ing.Parameters.Add(prm1);
                    ing.Parameters.Add(prm2);
                    ing.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = ing;
                    break;
                case "38":
                    Pecuniaus.Reports.Monthly_DAR_Pendiente mon = new Pecuniaus.Reports.Monthly_DAR_Pendiente();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = mon.Parameters["StartDate"];
                    prm2 = mon.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    mon.Parameters.Add(prm1);
                    mon.Parameters.Add(prm2);
                    mon.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = mon;
                    break;
                case "39":
                    Pecuniaus.Reports.Monthly_Procesamiento_Saldo_del_avance di = new Pecuniaus.Reports.Monthly_Procesamiento_Saldo_del_avance();
                    prm1=prm2 =prm3= new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = di.Parameters["StartDate"];
                    prm2 = di.Parameters["EndDate"];
                    prm3 = di.Parameters["Percent"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    prm3.Value = Percent;
                    di.Parameters.Add(prm1);
                    di.Parameters.Add(prm2);
                    di.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate] AND [Percent]=[Parameters.Percent]";
                    ViewData["Report"] = di;
                    break;
                case "40":
                    Pecuniaus.Reports.Monthly_Procesamiento_DAR_PENDIENTE pro = new Pecuniaus.Reports.Monthly_Procesamiento_DAR_PENDIENTE();
                    prm1=prm2 =prm3= new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = pro.Parameters["StartDate"];
                    prm2 = pro.Parameters["EndDate"];
                    prm3 = pro.Parameters["Percent"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    prm3.Value = Percent;
                    pro.Parameters.Add(prm1);
                    pro.Parameters.Add(prm2);
                    pro.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate] AND [Percent]=[Parameters.Percent]";
                    ViewData["Report"] = pro;
                    break;
                case "41":
                    Pecuniaus.Reports.Daily_DER_DAR_PENDIENTE ddd = new Pecuniaus.Reports.Daily_DER_DAR_PENDIENTE();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = ddd.Parameters["StartDate"];
                    prm2 = ddd.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    ddd.Parameters.Add(prm1);
                    ddd.Parameters.Add(prm2);
                    ddd.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = ddd;
                    break;
                case "42":
                    Pecuniaus.Reports.Daily_RPT_DER_SALDOS_DE_AVANCE sald = new Pecuniaus.Reports.Daily_RPT_DER_SALDOS_DE_AVANCE();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = sald.Parameters["StartDate"];
                    prm2 = sald.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    sald.Parameters.Add(prm1);
                    sald.Parameters.Add(prm2);
                    sald.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = sald;
                    break;
                case "43":
                    Pecuniaus.Reports.Daily_Portafolio_del_avance por = new Pecuniaus.Reports.Daily_Portafolio_del_avance();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = por.Parameters["StartDate"];
                    prm2 = por.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    por.Parameters.Add(prm1);
                    por.Parameters.Add(prm2);
                    por.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = por;
                    break;
                case "44":
                    Pecuniaus.Reports.Daily_Ingresso dai = new Pecuniaus.Reports.Daily_Ingresso();
                    prm1=prm2 = new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = dai.Parameters["StartDate"];
                    prm2 = dai.Parameters["EndDate"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    dai.Parameters.Add(prm1);
                    dai.Parameters.Add(prm2);
                    dai.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate]";
                    ViewData["Report"] = dai;
                    break;
                case "45":
                    Pecuniaus.Reports.Daily_Procesamiento_Saldo_del_avance sal = new Pecuniaus.Reports.Daily_Procesamiento_Saldo_del_avance();
                    prm1=prm2 =prm3= new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = sal.Parameters["StartDate"];
                    prm2 = sal.Parameters["EndDate"];
                    prm3 = sal.Parameters["Percent"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    prm3.Value = Percent;
                    sal.Parameters.Add(prm1);
                    sal.Parameters.Add(prm2);
                    sal.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate] AND [Percent]=[Parameters.Percent]";
                    ViewData["Report"] = sal;
                    break;
                case "46":
                    Pecuniaus.Reports.Daily_Procesamiento_DAR_PENDIENTE ces = new Pecuniaus.Reports.Daily_Procesamiento_DAR_PENDIENTE();
                    prm1=prm2 =prm3= new DevExpress.XtraReports.Parameters.Parameter();
                    prm1 = ces.Parameters["StartDate"];
                    prm2 = ces.Parameters["EndDate"];
                    prm3 = ces.Parameters["Percent"];
                    prm1.Value = System.Convert.ToDateTime(StartDate);
                    prm2.Value = System.Convert.ToDateTime(EndDate);
                    prm3.Value = Percent;
                    ces.Parameters.Add(prm1);
                    ces.Parameters.Add(prm2);
                    ces.FilterString = "[StartDate] = [Parameters.StartDate] AND [EndDate] = [Parameters.EndDate] AND [Percent]=[Parameters.Percent]";
                    ViewData["Report"] = ces;
                    break;
            }
            return PartialView("_ViewReport");
        }
    }
}
