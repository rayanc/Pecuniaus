using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Repository;
using Bridge.Models;

namespace Bridge.BusinessTier
{
    public class SalesTier:IDisposable
    {
         #region Private Variables
        private ISales salesRepository;
        #endregion


        #region Contructors
        public SalesTier() : this(new SalesRepository()) { }
        public SalesTier(ISales salesRepository)
        {
            this.salesRepository = salesRepository;
        }
        #endregion
        public IList<SalesRepresentativeModel> RetrieveSalesRep(string searchString)
        {
            return salesRepository.RetrieveSalesRep(searchString);
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}