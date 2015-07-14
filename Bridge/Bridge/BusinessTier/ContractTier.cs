using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;
using System.Data;
using Bridge.Models.Merchant;
using Bridge.Models.Contracts;

namespace Bridge.BusinessTier
{
    public class ContractTier : IDisposable
    {
        #region Private Variables

        private IContract contractsRepository;

        #endregion

        #region Contructors

        public ContractTier() : this(new ContractsRepository()) { }
        public ContractTier(IContract contractsRepository)
        {
            this.contractsRepository = contractsRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// To retrieve bank name list
        /// </summary>
        /// <returns></returns>
        public IList<BankNameModel> ListBankNames()
        {
            return contractsRepository.ListBankNames();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
        public IList<BankDetailModel> RetrieveBankDetails(Int64 contractId, Int64 merchantId, int bankId)
        {
            return contractsRepository.RetrieveBankDetails(contractId, merchantId, bankId);
        }

        /// <summary>
        /// To get the details of decline
        /// </summary>
        /// <param name="contractid"></param>
        /// <returns></returns>
        public Int64 DeclineContract(Int64 contractid, Int64 declineReasonId, Int64 workFlowId, string declinNotes, bool IsReEvaluvate, Int64 merchantId, string screen)
        {
            return contractsRepository.DeclineContract(contractid, declineReasonId, workFlowId, declinNotes, IsReEvaluvate, merchantId, string.IsNullOrEmpty(screen) ? "" : screen);
        }

        /// <summary>
        /// To Insert or Update the bank details of Merchant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateBankDetails(BankDetailModel model, Int16 isCompleted)
        {
            return contractsRepository.InsUpdateBankDetails(model, isCompleted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public CorporateDocModel RetrieveCorpDetails(Int64 contractId, Int64 merchantId)
        {
            CorporateDocModel mDetail = new CorporateDocModel();
            List<OwnerModel> OwnerList = new List<OwnerModel>();
            DataSet dsData = new DataSet();
            dsData = contractsRepository.RetrieveCorpDetails(contractId, merchantId);
            if (dsData.Tables.Count > 0)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    mDetail.nameOfCompany = Convert.ToString(dsData.Tables[0].Rows[0]["nameOfCompany"]);
                    mDetail.addressDesc = Convert.ToString(dsData.Tables[0].Rows[0]["addressDesc"]);
                    mDetail.RNCNumber = Convert.ToString(dsData.Tables[0].Rows[0]["RNCNumber"]);
                }
                if (dsData.Tables[1].Rows.Count > 0)
                {
                    //foreach (DataRow dRow in dsData.Tables[1].Rows)
                    //{
                    //    OwnerModel mCorp = new OwnerModel();
                    //    mCorp.ownerFirstName = Convert.ToString(dRow["OwnerName"]);
                    //    mCorp.ownerLastName = Convert.ToString(dRow["OwnerLastName"]);
                    //    mCorp.PassportNumber = Convert.ToString(dRow["PassportNbr"]);
                    //    mCorp.phone1 = Convert.ToString(dRow["Telephone"]);
                    //    mCorp.Authorized = Convert.ToBoolean(dRow["IsAuthorized"]);
                    //    mCorp.ownerId = Convert.ToInt64(dRow["OwnerId"]);
                    //    mCorp.contactId = Convert.ToInt64(dRow["ContactId"]);
                    //    mCorp.addressId = Convert.ToInt64(dRow["AddressId"]);
                    //    OwnerList.Add(mCorp);
                    //    mCorp = null;
                    //}
                    //    List<OwnerModel> ownermodellist = new List<OwnerModel>();
                    foreach (DataRow dRow in dsData.Tables[1].Rows)
                    {
                        OwnerModel ownermodel = new OwnerModel();
                        if (dRow["contactId"].ToString() != null && dRow["contactId"].ToString() != "")
                            ownermodel.contactId = Convert.ToInt64(dRow["contactId"]);
                        if (dRow["ownerId"].ToString() != null && dRow["ownerId"].ToString() != "")
                            ownermodel.ownerId = Convert.ToInt64(dRow["ownerId"]);
                        if (dRow["addressId"].ToString() != null && dRow["addressId"].ToString() != "")
                            ownermodel.addressId = Convert.ToInt64(dRow["addressId"]);
                        ownermodel.merchantId = merchantId;
                        ownermodel.ownerFirstName = Convert.ToString(dRow["ownerFirstName"]);
                        ownermodel.ownerLastName = Convert.ToString(dRow["ownerLastName"]);
                        if (dRow["ownerDOB"].ToString() != null && dRow["ownerDOB"].ToString() != "")
                            ownermodel.ownerDOB = Convert.ToDateTime(dRow["ownerDOB"]);
                        ownermodel.PassportNumber = Convert.ToString(dRow["passportnbr"]);
                        ownermodel.phone1 = Convert.ToString(dRow["phone1"]);
                        ownermodel.CellNumber = Convert.ToString(dRow["phone2"]);
                        ownermodel.ssn = Convert.ToString(dRow["ssn"]);
                        ownermodel.addressLine1 = Convert.ToString(dRow["addressLine1"]);
                        ownermodel.addressLine2 = Convert.ToString(dRow["addressLine2"]);
                        ownermodel.country = Convert.ToString(dRow["country"]);
                        ownermodel.city = Convert.ToString(dRow["city"]);
                        ownermodel.state = Convert.ToString(dRow["state"]);
                        ownermodel.stateId = Convert.ToString(dRow["stateId"]);
                        ownermodel.zip = Convert.ToString(dRow["zip"]);
                        ownermodel.email = Convert.ToString(dRow["email"]);
                        ownermodel.Authorized = Convert.ToBoolean(dRow["IsAuthorized"]);
                        OwnerList.Add(ownermodel);
                    }
                }

                mDetail.OwnerList = OwnerList;
                OwnerList = null;
                if (dsData.Tables[2].Rows.Count > 0)
                {
                    mDetail.fileName = Convert.ToString(dsData.Tables[2].Rows[0]["fileName"]);
                    mDetail.fileDetails = Convert.ToString(dsData.Tables[2].Rows[0]["fileDetails"]);
                }
            }
            return mDetail;
        }

        /// <summary>
        /// To Retrieve Commercial Bank Information
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public IList<CommercialVerification> RetrieveCommercialBankInfo(Int64 contractId, Int64 merchantId)
        {
            return contractsRepository.RetrieveCommercialBankInfo(contractId, merchantId);
        }

        /// <summary>
        /// Update commercial information
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public bool UpdateCommercialDetails(Int64 merchantId, CommercialVerification cm)
        {
            return contractsRepository.UpdateCommercialDetails(merchantId, cm);
        }

        public bool UpdateCommericalOwnerDetails(Int64 contractId, Int64 merchantId, List<OwnerModel> cm)
        {
            return contractsRepository.UpdateCommericalOwnerDetails(contractId, merchantId, cm);
        }


        public ContractModel GetAdminExp(Int64 contractId)
        {
            return contractsRepository.GetAdminExp(contractId);
        }

        /// <summary>
        /// To retrieve funding details
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public FundingModel RetrieveFundingDetails(Int64 contractId, Int64 merchantId)
        {
            return contractsRepository.RetrieveFundingDetails(contractId, merchantId);
        }

        public MerchantDetailQuestionModel GetMerchantDetailsForVerification(Int64 merchantId)
        {
            MerchantDetailQuestionModel details = new MerchantDetailQuestionModel();
            //List<UseOfAdvanceModel> AdvancesList = new List<UseOfAdvanceModel>();            
            DataSet dsData = new DataSet();
            dsData = contractsRepository.GetMerchantDetailsForQuestion(merchantId);
            if (dsData.Tables.Count > 0)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    details.MerchantId = Convert.ToInt16(dsData.Tables[0].Rows[0]["MerchantId"]);
                    details.LegalName = Convert.ToString(dsData.Tables[0].Rows[0]["LegalName"]);
                    details.BusinessName = Convert.ToString(dsData.Tables[0].Rows[0]["BusinessName"]);
                    details.SalesRepName = Convert.ToString(dsData.Tables[0].Rows[0]["SalesRepName"]);

                    details.OwnerFirstName = Convert.ToString(dsData.Tables[0].Rows[0]["OwnerFirstName"]);
                    details.OwnerLastName = Convert.ToString(dsData.Tables[0].Rows[0]["OwnerLastName"]);

                    //details.OwnerName = Convert.ToString(dsData.Tables[0].Rows[0]["OwnerName"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["retention"].ToString()) == false)
                        details.RetensionPct = Convert.ToInt16(dsData.Tables[0].Rows[0]["retention"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["IsAuthorized"].ToString()) == false)
                        details.IsAuthorised = Convert.ToInt16(dsData.Tables[0].Rows[0]["IsAuthorized"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["LoanedAmount"].ToString()) == false)
                        details.LoanAmount = Convert.ToDouble(dsData.Tables[0].Rows[0]["LoanedAmount"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["ownedAmount"].ToString()) == false)
                        details.OwnedAmount = Convert.ToDouble(dsData.Tables[0].Rows[0]["ownedAmount"]);
                    details.OwnerPassport = Convert.ToString(dsData.Tables[0].Rows[0]["PassportNbr"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["GrossAnnualSales"].ToString()) == false)
                        details.GrossAnnualSales = Convert.ToDouble(dsData.Tables[0].Rows[0]["GrossAnnualSales"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["AdminExp"].ToString()) == false)
                        details.AdminExp = Convert.ToDouble(dsData.Tables[0].Rows[0]["AdminExp"]);
                    if (dsData.Tables[0].Rows[0]["BusinessStartDate"] != null)
                    {
                        if (dsData.Tables[0].Rows[0]["BusinessStartDate"].ToString() != "")
                            details.BusinessStartDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["BusinessStartDate"]);
                    }
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["TypeOfAdvanceId"].ToString()) == false)
                        details.TypeOfAdvanceId = Convert.ToInt64(dsData.Tables[0].Rows[0]["TypeOfAdvanceId"]);

