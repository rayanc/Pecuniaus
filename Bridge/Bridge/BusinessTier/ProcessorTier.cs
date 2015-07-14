using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;

namespace Bridge.BusinessTier
{
    public class ProcessorTier:IDisposable
    {
            #region Private Variables
        private IProcessor processorRepository; 
        #endregion

        #region Contructors
        public ProcessorTier() : this(new ProcessorRepository()) { }
        public ProcessorTier(IProcessor processorsRepository)
        {
            this.processorRepository = processorsRepository;
        }
        #endregion

        #region Methods
        public bool CheckProcessorExist(ProcessorLookupModel entity)
        {
            return processorRepository.CheckProcessorExist(entity);
        }
        public bool AddUpdateProcessor(ProcessorLookupModel entity)
        {
            return processorRepository.AddUpdateProcessor(entity);
        }
        public IList<ProcessorLookupModel> GetAllProcessors() 
        {
            return processorRepository.GetAllProcessors();
        }
         #endregion

        public void Dispose()
        {
            processorRepository = null;
        }
    }
}