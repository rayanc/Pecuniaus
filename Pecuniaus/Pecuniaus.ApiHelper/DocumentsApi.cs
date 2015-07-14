using Pecuniaus.Models.Contract;
using System.Collections.Generic;

namespace Pecuniaus.ApiHelper
{
    public class DocumentsApi
    {
        public IList< DocumentModel> RetriveDocument(long merchantId, long contractId, long documentTypeId)
        {
            string apiQuery = string.Format("documents/RetriveDocument?merchantId={0}&contractId={1}&documentTypeId={2}",
                merchantId, contractId, documentTypeId);

            var docs = BaseApiData.GetAPIResult<IList<DocumentModel>>(apiQuery, () => new List<DocumentModel>());

            string noImage = "NoImage.jpg";
            foreach (var d in docs)
            { 
                if (string.IsNullOrEmpty(d.FileName))
                {
                    d.FileName = noImage;
                    d.FileDetails = "image/jpg";
                }
            }
            return docs;
            //if (docs.Count > 0)
            //{
            //    var doc = docs[0];
            //    if (string.IsNullOrEmpty(doc.FileName))
            //    {
            //        doc.FileName = noImage;
            //    }
            //    return doc;
            //}
            //return new DocumentModel { FileName = noImage };

        }

        public void UpdateDocuments(DocumentModel docModel)
        {
            BaseApiData.PutAPIData("documents/UpdateDocuments", docModel);
        }

    }
}
