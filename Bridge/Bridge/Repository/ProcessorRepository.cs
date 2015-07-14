using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;
namespace Bridge.Repository
{
    public class ProcessorRepository:IProcessor,IDisposable
    {
        public bool AddUpdateProcessor(ProcessorLookupModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_Proc_spAddUpdateProcessor", new
            {
                ProcessorId = entity.ProcessorId,
                Name = entity.Name,
                ProcessorCode = entity.ProcessorCode,
                Description = entity.Description,
                IsActive = entity.IsActive              
            });
        }
        // bool CheckProcessorExist(ProcessorLookupModel entity);
        public bool CheckProcessorExist(ProcessorLookupModel entity)  
        {
            int result = new DataAccess.DataAccess().ExecuteScalar<int>("avz_proc_CheckprocessorNameExist", new { Name = entity.Name, ProcessorId = entity.ProcessorId });
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IList<ProcessorLookupModel> GetAllProcessors()
        {
            return new DataAccess.DataAccess().ExecuteReader<ProcessorLookupModel>("avz_Proc_spGetAllProcessors");
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}