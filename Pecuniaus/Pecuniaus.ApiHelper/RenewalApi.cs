using System.Linq;
using Pecuniaus.Models.Renewal;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Pecuniaus.ApiHelper
{
    public class RenewalApi
    {
        public IList<DocumentTypeModel> GetDocument(int WorkflowID)
        {
            return BaseApiData.GetAPIResult<IList<DocumentTypeModel>>("documents/DocumentTypes?workflowId=" + WorkflowID, () => new List<DocumentTypeModel>());
        }
        public FundingModel GetFundingDetail(long contractId, long merchantId)
        {
            var query = string.Format("contracts/GetFundingDetails?contractId={0}&merchantId={1}", contractId, merchantId);
            List<FundingModel> funding = BaseApiData.GetAPIResult<List<FundingModel>>(query, () => new List<FundingModel>());
            if (funding.Count > 0)
                return funding[0];
            return new FundingModel { };
        }
        public void CompContractTask(long merchantId, long taskType, int workflowId, long contractId)
        {
            string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
                merchantId, taskType, workflowId, contractId);
            BaseApiData.GetAPIResult<object>(apiData, () => new object());
        }
        public void KickBack(long merchantId, long taskTypeId, long contractId, int workflowId)        {
            
            var apiMethod = string.Format("gen/kickback?merchantId={0}&tasktypeId={1}&workflowId={2}&contractid={3}", merchantId, taskTypeId, workflowId, contractId);
            BaseApiData.PostAPIData(apiMethod, null);
        }
            

        }
       

    
}
