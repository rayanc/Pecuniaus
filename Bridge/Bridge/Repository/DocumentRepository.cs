using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;


namespace Bridge.Repository
{
    public class DocumentRepository: IDocument, IDisposable
    {
        #region Methods
        /// <summary>
        /// To retrieve all documents types related to a WorkflowId
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IList<DocumentsModel> ListDocumentTypes(int workflowId)
        {
            DocumentsModel model = new DocumentsModel();
            return new DataAccess.DataAccess().ExecuteReader<DocumentsModel>("AVZ_DOC_spListDocTypes", new { WorkflowId = workflowId });
        }
        
        /// <summary>
        /// To retrieve all documents related to a MerchantId
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        public IList<DocumentsModel> ListDocuments(Int64 merchantId,Int64 contractId, int documentTypeId)
        {
            DocumentsModel model = new DocumentsModel();
            return new DataAccess.DataAccess().ExecuteReader<DocumentsModel>("AVZ_DOC_spListDocs", new { MerchantID = merchantId, ContractId = contractId, DocumentTypeId = documentTypeId });           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<DocumentsModel> ListAllDocuments(Int64 merchantId, Int64 contractId)
        {
            DocumentsModel model = new DocumentsModel();
            return new DataAccess.DataAccess().ExecuteReader<DocumentsModel>("AVZ_DOC_spListAllDocs", new { MerchantID = merchantId, ContractId = contractId});
        }

        /// <summary>
        /// Update Documents for a Merchant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDocument(DocumentsModel model)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_DOC_spUpdateDocs", new { DocumentTypeID = model.documentTypeId, MerchantID = model.merchantId, ContractId = model.contractId, FileName = model.fileName, FileDetails = model.fileDetails, UploadUserId = model.uploadUserId, DocumentId = model.documentId,StatusId= model.StatusId });
        }

        /// <summary>
        /// To insert scanned document from Merchants Screen
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertDocuments(DocumentsModel model)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_DOC_spInsertDocuments", new { DocumentTypeID = model.documentTypeId, MerchantID = model.merchantId, ContractID = model.contractId, FileName = model.fileName, FileDetails = model.fileDetails, UploadUserId = model.uploadUserId });
        }

        public bool Remove(int id)
        {
            return true;
        }

        public bool Create(DocumentsModel model)
        {
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public bool Update(DocumentsModel entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}