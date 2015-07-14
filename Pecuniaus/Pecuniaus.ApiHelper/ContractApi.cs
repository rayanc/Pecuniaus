using System.Linq;
using Pecuniaus.Models.Contract;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using Pecuniaus.Utilities;

namespace Pecuniaus.ApiHelper
{
    public class ContractApi
    {
        public IEnumerable<BankNameModel> GetBankNames()
        {
            return BaseApiData.GetAPIResult<IEnumerable<BankNameModel>>("contracts/GetBankNames",
                () => new List<BankNameModel>());
        }


        public FundingModel GetFundingDetail(long contractId, long merchantId)
        {
            var query = string.Format("contracts/GetFundingDetails?contractId={0}&merchantId={1}", contractId, merchantId);
            List<FundingModel> funding = BaseApiData.GetAPIResult<List<FundingModel>>(query, () => new List<FundingModel>());
            if (funding.Count > 0)
                return funding[0];
            return new FundingModel { };
        }
        public IList<DocumentTypeModel> GetDocument(int WorkflowID)
        {
            return BaseApiData.GetAPIResult<IList<DocumentTypeModel>>("documents/DocumentTypes?workflowId=" + WorkflowID, () => new List<DocumentTypeModel>());
        }

        public BankInfoVerificationModel GetBankDetails(int BankId, long contractId, long merchantId)
        {
            var bankInfo = string.Format("contracts/GetBankDetails?contractId={0}&merchantId={1}&bankId={2}", contractId, merchantId, BankId);
            List<BankInfoVerificationModel> model = BaseApiData.GetAPIResult<List<BankInfoVerificationModel>>(bankInfo, () => new List<BankInfoVerificationModel>());
            if (model.Count > 0)
                return model[0];
            return new BankInfoVerificationModel { };

        }
        public CorporateDocVerificationModel GetCorporateDocumemts(long contractId, long merchantId)
        {
            var query = string.Format("contracts/GetCorporateDocumemts?contractId={0}&merchantId={1}", contractId, merchantId);
            return BaseApiData.GetAPIResult<CorporateDocVerificationModel>(query, () => new CorporateDocVerificationModel());
        }

        public CommercialNameVeriModel GetCommercialNameVerification(long contractId, long merchantId)
        {
            var query = string.Format("contracts/GetCommercialDocVerification?contractId={0}&merchantId={1}", contractId, merchantId);
            List<CommercialNameVeriModel> commercial = BaseApiData.GetAPIResult<List<CommercialNameVeriModel>>(query, () => new List<CommercialNameVeriModel>());
            if (commercial.Count > 0)
                return commercial[0];
            return new CommercialNameVeriModel { };
        }

        public VerificationCallModel GetVerificationCall(long contractId, long merchantId)
        {
            var query = string.Format("contracts/GetVerificationCall?contractId={0}&merchantid={1}", contractId, merchantId);
            var vcModel = BaseApiData.GetAPIResult<VerificationCallModel>(query, () => new VerificationCallModel());

            vcModel.VcAnswers = ConvertQuestionsToModel<VCRightWrong>(vcModel.Questions);
            return vcModel;
        }


        public void UpdateVerificationCall(VerificationCallModel model, long contractId, bool isComplete)
        {
            //var questions = new List<QuestionModel>();

            //// get all public static properties of VCRightWrong type
            //PropertyInfo[] propertyInfos;
            //propertyInfos = typeof(VCRightWrong).GetProperties();

            //foreach (PropertyInfo propertyInfo in propertyInfos)
            //{
            //    questions.Add(
            //       new QuestionModel
            //       {
            //           QuestionDesc = propertyInfo.Name,
            //           AnswerDesc = propertyInfo.GetValue(model.VcAnswers, null).ToString()
            //       }
            //       );
            //}
            //model.Questions = questions;

            model.Questions = GetQuestionsFromModel<VCRightWrong>(model.VcAnswers);

            var complete = isComplete ? "1" : "0";

            var updateQuery = string.Format("contracts/UpdateVerificationCall?contractId={0}&isCompleted={1}", contractId, complete);
            BaseApiData.PutAPIData(updateQuery, model);

        }

