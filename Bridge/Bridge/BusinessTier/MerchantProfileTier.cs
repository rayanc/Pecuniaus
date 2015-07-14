using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Repository;
using Bridge.Models;

namespace Bridge.BusinessTier
{
    public class MerchantProfileTier : IDisposable
    {
        #region Private Variables
        private IMerchantProfile merchantProfileRepository;
        #endregion

        #region Contructors
        public MerchantProfileTier() : this(new MerchantProfileRepository()) { }
        public MerchantProfileTier(IMerchantProfile merchantProfileRepository)
        {
            this.merchantProfileRepository = merchantProfileRepository;
        }
        #endregion

        #region Business
        public IList<GeneralModel> RetrieveStatus(string statusType)
        {
            return merchantProfileRepository.RetrieveStatus(statusType);
        }
        public MPMerchantBusinessModel RetrieveMerchantBusinessInformation(int merchantId)
        {
            Dictionary<string, string> result = merchantProfileRepository.RetrieveMerchantBusinessInformation(merchantId);
            MPMerchantBusinessModel resultedModel = new MPMerchantBusinessModel();
            if (result.Count > 0)
            {
                resultedModel.MerchantID = Convert.ToInt64(result["MerchantId"]);
                resultedModel.MerchantStatus = result["MerchantStatus"];
                resultedModel.ContractStatus = result["ContractStatus"];
                resultedModel.ContractID = Convert.ToInt64(result["ContractId"]);
                resultedModel.LoanedAmount = Convert.ToDouble(result["LoanedAmount"]);
                resultedModel.OwnedAmount = Convert.ToDouble(result["OwnedAmount"]);
                resultedModel.PaidAmount = Convert.ToDouble(result["PaidAmount"]);

                resultedModel.Business.affilliationNumber = result["AffiliationNumber"];
                resultedModel.Business.businessCCStartDate = Convert.ToDateTime(result["businessCCStartDate"]);
                resultedModel.Business.businessStartDate = Convert.ToDateTime(result["businessStartDate"]);
                resultedModel.Business.businessStatus = result["BusinessStatus"];
                resultedModel.Business.entityTypeID = Convert.ToInt32(result["EntityTypeID"]);
                resultedModel.Business.industryTypeID = Convert.ToInt32(result["IndustryTypeID"]);
                resultedModel.Business.nameOfBusiness = result["BusinessName"];
                resultedModel.Business.nameOfCompany = result["LegalName"];
                resultedModel.Business.RNC = result["RNCNumber"];

                resultedModel.PhysicalAddress.Address = result["PAddress"];
                resultedModel.PhysicalAddress.Email = result["Pemail"];
                resultedModel.PhysicalAddress.Fax = result["PFax"];
                resultedModel.PhysicalAddress.Phone1 = result["PPhone1"];
                resultedModel.PhysicalAddress.Phone2 = result["PPhone2"];
                resultedModel.PhysicalAddress.ProvinceID = Convert.ToInt32(result["PprovinceId"]);

                resultedModel.LegalAddress.Address = result["LAddress"];
                resultedModel.LegalAddress.City = result["LCity"];
                resultedModel.LegalAddress.Email = result["Lemail"];
                resultedModel.LegalAddress.Fax = result["LFax"];
                resultedModel.LegalAddress.Phone1 = result["LPhone1"];
                resultedModel.LegalAddress.Phone2 = result["LPhone2"];
                resultedModel.LegalAddress.ProvinceID = Convert.ToInt32(result["LprovinceId"]);

                resultedModel.Processor.AddRange(RetrieveMerchantBusinessProcessorInformation(merchantId));

                //resultedModel.Processor.Add(new MPMerchantProcessorInfoModel() {
                //    FirstProcessedDate = Convert.ToDateTime(result["businessStartDate"])
                //});
            }
            return resultedModel;
        }
        public IList<MPMerchantProcessorInfoModel> RetrieveMerchantBusinessProcessorInformation(int merchantId)
        {
            return merchantProfileRepository.RetrieveMerchantBusinessProcessorInformation(merchantId);
        }
        public bool UpdateMerchantBusinessInformation(MPMerchantBusinessModel entity)
        {
                return merchantProfileRepository.UpdateMerchantBusinessInformation(entity);
        }
        public bool UpdateMerchantBusinessProcessorInformation(Int64 MerchantId,int Terminal, int processorTypeId, string processorNumber, DateTime dateGracePeriod, int industryTypeID, DateTime firstProcessedDate, int processorId)
        {
                return merchantProfileRepository.UpdateMerchantBusinessProcessorInformation(MerchantId, Terminal, processorTypeId, processorNumber, dateGracePeriod, industryTypeID, firstProcessedDate, processorId);
                
        }
        public bool UpdateMerchantBusinessProcessorInformation(MPMerchantProcessorInfoModel entity)
        {
                return merchantProfileRepository.UpdateMerchantBusinessProcessorInformation(entity);
        }
        #endregion

