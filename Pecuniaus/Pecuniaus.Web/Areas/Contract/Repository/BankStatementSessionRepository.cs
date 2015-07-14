using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.Models.Contract;

namespace Pecuniaus.Contract.Repository
{
    public class BankStatementSessionRepository
    {
        private readonly string SessionStatementList = "_BankStatementList";

        public List<BankStatementModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionStatementList] != null)
                return (List<BankStatementModel>)HttpContext.Current.Session[SessionStatementList];
            return new List<BankStatementModel>();
        }

        public void Set(List<BankStatementModel> statement)
        {
            if (statement != null)
                foreach (var p in statement)
                {
                    if (p.Id == 0)
                        p.Id = statement.Max(a => a.Id) + 1;

                    p.StatementMonth = Utilities.Common.GetMonthName(p.StatementMonthId);

                }

            HttpContext.Current.Session[SessionStatementList] = statement;
        }

        public void Add(BankStatementModel statement)
        {
            var data = GetAll();

            //var statements = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetMonthNames();

            //var p = statements.FirstOrDefault(a => a.Value == statement.StatementMonthId.ToString());
            //if (p != null)
            //      statement.StatementMonth = p.Text;

            statement.StatementMonth = Utilities.Common.GetMonthName(statement.StatementMonthId);

            if (statement.Id == 0)
            {
                if (data.Count > 0)
                    statement.Id = data.Max(a => a.Id) + 1;
                else
                    statement.Id = 100000;
            }

            data.Add(statement);
            HttpContext.Current.Session[SessionStatementList] = data;
        }

        public void Update(BankStatementModel statement)
        {
            Delete(statement.Id);
            Add(statement);
        }

        public void Delete(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.Id == id).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionStatementList] = data;
            }
        }

        public BankStatementModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.Id == id).FirstOrDefault();
        }

    }

}