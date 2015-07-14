using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;

namespace Bridge.Repository
{
    public class SalesRepository:ISales,IDisposable
    {
        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public IList<SalesRepresentativeModel> RetrieveSalesRep(string searchString="")
        {
            IList<SalesRepresentativeModel> saleList = new DataAccess.DataAccess().ExecuteReader<SalesRepresentativeModel>("avz_sal_spRetrieveSalesRep", new { searchString = searchString == null ? string.Empty : searchString });

            return saleList;
        }
    }
}