using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bridge.Models;

namespace Bridge.BusinessTier
{
   public  interface ISales
    {
        IList<SalesRepresentativeModel> RetrieveSalesRep(string searchString);
    }
}
