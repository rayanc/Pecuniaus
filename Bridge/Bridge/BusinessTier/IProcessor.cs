using System.Collections.Generic;
using Bridge.Models;
namespace Bridge.BusinessTier
{
  public  interface IProcessor
    {
      bool AddUpdateProcessor(ProcessorLookupModel entity);
      IList<ProcessorLookupModel> GetAllProcessors();
      bool CheckProcessorExist(ProcessorLookupModel entity);
    }
}