        #region Landlord
        public MPMerchantLandlordModel RetrieveMerchantLandlordInformation(int merchantId)
        {
            Dictionary<string, string> result = merchantProfileRepository.RetrieveMerchantLandlordInformation(merchantId);
            MPMerchantLandlordModel resultedModel = new MPMerchantLandlordModel();
            if (result.Count > 0)
            {
                resultedModel.MerchantID = Convert.ToInt32(result["MerchantId"]);
                resultedModel.MerchantStatus = result["MerchantStatus"];
                resultedModel.ContractStatus = result["ContractStatus"];
                resultedModel.TypeofPropertyId = result["TypeofPropertyId"];
                resultedModel.LandlordCompanyName = result["LandlordCompanyName"];
                resultedModel.landlordFirstName = result["LandlordFirstName"];
                resultedModel.landlordLastName = result["LandlordLastName"];

                resultedModel.MonthlyRentAmount = result["RentedAmount"];
                resultedModel.LandlordAddress.Address = result["Address"];
                resultedModel.LandlordAddress.City = result["City"];
                resultedModel.LandlordAddress.Phone1 = result["Phone"];
                resultedModel.LandlordAddress.Cell = result["CellPhone"];
                resultedModel.LandlordAddress.Email = result["Email"];

                resultedModel.Questions = RetrieveMerchantLandlordQuestionInformation(merchantId);
            }
            return resultedModel;
        }
        public MPMerchantLandlordQuestionsModel RetrieveMerchantLandlordQuestionInformation(int merchantId)
        {
            return merchantProfileRepository.RetrieveMerchantLandlordQuestionInformation(merchantId);
        }
        public bool UpdateMerchantLandlordInformation(MPMerchantLandlordModel entity)
        {
                return merchantProfileRepository.UpdateMerchantLandlordInformation(entity);
        }
        #endregion

        #region Contacts
        public IList<MPMerchantContactsModel> RetrieveMerchantContacts(int merchantId)
        {
            return merchantProfileRepository.RetrieveMerchantContacts(merchantId);
        }
        public MPMerchantContactDetailModel RetrieveMerchantContactsInformation(int contactId)
        {
            return merchantProfileRepository.RetrieveMerchantContactsInformation(contactId);
        }
        public bool UpdateMerchantContactInformation(MPMerchantContactDetailModel entity)
        {
                return merchantProfileRepository.UpdateMerchantContactInformation(entity);
        }
        public bool AddMerchantContactInformation(MPMerchantContactDetailModel entity)
        {
                return merchantProfileRepository.AddMerchantContactInformation(entity);
        }

        public bool DeleteMerchantContacts(int contactId)
        {
            return merchantProfileRepository.DeleteMerchantContacts(contactId);
        }
        #endregion

        #region Owners
        public IList<MPMerchantOwnersModel> RetrieveMerchantOwners(int merchantId)
        {
            return merchantProfileRepository.RetrieveMerchantOwners(merchantId);
        }
        public MPMerchantOwnerDetailModel RetrieveMerchantOwnersInformation(int OwnerId)
        {
            return merchantProfileRepository.RetrieveMerchantOwnersInformation(OwnerId);
        }
        public bool UpdateMerchantOwnerInformation(MPMerchantOwnerDetailModel entity)
        {
                return merchantProfileRepository.UpdateMerchantOwnerInformation(entity);
        }
        public bool AddMerchantOwnerInformation(MPMerchantOwnerDetailModel entity)
        {
                return merchantProfileRepository.AddMerchantOwnerInformation(entity);
        }

        public bool DeleteMerchantOwners(int OwnerID)
        {
            return merchantProfileRepository.DeleteMerchantOwners(OwnerID);
        }
        #endregion

        #region Profiles
        public MPMerchantProfileDetailModel RetrieveMerchantProfiles(int merchantId, int ProcessorId, string ProcessorNumber)
        {
            MPMerchantProfileDetailModel objProfiles = merchantProfileRepository.RetrieveMerchantProfiles(merchantId, ProcessorId, ProcessorNumber).FirstOrDefault();

            IList<MPMerchantProfilesModel> objDetails = merchantProfileRepository.RetrieveMerchantProfileDetails(merchantId, ProcessorId, ProcessorNumber);
            objProfiles.ProfileDetail = objDetails.ToList();

            return objProfiles;
        }
        public MPMerchantProfileDetailModel RetrieveMerchantProfile(int ActivityId)
        {
            IList<MPMerchantProfilesModel> objDetails = merchantProfileRepository.RetrieveMerchantProfileDetails(ActivityId);
            MPMerchantProfileDetailModel objProfiles = new MPMerchantProfileDetailModel();
            objProfiles.ProfileDetail = objDetails.ToList();

            return objProfiles;
        }
        public bool UpdateMerchantProfileInformation(MPMerchantProfileDetailModel entity)
        {
                return merchantProfileRepository.UpdateMerchantProfileInformation(entity);
        }
        #endregion

