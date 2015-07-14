using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.Models;

namespace Bridge.BusinessTier
{
    public interface IMerchantProfile
    {
        //
        IList<GeneralModel> RetrieveStatus(string statusType);
        Dictionary<string, string> RetrieveMerchantBusinessInformation(int merchantId);
        IList<MPMerchantProcessorInfoModel> RetrieveMerchantBusinessProcessorInformation(int merchantId);
        bool UpdateMerchantBusinessInformation(MPMerchantBusinessModel entity);
        bool UpdateMerchantBusinessProcessorInformation(Int64 MerchantId, int Terminal, int processorTypeId, string processorNumber, DateTime dateGracePeriod, int industryTypeID, DateTime firstProcessedDate, int processorId);
        bool UpdateMerchantBusinessProcessorInformation(MPMerchantProcessorInfoModel entity);
        //
        Dictionary<string, string> RetrieveMerchantLandlordInformation(int merchantId);
        MPMerchantLandlordQuestionsModel RetrieveMerchantLandlordQuestionInformation(int merchantId);
        bool UpdateMerchantLandlordInformation(MPMerchantLandlordModel entity);
        //
        IList<MPMerchantContactsModel> RetrieveMerchantContacts(int merchantId);
        MPMerchantContactDetailModel RetrieveMerchantContactsInformation(int contactId);
        bool UpdateMerchantContactInformation(MPMerchantContactDetailModel entity);
        bool AddMerchantContactInformation(MPMerchantContactDetailModel entity);
        bool DeleteMerchantContacts(int contactId);
        //
        IList<MPMerchantOwnersModel> RetrieveMerchantOwners(int merchantId);
        MPMerchantOwnerDetailModel RetrieveMerchantOwnersInformation(int OwnerId);
        bool UpdateMerchantOwnerInformation(MPMerchantOwnerDetailModel entity);
        bool AddMerchantOwnerInformation(MPMerchantOwnerDetailModel entity);
        bool DeleteMerchantOwners(int OwnerID);
        //
        IList<MPMerchantProfileDetailModel> RetrieveMerchantProfiles(int merchantId, int ProcessorId, string ProcessorNumber);
        IList<MPMerchantProfilesModel> RetrieveMerchantProfileDetails(int merchantId, int ProcessorId, string ProcessorNumber);
        IList<MPMerchantProfilesModel> RetrieveMerchantProfileDetails(int ActivityId);
        bool UpdateMerchantProfileInformation(MPMerchantProfileDetailModel entity);
        //
        IList<MPMerchantDocumentModel> RetrieveMerchantDocuments(int merchantId, int DocumentTypeId);
        bool DeleteMerchantDocuments(int documentId);
        //
        IList<MPMerchantActivityModel> RetrieveMerchantActivities(int merchantId, int ProcessorTypeId, string MonthName);
        IList<MPMerchantActivityModel> RetrieveMerchanActivitiesMonthly(int merchantId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate);
        //
        IList<MPMerchantStatementModel> RetrieveMerchantStatements(int merchantId, DateTime StatementFrom, DateTime StatementTo);
        IList<MPMerchantStatementsDetailModel> RetrieveMerchantStatementsSummary(int merchantId, DateTime StatementFrom, DateTime StatementTo);
        //
        IList<MPMerchantHistoryModel> RetrieveMerchantHistory(int merchantId, DateTime HistoryStartDate, DateTime HistoryEndDate);
        //
        IList<MPMerchantRiskEvaluationModel> RetrieveMerchantRiskEvaluation(int merchantId, DateTime StartDate, DateTime EndDate);
        //
        IList<MPMerchantContractModel> RetrieveMerchantContracts(int merchantId, DateTime StartDate, DateTime EndDate);
        MPMerchantContractModel RetrieveContractGeneralInformation(int contractId);
        bool UpdateContractGeneralInformation(MPMerchantContractModel entity);
        IList<MPMerchantActivityModel> RetrieveContractActivities(int contractId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate);
        IList<MPMerchantHistoryModel> RetrieveContractHistory(int contractId, DateTime HistoryStartDate, DateTime HistoryEndDate);
        IList<MPMerchantContractSalesRepresentativeModel> RetrieveContractSalesRepresentative(int contractId );
        MPMerchantContractSalesRepresentativeModel RetrieveContractSalesRepresentativeDetail(int SalesRepId);
        bool UpdateContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel entity);
        bool AddContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel entity);

        //
        IList<MPMerchantCollectionModel> RetrieveMerchantCollection(int merchantId, DateTime StartDate, DateTime EndDate);

        //
        MPMerchantAffiliationDetailModel RetrieveMerchanAffiliations(int merchantId, string RequestType);

        //
        IList<GeneralModel> RetriveMerchantContracts(int merchantId);
        IList<GeneralModel> RetriveMerchantContractCreditReports(int contractId);
        IList<MPMerchantCreditReportModel> RetrieveMerchantCreditReport(int reportId);
        IList<MPMerchantCreditReportModel> RetrieveMerchantCreditReportInformation(int contractId);
    }
}
