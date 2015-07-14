using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;

namespace Bridge.BusinessTier
{
    public class DocumentTier:IDisposable
    {
        #region Private Variables

        private IDocument documentsRepository;

        #endregion

        #region Contructors

        public DocumentTier() : this(new DocumentRepository()) { }
        public DocumentTier(IDocument documentsRepository)
        {
            this.documentsRepository = documentsRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// To retrieve all documents types related to a WorkflowId
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IList<DocumentsModel> ListDocumentTypes(int workflowId)
        {
            return documentsRepository.ListDocumentTypes(workflowId);
        }
        
        /// <summary>
        /// To retrieve all documents related to a MerchantId
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        public IList<DocumentsModel> RetrieveDocuments(Int64 merchantId,Int64 contractId,int documentTypeId)
        {
            return documentsRepository.ListDocuments(merchantId,contractId, documentTypeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<DocumentsModel> RetrieveAllDocs(Int64 merchantId, Int64 contractId)
        {
            return documentsRepository.ListAllDocuments(merchantId, contractId);
        }

        /// <summary>
        /// Update Documents for a Merchant
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool UpdateDocument(DocumentsModel Model)
        {
            return documentsRepository.UpdateDocument(Model);
        }      
        
        /// <summary>
        /// To insert scanned document from Merchants Screen
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertDocuments(DocumentsModel model)
        {
            return documentsRepository.InsertDocuments(model);
        }

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}