using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Repository;
using Bridge.Models;


namespace Bridge.BusinessTier
{
    public interface IDocument : IRepository<DocumentsModel, int>
    {
        #region Methods
        /// <summary>
        /// To retrieve all documents types related to a WorkflowId
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        IList<DocumentsModel> ListDocumentTypes(int workflowId);

        /// <summary>
        /// To retrieve all documents related to a MerchantId
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        IList<DocumentsModel> ListDocuments(Int64 merchantId, Int64 contractId, int documentTypeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        IList<DocumentsModel> ListAllDocuments(Int64 merchantId, Int64 contractId);

        /// <summary>
        /// Update Documents for a Merchant
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        bool UpdateDocument(DocumentsModel Model);

        /// <summary>
        /// To insert scanned document from Merchants Screen
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        bool InsertDocuments(DocumentsModel Model);
        #endregion
    }
}