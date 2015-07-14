using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Repository
{
    public interface IRepository<T, TId> 
    {
      bool Create(T entity);
      bool Update(T entity);
      bool Remove(TId id);
      
    }

   
}