        #region Documents
        public IList<MPMerchantDocumentModel> RetrieveMerchantDocuments(int merchantId, int DocumentTypeId)
        {
            IList<MPMerchantDocumentModel> obj = merchantProfileRepository.RetrieveMerchantDocuments(merchantId, DocumentTypeId);

            return obj;
        }

        public bool DeleteMerchantDocuments(int documentId)
        {
            return merchantProfileRepository.DeleteMerchantDocuments(documentId);
        }
        #endregion

        #region Activities
        public MPMerchantActivityDetailModel RetrieveMerchanActivitiesMonthly(int merchantId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate)
        {
            List<MPMerchantActivityModel> Activities = merchantProfileRepository.RetrieveMerchanActivitiesMonthly(merchantId, ProcessorTypeId, ProcessorNumber, ActivityStartDate, ActivityEndDate).ToList();
            var GroupedActivities = Activities.
                GroupBy(m => m.MonthName).
                SelectMany(m => m).ToList();

            MPMerchantActivityDetailModel objActivities = new MPMerchantActivityDetailModel();
            objActivities.ActivityDetail = GroupedActivities;

            return objActivities;
        }
        public MPMerchantActivityDetailModel RetrieveMerchantActivities(int merchantId, int ProcessorTypeId, string MonthName)
        {
            List<MPMerchantActivityModel> Activities = merchantProfileRepository.RetrieveMerchantActivities(merchantId, ProcessorTypeId, MonthName).ToList();
            var GroupedActivities = Activities.
                GroupBy(m => m.MonthName).
                SelectMany(m => m).ToList();

            MPMerchantActivityDetailModel objActivities = new MPMerchantActivityDetailModel();
            objActivities.ActivityDetail = GroupedActivities;

            return objActivities;
        }

        #endregion

        #region Statements
        public MPMerchantStatementsDetailModel RetrieveMerchantStatements(int merchantId, DateTime StatementFrom, DateTime StatementTo)
        {
            IList<MPMerchantStatementModel> objDetails = merchantProfileRepository.RetrieveMerchantStatements(merchantId, StatementFrom, StatementTo);
            IList<MPMerchantStatementsDetailModel> objSummary = merchantProfileRepository.RetrieveMerchantStatementsSummary(merchantId, StatementFrom, StatementTo);
            MPMerchantStatementsDetailModel objStatements = new MPMerchantStatementsDetailModel();
            objStatements.StatementsDetail = objDetails.ToList();
            objStatements.TradeID = objSummary[0].TradeID;
            objStatements.FirmName = objSummary[0].FirmName;
            objStatements.RNC = objSummary[0].RNC;
            objStatements.TradeName = objSummary[0].TradeName;
            objStatements.AddressDetail = objSummary[0].AddressDetail;
            objStatements.TotalCashRecieved = objSummary[0].TotalCashRecieved;
            objStatements.TotalAdjustmentApplied = objSummary[0].TotalAdjustmentApplied;
            objStatements.TotalCashApplied = objSummary[0].TotalCashApplied;
            objStatements.TotalPriceApplied = objSummary[0].TotalPriceApplied;
            objStatements.OutstandingBalance = objSummary[0].OutstandingBalance;
            objStatements.RightstoRecieve = objSummary[0].RightstoRecieve;
            objStatements.AdminCharges = objSummary[0].AdminCharges;
            objStatements.NFC = objSummary[0].NFC;

            objStatements.StatementPeriod = StatementFrom.ToShortDateString() + " - " + StatementTo.ToShortDateString();

            return objStatements;
        }

        public IList<MPMerchantStatementsDetailModel> RetrieveAllMerchantStatements(int merchantId, DateTime StatementFrom, DateTime StatementTo)
        {
            IList<MPMerchantStatementModel> objDetails = merchantProfileRepository.RetrieveMerchantStatements(merchantId, StatementFrom, StatementTo);
            IList<MPMerchantStatementsDetailModel> objSummary = merchantProfileRepository.RetrieveMerchantStatementsSummary(merchantId, StatementFrom, StatementTo);

            foreach (MPMerchantStatementsDetailModel detail in objSummary)
            {
                detail.StatementsDetail = objDetails.Where(obj => obj.MerchantId == detail.TradeID).ToList();
                detail.StatementPeriod = StatementFrom + " - " + StatementTo;
            }

            //MPMerchantStatementsDetailModel objStatements = new MPMerchantStatementsDetailModel();
            //objStatements.StatementsDetail = objDetails.ToList();
            //objStatements.TradeID = objSummary[0].TradeID;
            //objStatements.FirmName = objSummary[0].FirmName;
            //objStatements.RNC = objSummary[0].RNC;
            //objStatements.TradeName = objSummary[0].TradeName;
            //objStatements.AddressDetail = objSummary[0].AddressDetail;
            //objStatements.TotalCashRecieved = objSummary[0].TotalCashRecieved;
            //objStatements.TotalAdjustmentApplied = objSummary[0].TotalAdjustmentApplied;
            //objStatements.TotalCashApplied = objSummary[0].TotalCashApplied;
            //objStatements.TotalPriceApplied = objSummary[0].TotalPriceApplied;
            //objStatements.OutstandingBalance = objSummary[0].OutstandingBalance;
            //objStatements.RightstoRecieve = objSummary[0].RightstoRecieve;
            //objStatements.AdminCharges = objSummary[0].AdminCharges;

            return objSummary;
        }

