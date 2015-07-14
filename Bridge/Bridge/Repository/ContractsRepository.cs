using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;
using System.Data;
using System.Xml;
using System.Text;
using Bridge.Models.Merchant;
using System.Reflection;
using System.Globalization;
using Bridge.Models.Contracts;


namespace Bridge.Repository
{
    public class ContractsRepository : IContract, IDisposable
    {
        #region Methods

        #region Retrieve/List
        /// <summary>
        /// To list all the bank names
        /// </summary>
        /// <returns></returns>
        public IList<BankNameModel> ListBankNames()
        {
            return new DataAccess.DataAccess().ExecuteReader<BankNameModel>("AVZ_DOC_spListBankNames");
        }

        /// <summary>
        /// To retrieve bank details for particular contract of a Merchant
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
        public IList<BankDetailModel> RetrieveBankDetails(Int64 contractId, Int64 merchantId, int bankId)
        {
            return new DataAccess.DataAccess().ExecuteReader<BankDetailModel>("AVZ_DOC_spCNTBankDetails", new { ContractId = contractId, MerchantId = merchantId, BankId = bankId });
        }

        /// <summary>
        /// To Retrieve Commercial Bank Information
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public IList<CommercialVerification> RetrieveCommercialBankInfo(Int64 contractId, Int64 merchantId)
        {
            return new DataAccess.DataAccess().ExecuteReader<CommercialVerification>("AVZ_CNT_spCommercialNameVerification", new { ContractId = contractId, MerchantId = merchantId });
        }

