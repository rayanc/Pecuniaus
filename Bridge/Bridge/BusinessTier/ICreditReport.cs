using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bridge.Models;
using Bridge.Repository;

namespace Bridge.BusinessTier
{
    public interface ICreditReport : IRepository<CreditReportModel, int>
    {
        bool InsertCreditreport(string dataxml, long merchantId, long contractId, string isCompleted, byte[] image, string rawdata, int isavailable);
        bool InsertOffers(MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int iscompleted);
        IList<OfferModel> RetrieveOffers(long merchantId, Int64 contractId);
        IList<CreditReportModel> RetrieveCreditReport(long merchantId, Int64 contractId);
        MerchantInformationOfferModel RetrieveMerchantCreditInformation(Int64 merchantId, Int64 contractId);
        RetrieveMerchantInformation RetrieveMerchantInformation(Int64 merchantId,Int64 contractId);
        bool SelectOffer(Int64 offerId, Int64 merchantId, Int64 contractId, int isCompleted);
        DataSet RetrieveInformationforScorin(Int64 merchantId);
        bool CompleteTask(RetrieveMerchantInformation model,Int64 tasktypeid, Int64 merchantId, Int64 contractId, int iscompleted, int workflowid,string rawdata,string requestxml);
        bool CompleteTask(double score, double roundedscore, string finalletter, Int64 tasktypeid, Int64 merchantId, Int64 contractId, int iscompleted, int workflowid, string rawdata, string requestxml);
        bool InsertSelectOffers(MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int iscompleted);
        bool OfferAcceptance(OfferModel model, Int64 merchantId, Int64 contractId, int iscompleted);

        bool UpdateOfferEmailFlag(bool isEmailSent, long offerId, long merchantId, long contractId);
        bool UpdateOfferCreationEmailFlag(bool isEmailSent, long merchantId, long contractId);
    }
}