        #endregion

        #region History
        public MPMerchantHistoryDetailModel RetrieveMerchantHistory(int merchantId, DateTime HistoryStartDate, DateTime HistoryEndDate)
        {
            IList<MPMerchantHistoryModel> objDetails = merchantProfileRepository.RetrieveMerchantHistory(merchantId, HistoryStartDate, HistoryEndDate);
            MPMerchantHistoryDetailModel objActivities = new MPMerchantHistoryDetailModel();
            objActivities.HistoryDetail = objDetails.ToList();

            return objActivities;
        }

        #endregion

        #region Risk Evaluation
        public MPMerchantRiskEvaluationDetailModel RetrieveMerchantRiskEvaluation(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            IList<MPMerchantRiskEvaluationModel> objDetails = merchantProfileRepository.RetrieveMerchantRiskEvaluation(merchantId, StartDate, EndDate);
            MPMerchantRiskEvaluationDetailModel objEvaluation = new MPMerchantRiskEvaluationDetailModel();
            objEvaluation.RiskEvaluationDetail = objDetails.ToList();

            return objEvaluation;
        }

        #endregion

        #region Contracts
        public MPMerchantContractDetailModel RetrieveMerchantContracts(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            IList<MPMerchantContractModel> objDetails = merchantProfileRepository.RetrieveMerchantContracts(merchantId, StartDate, EndDate);
            MPMerchantContractDetailModel objContracts = new MPMerchantContractDetailModel();
            objContracts.ContractDetail = objDetails.ToList();

            return objContracts;
        }
        public MPMerchantContractModel RetrieveContractGeneralInformation(int contractId)
        {
            MPMerchantContractModel objDetails = merchantProfileRepository.RetrieveContractGeneralInformation(contractId);
            return objDetails;
        }
        public bool UpdateContractGeneralInformation(MPMerchantContractModel entity)
        {
                return merchantProfileRepository.UpdateContractGeneralInformation(entity);
        }
        public MPMerchantHistoryDetailModel RetrieveContractHistory(int contractId, DateTime HistoryStartDate, DateTime HistoryEndDate)
        {
            IList<MPMerchantHistoryModel> objDetails = merchantProfileRepository.RetrieveContractHistory(contractId, HistoryStartDate, HistoryEndDate);
            MPMerchantHistoryDetailModel objActivities = new MPMerchantHistoryDetailModel();
            objActivities.HistoryDetail = objDetails.ToList();

            return objActivities;
        }
        public MPMerchantActivityDetailModel RetrieveContractActivities(int contractId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate)
        {
            IList<MPMerchantActivityModel> objDetails = merchantProfileRepository.RetrieveContractActivities(contractId, ProcessorTypeId, ProcessorNumber, ActivityStartDate, ActivityEndDate);
            MPMerchantActivityDetailModel objActivities = new MPMerchantActivityDetailModel();
            objActivities.ActivityDetail = objDetails.ToList();

            return objActivities;
        }
        public MPMerchantContractModel RetrieveContractSalesRepresentative(int contactId)
        {
            IList<MPMerchantContractSalesRepresentativeModel> objDetails = merchantProfileRepository.RetrieveContractSalesRepresentative(contactId);
            MPMerchantContractModel objContracts = new MPMerchantContractModel();
            objContracts.SalesRep = objDetails.ToList();

            return objContracts;
        }
        public MPMerchantContractSalesRepresentativeModel RetrieveContractSalesRepresentativeDetail(int salesRepID)
        {
            MPMerchantContractSalesRepresentativeModel objDetails = merchantProfileRepository.RetrieveContractSalesRepresentativeDetail(salesRepID);

            return objDetails;
        }
        public bool UpdateContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel entity)
        {
                return merchantProfileRepository.UpdateContractSalesRepresentative(entity);
        }
        public bool AddContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel entity)
        {
                return merchantProfileRepository.AddContractSalesRepresentative(entity);
        }

        #endregion

        #region Collections
        public MPMerchantCollectionDetailModel RetrieveMerchantCollection(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            IList<MPMerchantCollectionModel> objDetails = merchantProfileRepository.RetrieveMerchantCollection(merchantId, StartDate, EndDate);
            MPMerchantCollectionDetailModel objCollections = new MPMerchantCollectionDetailModel();
            objCollections.CollectionDetail = objDetails;
            return objCollections;
        }

        #endregion

        #region Affiliations
        public MPMerchantAffiliationDetailModel RetrieveMerchanAffiliations(int merchantId, string RequestType)
        {
            MPMerchantAffiliationDetailModel objDetails = merchantProfileRepository.RetrieveMerchanAffiliations(merchantId, RequestType);
            return objDetails;
        }

        #endregion

        #region Credit Report
        public IList<GeneralModel> RetriveMerchantContracts(int merchantId)
        {
            return merchantProfileRepository.RetriveMerchantContracts(merchantId);
        }
        public IList<GeneralModel> RetriveMerchantContractCreditReports(int contractId)
        {
            return merchantProfileRepository.RetriveMerchantContractCreditReports(contractId);
        }
        public MPMerchantCreditReportDetailModel RetrieveMerchanCreditReport(int reportId)
        {
            IList<MPMerchantCreditReportModel> obj = merchantProfileRepository.RetrieveMerchantCreditReport(reportId);

            MPMerchantCreditReportDetailModel detail = new MPMerchantCreditReportDetailModel();
            
            detail.Business_Loans = obj.Where(o => (o.ReportType == "C") && (o.AnalysisType == 1)).FirstOrDefault();
            if (detail.Business_Loans == null)
                detail.Business_Loans = new MPMerchantCreditReportModel();
            
            detail.Business_CreditCards = obj.Where(o => (o.ReportType == "C") && (o.AnalysisType == 2)).FirstOrDefault();
            if (detail.Business_CreditCards == null)
                detail.Business_CreditCards = new MPMerchantCreditReportModel();
            
            detail.Business_Others = obj.Where(o => (o.ReportType == "C") && (o.AnalysisType == 3)).FirstOrDefault();
            if (detail.Business_Others == null)
                detail.Business_Others = new MPMerchantCreditReportModel();

            detail.Business_TotalCredit.AmountInLegal = detail.Business_Loans.AmountInLegal + detail.Business_CreditCards.AmountInLegal + detail.Business_Others.AmountInLegal;
            detail.Business_TotalCredit.ApprovedAmount = detail.Business_Loans.ApprovedAmount + detail.Business_CreditCards.ApprovedAmount + detail.Business_Others.ApprovedAmount;
            detail.Business_TotalCredit.DebtIndex = detail.Business_Loans.DebtIndex + detail.Business_CreditCards.DebtIndex + detail.Business_Others.DebtIndex;
            detail.Business_TotalCredit.LateAmount = detail.Business_Loans.LateAmount + detail.Business_CreditCards.LateAmount + detail.Business_Others.LateAmount;
            detail.Business_TotalCredit.LateIndex = detail.Business_Loans.LateIndex + detail.Business_CreditCards.LateIndex + detail.Business_Others.LateIndex;
            detail.Business_TotalCredit.MonthlyPayment = detail.Business_Loans.MonthlyPayment + detail.Business_CreditCards.MonthlyPayment + detail.Business_Others.MonthlyPayment;
            detail.Business_TotalCredit.NumberofLoans = detail.Business_Loans.NumberofLoans + detail.Business_CreditCards.NumberofLoans + detail.Business_Others.NumberofLoans;
            detail.Business_TotalCredit.OwnedAmount = detail.Business_Loans.OwnedAmount + detail.Business_CreditCards.OwnedAmount + detail.Business_Others.OwnedAmount;            

            detail.Owner_Loans = obj.Where(o => (o.ReportType == "O") && (o.AnalysisType == 1)).FirstOrDefault();
            if (detail.Owner_Loans == null)
                detail.Owner_Loans = new MPMerchantCreditReportModel();

            detail.Owner_CreditCards = obj.Where(o => (o.ReportType == "O") && (o.AnalysisType == 2)).FirstOrDefault();
            if (detail.Owner_CreditCards == null)
                detail.Owner_CreditCards = new MPMerchantCreditReportModel();

            detail.Owner_Others = obj.Where(o => (o.ReportType == "O") && (o.AnalysisType == 3)).FirstOrDefault();
            if (detail.Owner_Others == null)
                detail.Owner_Others = new MPMerchantCreditReportModel();

            detail.Owner_TotalCredit.AmountInLegal = detail.Owner_Loans.AmountInLegal + detail.Owner_CreditCards.AmountInLegal + detail.Owner_Others.AmountInLegal;
            detail.Owner_TotalCredit.ApprovedAmount = detail.Owner_Loans.ApprovedAmount + detail.Owner_CreditCards.ApprovedAmount + detail.Owner_Others.ApprovedAmount;
            detail.Owner_TotalCredit.DebtIndex = detail.Owner_Loans.DebtIndex + detail.Owner_CreditCards.DebtIndex + detail.Owner_Others.DebtIndex;
            detail.Owner_TotalCredit.LateAmount = detail.Owner_Loans.LateAmount + detail.Owner_CreditCards.LateAmount + detail.Owner_Others.LateAmount;
            detail.Owner_TotalCredit.LateIndex = detail.Owner_Loans.LateIndex + detail.Owner_CreditCards.LateIndex + detail.Owner_Others.LateIndex;
            detail.Owner_TotalCredit.MonthlyPayment = detail.Owner_Loans.MonthlyPayment + detail.Owner_CreditCards.MonthlyPayment + detail.Owner_Others.MonthlyPayment;
            detail.Owner_TotalCredit.NumberofLoans = detail.Owner_Loans.NumberofLoans + detail.Owner_CreditCards.NumberofLoans + detail.Owner_Others.NumberofLoans;
            detail.Owner_TotalCredit.OwnedAmount = detail.Owner_Loans.OwnedAmount + detail.Owner_CreditCards.OwnedAmount + detail.Owner_Others.OwnedAmount;

            detail.Total_TotalCredit.AmountInLegal = detail.Business_TotalCredit.AmountInLegal + detail.Owner_TotalCredit.AmountInLegal;
            detail.Total_TotalCredit.ApprovedAmount = detail.Business_TotalCredit.ApprovedAmount + detail.Owner_TotalCredit.ApprovedAmount;
            detail.Total_TotalCredit.DebtIndex = detail.Business_TotalCredit.DebtIndex + detail.Owner_TotalCredit.DebtIndex;
            detail.Total_TotalCredit.LateAmount = detail.Business_TotalCredit.LateAmount + detail.Owner_TotalCredit.LateAmount;
            detail.Total_TotalCredit.LateIndex = detail.Business_TotalCredit.LateIndex + detail.Owner_TotalCredit.LateIndex;
            detail.Total_TotalCredit.MonthlyPayment = detail.Business_TotalCredit.MonthlyPayment + detail.Owner_TotalCredit.MonthlyPayment;
            detail.Total_TotalCredit.NumberofLoans = detail.Business_TotalCredit.NumberofLoans + detail.Owner_TotalCredit.NumberofLoans;
            detail.Total_TotalCredit.OwnedAmount = detail.Business_TotalCredit.OwnedAmount + detail.Owner_TotalCredit.OwnedAmount;

            detail.Total_CreditCards.AmountInLegal = detail.Business_CreditCards.AmountInLegal + detail.Owner_CreditCards.AmountInLegal;
            detail.Total_CreditCards.ApprovedAmount = detail.Business_CreditCards.ApprovedAmount + detail.Owner_CreditCards.ApprovedAmount;
            detail.Total_CreditCards.DebtIndex = detail.Business_CreditCards.DebtIndex + detail.Owner_CreditCards.DebtIndex;
            detail.Total_CreditCards.LateAmount = detail.Business_CreditCards.LateAmount + detail.Owner_CreditCards.LateAmount;
            detail.Total_CreditCards.LateIndex = detail.Business_CreditCards.LateIndex + detail.Owner_CreditCards.LateIndex;
            detail.Total_CreditCards.MonthlyPayment = detail.Business_CreditCards.MonthlyPayment + detail.Owner_CreditCards.MonthlyPayment;
            detail.Total_CreditCards.NumberofLoans = detail.Business_CreditCards.NumberofLoans + detail.Owner_CreditCards.NumberofLoans;
            detail.Total_CreditCards.OwnedAmount = detail.Business_CreditCards.OwnedAmount + detail.Owner_CreditCards.OwnedAmount;

            detail.Total_Loans.AmountInLegal = detail.Business_Loans.AmountInLegal + detail.Owner_Loans.AmountInLegal;
            detail.Total_Loans.ApprovedAmount = detail.Business_Loans.ApprovedAmount + detail.Owner_Loans.ApprovedAmount;
            detail.Total_Loans.DebtIndex = detail.Business_Loans.DebtIndex + detail.Owner_Loans.DebtIndex;
            detail.Total_Loans.LateAmount = detail.Business_Loans.LateAmount + detail.Owner_Loans.LateAmount;
            detail.Total_Loans.LateIndex = detail.Business_Loans.LateIndex + detail.Owner_Loans.LateIndex;
            detail.Total_Loans.MonthlyPayment = detail.Business_Loans.MonthlyPayment + detail.Owner_Loans.MonthlyPayment;
            detail.Total_Loans.NumberofLoans = detail.Business_Loans.NumberofLoans + detail.Owner_Loans.NumberofLoans;
            detail.Total_Loans.OwnedAmount = detail.Business_Loans.OwnedAmount + detail.Owner_Loans.OwnedAmount;

            detail.Total_Others.AmountInLegal = detail.Business_Others.AmountInLegal + detail.Owner_Others.AmountInLegal;
            detail.Total_Others.ApprovedAmount = detail.Business_Others.ApprovedAmount + detail.Owner_Others.ApprovedAmount;
            detail.Total_Others.DebtIndex = detail.Business_Others.DebtIndex + detail.Owner_Others.DebtIndex;
            detail.Total_Others.LateAmount = detail.Business_Others.LateAmount + detail.Owner_Others.LateAmount;
            detail.Total_Others.LateIndex = detail.Business_Others.LateIndex + detail.Owner_Others.LateIndex;
            detail.Total_Others.MonthlyPayment = detail.Business_Others.MonthlyPayment + detail.Owner_Others.MonthlyPayment;
            detail.Total_Others.NumberofLoans = detail.Business_Others.NumberofLoans + detail.Owner_Others.NumberofLoans;
            detail.Total_Others.OwnedAmount = detail.Business_Others.OwnedAmount + detail.Owner_Others.OwnedAmount;

            return detail;
        }
        public MPMerchantCreditReportDetailModel RetrieveMerchanCreditReportInformation(int contractId)
        {
            IList<MPMerchantCreditReportModel> obj = merchantProfileRepository.RetrieveMerchantCreditReportInformation(contractId);

            MPMerchantCreditReportDetailModel detail = new MPMerchantCreditReportDetailModel();

            detail.Business_Loans = obj.Where(o => (o.ReportType == "C") && (o.AnalysisType == 1)).FirstOrDefault();
            if (detail.Business_Loans == null)
                detail.Business_Loans = new MPMerchantCreditReportModel();

            detail.Business_CreditCards = obj.Where(o => (o.ReportType == "C") && (o.AnalysisType == 2)).FirstOrDefault();
            if (detail.Business_CreditCards == null)
                detail.Business_CreditCards = new MPMerchantCreditReportModel();

            detail.Business_Others = obj.Where(o => (o.ReportType == "C") && (o.AnalysisType == 3)).FirstOrDefault();
            if (detail.Business_Others == null)
                detail.Business_Others = new MPMerchantCreditReportModel();

            detail.Business_TotalCredit.AmountInLegal = detail.Business_Loans.AmountInLegal + detail.Business_CreditCards.AmountInLegal + detail.Business_Others.AmountInLegal;
            detail.Business_TotalCredit.ApprovedAmount = detail.Business_Loans.ApprovedAmount + detail.Business_CreditCards.ApprovedAmount + detail.Business_Others.ApprovedAmount;
            detail.Business_TotalCredit.DebtIndex = detail.Business_Loans.DebtIndex + detail.Business_CreditCards.DebtIndex + detail.Business_Others.DebtIndex;
            detail.Business_TotalCredit.LateAmount = detail.Business_Loans.LateAmount + detail.Business_CreditCards.LateAmount + detail.Business_Others.LateAmount;
            detail.Business_TotalCredit.LateIndex = detail.Business_Loans.LateIndex + detail.Business_CreditCards.LateIndex + detail.Business_Others.LateIndex;
            detail.Business_TotalCredit.MonthlyPayment = detail.Business_Loans.MonthlyPayment + detail.Business_CreditCards.MonthlyPayment + detail.Business_Others.MonthlyPayment;
            detail.Business_TotalCredit.NumberofLoans = detail.Business_Loans.NumberofLoans + detail.Business_CreditCards.NumberofLoans + detail.Business_Others.NumberofLoans;
            detail.Business_TotalCredit.OwnedAmount = detail.Business_Loans.OwnedAmount + detail.Business_CreditCards.OwnedAmount + detail.Business_Others.OwnedAmount;

            detail.Owner_Loans = obj.Where(o => (o.ReportType == "O") && (o.AnalysisType == 1)).FirstOrDefault();
            if (detail.Owner_Loans == null)
                detail.Owner_Loans = new MPMerchantCreditReportModel();

            detail.Owner_CreditCards = obj.Where(o => (o.ReportType == "O") && (o.AnalysisType == 2)).FirstOrDefault();
            if (detail.Owner_CreditCards == null)
                detail.Owner_CreditCards = new MPMerchantCreditReportModel();

            detail.Owner_Others = obj.Where(o => (o.ReportType == "O") && (o.AnalysisType == 3)).FirstOrDefault();
            if (detail.Owner_Others == null)
                detail.Owner_Others = new MPMerchantCreditReportModel();

            detail.Owner_TotalCredit.AmountInLegal = detail.Owner_Loans.AmountInLegal + detail.Owner_CreditCards.AmountInLegal + detail.Owner_Others.AmountInLegal;
            detail.Owner_TotalCredit.ApprovedAmount = detail.Owner_Loans.ApprovedAmount + detail.Owner_CreditCards.ApprovedAmount + detail.Owner_Others.ApprovedAmount;
            detail.Owner_TotalCredit.DebtIndex = detail.Owner_Loans.DebtIndex + detail.Owner_CreditCards.DebtIndex + detail.Owner_Others.DebtIndex;
            detail.Owner_TotalCredit.LateAmount = detail.Owner_Loans.LateAmount + detail.Owner_CreditCards.LateAmount + detail.Owner_Others.LateAmount;
            detail.Owner_TotalCredit.LateIndex = detail.Owner_Loans.LateIndex + detail.Owner_CreditCards.LateIndex + detail.Owner_Others.LateIndex;
            detail.Owner_TotalCredit.MonthlyPayment = detail.Owner_Loans.MonthlyPayment + detail.Owner_CreditCards.MonthlyPayment + detail.Owner_Others.MonthlyPayment;
            detail.Owner_TotalCredit.NumberofLoans = detail.Owner_Loans.NumberofLoans + detail.Owner_CreditCards.NumberofLoans + detail.Owner_Others.NumberofLoans;
            detail.Owner_TotalCredit.OwnedAmount = detail.Owner_Loans.OwnedAmount + detail.Owner_CreditCards.OwnedAmount + detail.Owner_Others.OwnedAmount;

            detail.Total_TotalCredit.AmountInLegal = detail.Business_TotalCredit.AmountInLegal + detail.Owner_TotalCredit.AmountInLegal;
            detail.Total_TotalCredit.ApprovedAmount = detail.Business_TotalCredit.ApprovedAmount + detail.Owner_TotalCredit.ApprovedAmount;
            detail.Total_TotalCredit.DebtIndex = detail.Business_TotalCredit.DebtIndex + detail.Owner_TotalCredit.DebtIndex;
            detail.Total_TotalCredit.LateAmount = detail.Business_TotalCredit.LateAmount + detail.Owner_TotalCredit.LateAmount;
            detail.Total_TotalCredit.LateIndex = detail.Business_TotalCredit.LateIndex + detail.Owner_TotalCredit.LateIndex;
            detail.Total_TotalCredit.MonthlyPayment = detail.Business_TotalCredit.MonthlyPayment + detail.Owner_TotalCredit.MonthlyPayment;
            detail.Total_TotalCredit.NumberofLoans = detail.Business_TotalCredit.NumberofLoans + detail.Owner_TotalCredit.NumberofLoans;
            detail.Total_TotalCredit.OwnedAmount = detail.Business_TotalCredit.OwnedAmount + detail.Owner_TotalCredit.OwnedAmount;

            detail.Total_CreditCards.AmountInLegal = detail.Business_CreditCards.AmountInLegal + detail.Owner_CreditCards.AmountInLegal;
            detail.Total_CreditCards.ApprovedAmount = detail.Business_CreditCards.ApprovedAmount + detail.Owner_CreditCards.ApprovedAmount;
            detail.Total_CreditCards.DebtIndex = detail.Business_CreditCards.DebtIndex + detail.Owner_CreditCards.DebtIndex;
            detail.Total_CreditCards.LateAmount = detail.Business_CreditCards.LateAmount + detail.Owner_CreditCards.LateAmount;
            detail.Total_CreditCards.LateIndex = detail.Business_CreditCards.LateIndex + detail.Owner_CreditCards.LateIndex;
            detail.Total_CreditCards.MonthlyPayment = detail.Business_CreditCards.MonthlyPayment + detail.Owner_CreditCards.MonthlyPayment;
            detail.Total_CreditCards.NumberofLoans = detail.Business_CreditCards.NumberofLoans + detail.Owner_CreditCards.NumberofLoans;
            detail.Total_CreditCards.OwnedAmount = detail.Business_CreditCards.OwnedAmount + detail.Owner_CreditCards.OwnedAmount;

            detail.Total_Loans.AmountInLegal = detail.Business_Loans.AmountInLegal + detail.Owner_Loans.AmountInLegal;
            detail.Total_Loans.ApprovedAmount = detail.Business_Loans.ApprovedAmount + detail.Owner_Loans.ApprovedAmount;
            detail.Total_Loans.DebtIndex = detail.Business_Loans.DebtIndex + detail.Owner_Loans.DebtIndex;
            detail.Total_Loans.LateAmount = detail.Business_Loans.LateAmount + detail.Owner_Loans.LateAmount;
            detail.Total_Loans.LateIndex = detail.Business_Loans.LateIndex + detail.Owner_Loans.LateIndex;
            detail.Total_Loans.MonthlyPayment = detail.Business_Loans.MonthlyPayment + detail.Owner_Loans.MonthlyPayment;
            detail.Total_Loans.NumberofLoans = detail.Business_Loans.NumberofLoans + detail.Owner_Loans.NumberofLoans;
            detail.Total_Loans.OwnedAmount = detail.Business_Loans.OwnedAmount + detail.Owner_Loans.OwnedAmount;

            detail.Total_Others.AmountInLegal = detail.Business_Others.AmountInLegal + detail.Owner_Others.AmountInLegal;
            detail.Total_Others.ApprovedAmount = detail.Business_Others.ApprovedAmount + detail.Owner_Others.ApprovedAmount;
            detail.Total_Others.DebtIndex = detail.Business_Others.DebtIndex + detail.Owner_Others.DebtIndex;
            detail.Total_Others.LateAmount = detail.Business_Others.LateAmount + detail.Owner_Others.LateAmount;
            detail.Total_Others.LateIndex = detail.Business_Others.LateIndex + detail.Owner_Others.LateIndex;
            detail.Total_Others.MonthlyPayment = detail.Business_Others.MonthlyPayment + detail.Owner_Others.MonthlyPayment;
            detail.Total_Others.NumberofLoans = detail.Business_Others.NumberofLoans + detail.Owner_Others.NumberofLoans;
            detail.Total_Others.OwnedAmount = detail.Business_Others.OwnedAmount + detail.Owner_Others.OwnedAmount;

            return detail;
        }

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}