        public bool UpdateCommercialDetails(Int64 merchantId, CommercialVerification cm)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cnt_spUpdateCommercial", new
            {
                MerchantId = merchantId,
                businessName = cm.businessName,
                Telephone = cm.telephone,
                Address = cm.Address,
                StateId = cm.StateId,
                City = cm.City,
                AddressId = cm.addressId

            });
        }


        public bool UpdateCommericalOwnerDetails(Int64 contractId, Int64 merchantId, List<OwnerModel> cm)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;
            StringBuilder sbXml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sbXml, settings);

            //writer.WriteStartElement("ownerDetails");
            //foreach (var item in cm)
            //{
            //    writer.WriteStartElement("owner");
            //    writer.WriteAttributeString("ownerId", Convert.ToString(item.ownerId));
            //    writer.WriteAttributeString("isAuthorized", Convert.ToString(item.Authorized));
            //    writer.WriteAttributeString("ownerName", Convert.ToString(item.ownerFirstName));
            //    writer.WriteAttributeString("ownerLastName", Convert.ToString(item.ownerLastName));
            //    writer.WriteAttributeString("passportNbr", Convert.ToString(item.PassportNumber));
            //    writer.WriteAttributeString("telephone", Convert.ToString(item.phone1));
            //    writer.WriteAttributeString("AddressId", Convert.ToString(item.addressId));
            //    writer.WriteAttributeString("contactId", Convert.ToString(item.contactId));
            //    writer.WriteEndElement();
            //}
            writer.WriteStartElement("owners");

            foreach (var item in cm)
            {

                writer.WriteStartElement("owner");
                writer.WriteAttributeString("ownerId", Convert.ToString(item.ownerId));
                writer.WriteAttributeString("contactId", Convert.ToString(item.contactId));
                writer.WriteAttributeString("ownerFirstName", Convert.ToString(item.ownerFirstName));
                writer.WriteAttributeString("ownerLastName", Convert.ToString(item.ownerLastName));
                writer.WriteAttributeString("ssn", Convert.ToString(item.ssn));
                writer.WriteAttributeString("ownerDOB", String.Format("{0:yyyy-MM-dd}", item.ownerDOB));
                writer.WriteAttributeString("cityId", Convert.ToString(item.cityId));
                writer.WriteAttributeString("city", Convert.ToString(item.city));
                writer.WriteAttributeString("stateId", Convert.ToString(item.stateId));
                writer.WriteAttributeString("state", Convert.ToString(item.state));
                writer.WriteAttributeString("country", Convert.ToString(item.country));
                writer.WriteAttributeString("zipId", Convert.ToString(item.zipId));
                writer.WriteAttributeString("zip", Convert.ToString(item.zip));
                writer.WriteAttributeString("phone1", Convert.ToString(item.phone1));
                writer.WriteAttributeString("phone2", Convert.ToString(item.CellNumber));
                writer.WriteAttributeString("fax", Convert.ToString(item.fax));
                writer.WriteAttributeString("addressLine1", Convert.ToString(item.addressLine1));
                writer.WriteAttributeString("addressLine2", Convert.ToString(item.addressLine2));
                writer.WriteAttributeString("addressId", Convert.ToString(item.addressId));
                writer.WriteAttributeString("email", Convert.ToString(item.email));
                writer.WriteAttributeString("passportNumber", Convert.ToString(item.PassportNumber));
                writer.WriteAttributeString("authorised", Convert.ToString(item.Authorized));

                writer.WriteEndElement();

            }

            writer.WriteEndElement();
            writer.Flush();
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cnt_spCommericalOwnerDetails", new
            {
                MerchantId = merchantId,
                contractId = contractId,
                ownerDetails = sbXml.ToString()
            });
        }
        /// <summary>
        /// To retrieve funding details
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public FundingModel RetrieveFundingDetails(Int64 contractId, Int64 merchantId)
        {
            FundingModel obj = new DataAccess.DataAccess().ExecuteReader<FundingModel>("AVZ_CNT_spFunding", new { ContractId = contractId, MerchantId = merchantId }).FirstOrDefault();
            //obj.AdministrativeExpensesList = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_cnt_spRetrieveAdministrationExpenses", new { ContractId = contractId });

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ContractModel GetAdminExp(Int64 contractId)
        {
            ContractModel obj = new DataAccess.DataAccess().ExecuteReader<ContractModel>("avz_spreterieveAdminExp", new { ContractId = contractId }).FirstOrDefault();
            return obj;
        }




        /// <summary>
        /// To Retrieve Corporate Document details
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public DataSet RetrieveCorpDetails(Int64 contractId, Int64 merchantId)
        {
            DataSet dsData = new DataSet();
            dsData = new DataAccess.DataAccess().ExecuteDataSet("AVZ_CNT_spCorporateDocVerification", new { ContractId = contractId, MerchantId = merchantId });
            return dsData;
        }

        #endregion

        #region Update
        /// <summary>
        /// To Insert or Update the bank details of Merchant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsUpdateBankDetails(BankDetailModel model, Int16 isCompleted)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_CNT_spInsUpdateBankDetails", new { BankDetailId = model.bankDetailId, BankId = model.bankId, AccountName = model.accountName, AccountNumber = model.accountNumber, BankCode = model.bankCode, ContractId = model.contractId, MerchantId = model.merchantId, isCompleted = isCompleted });
        }

        /// <summary>
        /// To Retrieve BLA Details for a particular Contract - Generate Contract Screen
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public DataSet RetrieveBLADetails(Int64 contractId, Int64 merchantId)
        {
            DataSet dsData = new DataSet();
            dsData = new DataAccess.DataAccess().ExecuteDataSet("AVZ_CNT_spGenerateBLA", new { ContractId = contractId, MerchantId = merchantId });
            return dsData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public DataSet RetrieveIOUDetails(Int64 contractId, Int64 merchantId)
        {
            DataSet dsData = new DataSet();
            dsData = new DataAccess.DataAccess().ExecuteDataSet("avz_cnt_spGenrateIOU", new { ContractId = contractId, MerchantId = merchantId });
            return dsData;
        }

        public DataSet GetMerchantDetailsForQuestion(Int64 merchantId)
        {
            DataSet dsData = new DataSet();
            dsData = new DataAccess.DataAccess().ExecuteDataSet("avz_sp_reterieveMerchantLandlord", new { merchantId = merchantId });
            return dsData;
        }

        /// <summary>
        /// To complete the tasks of Contract
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="taskTypeId"></param>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public bool CompleteContractTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId)
        {
            if (new DataAccess.DataAccess().ExecuteNonQuery("avz_tsk_spCompleteTask", new { merchantId = merchantId, taskTypeId = taskTypeId, workflowId = workflowId, contractId = contractId }))
                return true;
            else
                return false;
        }

        public bool CompleteOfferAcceptanceTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId)
        {
            if (new DataAccess.DataAccess().ExecuteNonQuery("avz_tsk_spCompleteOfferAcceptanceTask", new { omerchantId = merchantId, otaskTypeId = taskTypeId, oworkflowId = workflowId, ocontractId = contractId }))
                return true;
            else
                return false;
        }

        public bool Update(BankDetailModel model)
        {
            return true;
        }
        #endregion

        #region Others
        /// <summary>
        /// TO decline the contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="declineReasonId"></param>
        /// <param name="workflowId"></param>
        /// <param name="declineNotes"></param>
        /// <returns></returns>

        public Int64 DeclineContract(Int64 contractId, Int64 declineReasonId, Int64 workflowId, string declineNotes, bool evaluateAgain, Int64 mId, string screen)
        {

            if (new DataAccess.DataAccess().ExecuteNonQuery("avz_cnt_spDeclineContract", new
            {
                contractId = contractId,
                declineReasonId = declineReasonId,
                workflowId = workflowId,
                declineNotes = declineNotes == null ? "" : declineNotes,
                evaluateAgain = evaluateAgain == true ? 1 : 0,
                merchantId = mId,
                Screen = screen
            }))
            {
                return 1;

            }
            else { return 0; }
        }

        public bool Remove(int id)
        {
            return true;
        }

        public bool Create(BankDetailModel model)
        {
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region VerificationCall

        public VerificationModel RetrieveVerificationDetails(Int64 contractId, Int64 merchantId, string entity)
        {
            var t = new DataAccess.DataAccess().ExecuteReader<QuestionsModel, VerificationModel>("avz_cnt_RetriveVeriCall",
                new
                {
                    ContractId = contractId,
                    Entity = entity
                });
            var vc = t.Item2.Count > 0 ? t.Item2[0] : new VerificationModel();
            vc.questions = t.Item1;

            MerchantDetailQuestionModel mod = new MerchantDetailQuestionModel();
            using (ContractTier dT = new ContractTier())
            {
                mod = dT.GetMerchantDetailsForVerification(merchantId);
            }
            vc.MerchantDetails = mod;

            return vc;
        }

        #endregion
        #endregion

        #region Funding + Final Validation
        public bool ContractFunded(FundingModel model)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("azn_cnt_spCompleteFunding", new
            {
                MerchantId = model.merchantId,
                ContractId = model.contractId,
                BankId = model.bankId,
                AccountNumber = model.accountNumber,
                AccountName = model.accountName,
                McaAmount = model.mcaAmount,
                ExpenseAmount = model.expenseAmount,
                TotalOwnedAmount = model.totalFundingAmount,
                OwnerId = model.ownerId,
                ContractReviewed = model.contractReviewed,
                ContractFunded = model.contractFunded,
                Action = model.action,
                UserId = model.UserId
            });
        }

        public IList<FinalVerificationModel> RetrieveFinalValidation(long MerchantId, long ContractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<FinalVerificationModel>("AVZ_cnt_spRetrieveFinalValidation", new { MerchantId = MerchantId, ContractId = ContractId });
        }

        public bool SaveFinalValidation(long ContractId, string action)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("azn_cnt_spCompleteFinalValidation", new { ContractId = ContractId, Action = action });
        }

        #endregion

        public bool UpdateVeriCall(VerificationModel verModel, long contractId, Int16 isCompleted, string scriptFile)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;
            StringBuilder sbAnswerXml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sbAnswerXml, settings);
            writer.WriteStartElement("answerslist");
            foreach (QuestionsModel ans in verModel.questions)
            {
                writer.WriteStartElement("answers");
                // writer.WriteAttributeString("AnswerId", Convert.ToString(ans.answerId));
                //writer.WriteAttributeString("QuestionId", Convert.ToString(ans.questionId));
                writer.WriteAttributeString("QuestText", ans.questionDesc);
                writer.WriteAttributeString("AnswerText", ans.answerDesc);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();

            var merchantDertails = verModel.MerchantDetails;
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_sp_UpdateMerchantCallVeri", new
            {
                MerchantId = merchantDertails.MerchantId,
                ContractId = contractId,
                OwnerFirstName = merchantDertails.OwnerFirstName,
                OwnerLastName = merchantDertails.OwnerLastName ?? "",
                OwnerPassport = merchantDertails.OwnerPassport,
                LegalName = merchantDertails.LegalName,
                BusinessName = merchantDertails.BusinessName,
                //IsAuthorised = merchantDertails.IsAuthorised,
                BusinessStartDate = merchantDertails.BusinessStartDate,
                GrossAnnualSales = merchantDertails.GrossAnnualSales,
                TypeOfAdvanceId = merchantDertails.TypeOfAdvanceId,
                LoanAmount = merchantDertails.LoanAmount,
                OwnedAmount = merchantDertails.OwnedAmount,
                RetensionPct = merchantDertails.RetensionPct,
                AdminExp = merchantDertails.AdminExp,
                AnswersXml = sbAnswerXml.ToString(),
                ScriptFile = scriptFile ?? string.Empty,
                IsCompleted = isCompleted
            });
        }

        public bool UpdateLandLordCall(VerificationModel verModel, long contractId, Int16 isCompleted, string scriptFile)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;
            StringBuilder sbAnswerXml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sbAnswerXml, settings);
            writer.WriteStartElement("answerslist");
            foreach (QuestionsModel ans in verModel.questions)
            {
                writer.WriteStartElement("answers");
                writer.WriteAttributeString("QuestText", ans.questionDesc);
                writer.WriteAttributeString("AnswerText", ans.answerDesc);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();

            var merchantDertails = verModel.MerchantDetails;
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_sp_UpdateLandLordlVeri", new
            {
                MerchantId = merchantDertails.MerchantId,
                ContractId = contractId,
                OwnerFirstName = merchantDertails.OwnerFirstName,
                OwnerLastName = merchantDertails.OwnerLastName ?? string.Empty,
                MerchantAddress = merchantDertails.MerchantAddress ?? string.Empty,
                LandLordFirstName = merchantDertails.LLFirstName ?? string.Empty,
                LandLordLastName = merchantDertails.LLLastName ?? string.Empty,
                LegalName = merchantDertails.LegalName ?? string.Empty,
                RentedAmount = merchantDertails.RentedAmount,
                ContractStartDate = merchantDertails.ContractStartDate,
                ContractExpireDate = merchantDertails.ContractExpireDate,
                AnswersXml = sbAnswerXml.ToString(),
                ScriptFile = scriptFile ?? string.Empty,
                IsCompleted = isCompleted
            });
        }

        public AnnualSaleModel RetrieveAnnualSalesFile(long merchantId, long contractId)
        {
            var model = new DataAccess.DataAccess().ExecuteReader<AnnualSaleModel>("AVZ_Cnt_GetAnnualSalesCalcFile", new { MerchantId = merchantId, ContractId = contractId });
            if (model.Count > 0)
                return model[0];
            return new AnnualSaleModel { };
        }
    }
}