                    if (dsData.Tables[0].Rows[0]["RentFlag"] != null)
                    {
                        long v = 0;
                        long.TryParse(dsData.Tables[0].Rows[0]["RentFlag"].ToString(), out v);
                        details.RentFlag = v;
                    }
                    if (dsData.Tables[0].Rows[0]["ContractStartDate"] != null)
                    {
                        if (dsData.Tables[0].Rows[0]["ContractStartDate"].ToString() != "")
                            details.ContractStartDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["ContractStartDate"]);
                    }
                    if (dsData.Tables[0].Rows[0]["ContractExpireDate"] != null)
                    {
                        if (dsData.Tables[0].Rows[0]["ContractExpireDate"].ToString() != "")
                            // details.ContractExpireDate = (dsData.Tables[0].Rows[0]["ContractExpireDate"]).ToString();
                            details.ContractExpireDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["ContractExpireDate"]);
                    }
                }


                if (dsData.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsData.Tables[1].Rows)
                    {
                        details.MerchantAddress = Convert.ToString(dRow["MerchantAddress"]);
                        details.LandlordAddress = Convert.ToString(dRow["LandlordAddress"]);
                        details.RentedAmount = dRow.Field<double?>("RentedAmount");
                        //details.LandlordName = Convert.ToString(dRow["LandlordName"]);
                        details.LLFirstName = Convert.ToString(dRow["LLFirstName"]);
                        details.LLLastName = Convert.ToString(dRow["LLLastName"]);
                        details.LLCompany = Convert.ToString(dRow["LLCompany"]);
                    }
                }


                //if (dsData.Tables[1].Rows.Count > 0)
                //{
                //    foreach (DataRow dRow in dsData.Tables[1].Rows)
                //    {
                //        UseOfAdvanceModel eType = new UseOfAdvanceModel();
                //        eType.AdvanceId = Convert.ToInt64(dRow["TypeId"]);
                //        eType.Description = Convert.ToString(dRow["Description"]);
                //        AdvancesList.Add(eType);
                //        eType = null;
                //    }
                //}
                //details.advances = AdvancesList;
            }

            return details;
        }

        public ContractIOU RetrieveIOUDetails(Int64 contractId, Int64 merchantId)
        {
            ContractIOU IOU = new ContractIOU();
            List<IOUOwnerList> ownersList = new List<IOUOwnerList>();
            DataSet dsData = new DataSet();
            dsData = contractsRepository.RetrieveIOUDetails(contractId, merchantId);
            if (dsData.Tables.Count > 0)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    IOU.LetterDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["LetterDate"]);
                    if (dsData.Tables[0].Rows[0]["FundingDate"] != null)
                    {
                        if (dsData.Tables[0].Rows[0]["FundingDate"].ToString() != "")
                            IOU.FundingDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["FundingDate"]);
                    }
                    IOU.RNC = Convert.ToString(dsData.Tables[0].Rows[0]["RNCNumber"]);
                    IOU.LegalCompanyName = Convert.ToString(dsData.Tables[0].Rows[0]["LegalCompanyName"]);
                    IOU.CompanyAddress = Convert.ToString(dsData.Tables[0].Rows[0]["CompanyAddress"]);
                    IOU.province = Convert.ToString(dsData.Tables[0].Rows[0]["Province"]);
                    if (string.IsNullOrEmpty(dsData.Tables[0].Rows[0]["TotalOwnedAmount"].ToString()) == false)
                        IOU.TotalOwnedAmount = Convert.ToDouble(dsData.Tables[0].Rows[0]["TotalOwnedAmount"]);
                    IOU.stateId = Convert.ToInt64(dsData.Tables[0].Rows[0]["stateid"]);
                    IOU.businesTypeId = Convert.ToInt64(dsData.Tables[0].Rows[0]["BusinessTypeId"]);

                }

                if (dsData.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsData.Tables[1].Rows)
                    {
                        IOUOwnerList eType = new IOUOwnerList();
                        eType.name = Convert.ToString(dRow["ownerName"]);
                        eType.ID = Convert.ToString(dRow["ID"]);
                        eType.Address = Convert.ToString(dRow["Address"]);
                        eType.Province = Convert.ToString(dRow["Province"]);
                        eType.IsAuthorised = Convert.ToBoolean(dRow["IsAuthorised"]);
                        ownersList.Add(eType);
                        eType = null;
                    }
                }
                IOU.ownerList = ownersList;
            }
            return IOU;
        }

        /// <summary>
        /// To Retrieve BLA Details for a particular Contract - Generate Contract Screen
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ContractBLA RetrieveBLADetails(Int64 contractId, Int64 merchantId)
        {
            ContractBLA mBLA = new ContractBLA();
            List<EntityType> entityList = new List<EntityType>();
            List<OwnerList> ownerList = new List<OwnerList>();
            DataSet dsData = new DataSet();
            dsData = contractsRepository.RetrieveBLADetails(contractId, merchantId);
            if (dsData.Tables.Count > 0)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    mBLA.contractNumber = Convert.ToString(dsData.Tables[0].Rows[0]["contractNumber"]);
                    mBLA.salesRepName = Convert.ToString(dsData.Tables[0].Rows[0]["salesRepName"]);
                    mBLA.salesRepId = Convert.ToString(dsData.Tables[0].Rows[0]["salesRepId"]);
                    mBLA.date = Convert.ToDateTime(dsData.Tables[0].Rows[0]["date"]);
                    mBLA.companyName = Convert.ToString(dsData.Tables[0].Rows[0]["companyName"]);
                    mBLA.businessName = Convert.ToString(dsData.Tables[0].Rows[0]["businessName"]);
                    mBLA.orgCountry = Convert.ToString(dsData.Tables[0].Rows[0]["orgCountry"]);
                    mBLA.address = Convert.ToString(dsData.Tables[0].Rows[0]["address"]);
                    mBLA.province = Convert.ToString(dsData.Tables[0].Rows[0]["province"]);
                    mBLA.country = Convert.ToString(dsData.Tables[0].Rows[0]["country"]);
                    mBLA.legalAddress = Convert.ToString(dsData.Tables[0].Rows[0]["legalAddress"]);
                    mBLA.legalProvince = Convert.ToString(dsData.Tables[0].Rows[0]["legalProvince"]);
                    mBLA.legalCountry = Convert.ToString(dsData.Tables[0].Rows[0]["legalCountry"]);
                    mBLA.businessStartDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["businessStartDate"]);
                    mBLA.RNCNumber = Convert.ToString(dsData.Tables[0].Rows[0]["RNCNumber"]);
                    //mBLA.ownerName = Convert.ToString(dsData.Tables[0].Rows[0]["ownerName"]);
                    // mBLA.jobTitle = Convert.ToString(dsData.Tables[0].Rows[0]["jobTitle"]);
                    //mBLA.telephone = Convert.ToString(dsData.Tables[0].Rows[0]["telephone"]);
                    //mBLA.email = Convert.ToString(dsData.Tables[0].Rows[0]["email"]);
                    mBLA.bankName = Convert.ToString(dsData.Tables[0].Rows[0]["bankName"]);
                    mBLA.bankCity = Convert.ToString(dsData.Tables[0].Rows[0]["bankCity"]);
                    mBLA.bankCountry = Convert.ToString(dsData.Tables[0].Rows[0]["bankCountry"]);
                    mBLA.mcaAmount = Convert.ToString(dsData.Tables[0].Rows[0]["mcaAmount"]);
                    mBLA.totalOwnedAount = Convert.ToString(dsData.Tables[0].Rows[0]["totalOwnedAount"]);
                    mBLA.retention = Convert.ToString(dsData.Tables[0].Rows[0]["retention"]);
                    mBLA.expenseAmount = Convert.ToString(dsData.Tables[0].Rows[0]["expenseAmount"]);
                    mBLA.businessTypeId = Convert.ToInt64(dsData.Tables[0].Rows[0]["businessTypeId"]);

                }

                if (dsData.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsData.Tables[1].Rows)
                    {
                        OwnerList eType = new OwnerList();
                        eType.name = Convert.ToString(dRow["ownerName"]);
                        eType.jobtitle = Convert.ToString(dRow["jobTitle"]);
                        eType.telephone = Convert.ToString(dRow["telephone"]);
                        eType.email = Convert.ToString(dRow["email"]);
                        eType.PassportNbr = Convert.ToString(dRow["PassportNbr"]);
                        DataHelper.FillObjectFromDataRow(eType.Address, dRow);

                        ownerList.Add(eType);
                        eType = null;
                    }
                }

                //if (ownerList.Count > 0)
                //{
                //    mBLA.ownerName = ownerList[0].name;
                //    mBLA.jobTitle = ownerList[0].jobtitle;
                //    mBLA.telephone = ownerList[0].telephone;
                //    mBLA.email = ownerList[0].email;
                //}
                if (dsData.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsData.Tables[2].Rows)
                    {
                        EntityType eType = new EntityType();
                        eType.entityTypeId = Convert.ToString(dRow["entityTypeId"]);
                        eType.entityName = Convert.ToString(dRow["entityName"]);
                        entityList.Add(eType);
                        eType = null;
                    }
                }
                mBLA.EntityList = entityList;
                mBLA.ownerList = ownerList;
                entityList = null;
            }
            return mBLA;
        }

        /// <summary>
        /// To complete the tasks of Contract
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="taskTypeId"></param>
        /// <param name="workflowId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public bool CompleteContractTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId)
        {
            return contractsRepository.CompleteContractTask(merchantId, taskTypeId, workflowId, contractId);
        }

        public bool CompleteOfferAcceptanceTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId)
        {
            return contractsRepository.CompleteOfferAcceptanceTask(merchantId, taskTypeId, workflowId, contractId);
        }

        public VerificationModel RetrieveVerificationCall(Int64 contractId, Int64 merchantId)
        {
            return contractsRepository.RetrieveVerificationDetails(contractId, merchantId, "Contract");
        }

        public VerificationModel RetrieveLandLordVerification(Int64 contractId, Int64 merchantId)
        {
            return contractsRepository.RetrieveVerificationDetails(contractId, merchantId, "Landlord");
        }

        public bool ContractFunded(FundingModel model)
        {
            return contractsRepository.ContractFunded(model);
        }

        public IList<FinalVerificationModel> RetrieveFinalValidation(long MerchantId, long ContractId)
        {
            return contractsRepository.RetrieveFinalValidation(MerchantId, ContractId);
        }
        public bool SaveFinalValidation(long ContractId, string action)
        {
            return contractsRepository.SaveFinalValidation(ContractId, action);
        }

        public bool UpdateVeriCall(VerificationModel verModel, long contractId, Int16 isCompleted, string scriptFile)
        {
            return contractsRepository.UpdateVeriCall(verModel, contractId, isCompleted, scriptFile);
        }

        public bool UpdateLandLordVeri(VerificationModel verModel, long contractId, Int16 isCompleted, string scriptFile)
        {
            return contractsRepository.UpdateLandLordCall(verModel, contractId, isCompleted, scriptFile);
        }

        public AnnualSaleModel RetrieveAnnualSalesFile(long merchantId, long contractId)
        {
            return contractsRepository.RetrieveAnnualSalesFile(merchantId, contractId);
        }

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}