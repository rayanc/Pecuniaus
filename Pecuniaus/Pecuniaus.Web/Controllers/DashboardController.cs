using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using Pecuniaus.UICore.Controllers;
using System.Web.Mvc;
using System.Linq;

namespace Pecuniaus.Web.Controllers
{
    public class DashboardController : UiBaseController
    {
        DashboardApi dashboardApi;
        public DashboardController()
        {
            dashboardApi = new DashboardApi();
        }

        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            var model = dashboardApi.GetDashboardDetails(CurrentUserID);
            SaveInSession(model);
            Pecuniaus.UICore.SessionHelper.SetCurrentGroup();
            return View(model);
        }

        public ActionResult TaskActivityList()
        {
            var dashboard = GetFromSession();
            return PartialView("_TaskActivityList", dashboard.UserAssignedTasks);
        }

        public ActionResult CollectionActivityList()
        {
            var dashboard = GetFromSession();
            return PartialView("_CollectionActivityList", dashboard.CollectionActivities);
        }

        public ActionResult NewLeadList()
        {
            var dashboard = GetFromSession();
            return PartialView("_NewLeadList", dashboard.NewLeads);
        }

        //public ActionResult SalePerformanceList()
        //{
        //    var dashboard = GetFromSession();

        //    //var model = dashboard.TotalSales
        //    // .GroupBy(l => l.Description)
        //    // .Select(cl => new
        //    //      TotalSale
        //    //      {
        //    //          Description = cl.First().Description,
        //    //          PaidAmount = cl.Sum(c => c.PaidAmount),
        //    //          BalanceAmount = cl.Sum(c => c.BalanceAmount),
        //    //          LoanedAmount = cl.Sum(c => c.LoanedAmount)
        //    //      }).ToList();

        //    return PartialView("_SalePerformanceList", dashboard.TotalSales);
        //}

        private void SaveInSession(UserDashboardModel m)
        {

            //sliceColors: ['#3366cc','#dc3912','#ff9900','#109618','#66aa00','#dd4477','#0099c6','#990099','#dc3911']
            //Group TotalSales
            var mTotalSales = m.TotalSales
          .GroupBy(l => l.Description)
          .Select(cl => new
               TotalSale
          {
              Description = cl.First().Description,
              PaidAmount = cl.Sum(c => c.PaidAmount),
              BalanceAmount = cl.Sum(c => c.BalanceAmount),
              LoanedAmount = cl.Sum(c => c.LoanedAmount)
          }).ToList();

            m.TotalSales = mTotalSales;

            Session["_DashBoard"] = m;
        }

        private UserDashboardModel GetFromSession()
        {
            if (Session["_DashBoard"] != null)
                return (UserDashboardModel)Session["_DashBoard"];
            return new UserDashboardModel();
        }


    }
}