        public LandLordVeriModel GetLandLordVerification(long contractId, long merchantId)
        {
            var query = string.Format("contracts/GetLandLordVerification?contractId={0}&merchantid={1}", contractId, merchantId);

            var model = BaseApiData.GetAPIResult<LandLordVeriModel>(query, () => new LandLordVeriModel());
            model.Answers = ConvertQuestionsToModel<LLRightWrong>(model.Questions);
            return model;
        }

        public void UpdateLandlordVerification(LandLordVeriModel model, long contractId, bool isComplete)
        {
            model.Questions = GetQuestionsFromModel<LLRightWrong>(model.Answers);

            var complete = isComplete ? "1" : "0";

            var updateQuery = string.Format("contracts/UpdateLandlordVerification?contractId={0}&isCompleted={1}", contractId, complete);
            BaseApiData.PutAPIData(updateQuery, model);

        }


        public void CompContractTask(long merchantId, long taskType, long contractId)
        {
            string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
                merchantId, taskType, 2, contractId);
            BaseApiData.GetAPIResult<object>(apiData, () => new object());
        }

        public ContractBLA GetBLADetails(long merchantId, long contractId)
        {
            string apiData = string.Format("contracts/GetBLADetails?contractId={0}&merchantId={1}", contractId, merchantId);
            return BaseApiData.GetAPIResult<ContractBLA>(apiData, () => new ContractBLA());
        }

        public void UpdateCommercialDetails(long merchantId, CommercialNameVeriModel model)
        {
            var updateQuery = string.Format("contracts/UpdateCommercialDetails?merchantId={0}", merchantId);
            BaseApiData.PutAPIData(updateQuery, model);
        }

        public void UpdateCommercialOwnerDetails(long merchantId, long contractId, List<OwnerModel> model)
        {
            var updateQuery = string.Format("contracts/UpdateCommercialOwnerDetails?merchantId={0}&contractId={1}", merchantId, contractId);
            BaseApiData.PutAPIData(updateQuery, model);
        }

        public ContractIOU GetIOUDetails(long merchantId, long contractId)
        {
            string apiData = string.Format("contracts/GetIOUDetails?contractId={0}&merchantId={1}", contractId, merchantId);
            return BaseApiData.GetAPIResult<ContractIOU>(apiData, () => new ContractIOU());
        }

        public ContractModel GetAdminExp(long contractId)
        {
            string apiData = string.Format("contracts/GetAdminExp/{0}", contractId);
            return BaseApiData.GetAPIResult<ContractModel>(apiData, () => new ContractModel());
        }

        #region PrivateMethods
        private T ConvertQuestionsToModel<T>(IEnumerable<QuestionModel> questions) where T : new()
        {
            var vcAnswers = new T();

            //     var vcAnswers = new T();
            PropertyInfo property = null;
            Type entityType = vcAnswers.GetType();

            if (questions != null)
                foreach (var q in questions)
                {
                    string propName = q.QuestionDesc;
                    try
                    {
                        property = entityType.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    }
                    catch (AmbiguousMatchException)
                    {
                        //If it is an Ambiguous Match, that is we have both properties on this object then get the specific match
                        property = entityType.GetProperty(propName);
                    }

                    if (property != null && q.AnswerDesc != null && property.CanWrite)
                    {
                        property.SetValue(vcAnswers, Convertor.ChangeType(q.AnswerDesc, property.PropertyType), null);
                        //var sValue = false;
                        //if (q.AnswerDesc.Equals("True") || q.AnswerDesc.Equals("Y"))
                        //{
                        //sValue =true;
                        //}
                        //property.SetValue(vcAnswers, sValue, null);
                    }
                }
            return vcAnswers;
        }

        public IEnumerable<QuestionModel> GetQuestionsFromModel<T>(T model)
        {
            var questions = new List<QuestionModel>();

            // get all public static properties of VCRightWrong type
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                questions.Add(
                   new QuestionModel
                   {
                       QuestionDesc = propertyInfo.Name,
                       AnswerDesc = propertyInfo.GetValue(model, null).ToString()
                   }
                   );
            }
            return questions;
        }

        public AnnualSaleModel RetrieveAnnualSalesFile(long merchantId, long contractId)
        {
            string apiData = string.Format("contracts/RetrieveAnnualSalesFile?merchantId={0}&contractId={1}", merchantId, contractId);
            return BaseApiData.GetAPIResult<AnnualSaleModel>(apiData, () => new AnnualSaleModel());
        }
        #endregion
    }
}
