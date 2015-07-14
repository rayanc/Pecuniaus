using System.Collections.Generic;
using Pecuniaus.Models.Contract;
using Pecuniaus.Models;
using Pecuniaus.Models.Merchant;

namespace Pecuniaus.ApiHelper
{
    public class MerchantApi
    {
        public DataEntryModel Info(long merchantId)
        {
            var apiMethod = string.Format("merchants/info/{0}", merchantId);

            var model = BaseApiData.GetAPIResult<DataEntryModel>(apiMethod, () => new DataEntryModel()) ?? new DataEntryModel { };
            model.LandlordInformation = model.LandlordInformation ?? new LandlordInformationModel();
            model.LandlordId = model.LandlordInformation.LandlordId;
            model.CompanyName = model.LandlordInformation.CompanyName;
            model.FirstName = model.LandlordInformation.FirstName;
            model.LastName = model.LandlordInformation.LastName;
            model.PhoneNumber = model.LandlordInformation.PhoneNumber;

            return model;
        }

        public MerchantInformationOfferModel GetOfferDetails(long merchantId, long contractId)
        {
            var apiMethod = string.Format("creditreport/offers/{0}?contractId={1}", merchantId, contractId);

            return BaseApiData.GetAPIResult<MerchantInformationOfferModel>(apiMethod, () => new MerchantInformationOfferModel()) ?? new MerchantInformationOfferModel { };

        }

        public void KickBack(long merchantId, long taskTypeId, long contractId)
        {
            long workflowId = 1;
            var apiMethod = string.Format("gen/kickback?merchantId={0}&tasktypeId={1}&workflowId={2}&contractid={3}", merchantId, taskTypeId, workflowId, contractId);
            BaseApiData.PostAPIData(apiMethod, null);
        }

        public MerchantModel Information(int merchantId)
        {
            var apiMethod = string.Format("merchants/merchantinfo/{0}", merchantId);

            return BaseApiData.GetAPIResult<MerchantModel>(apiMethod, () => new MerchantModel()) ?? new MerchantModel();
        }

        public MerchantOffer GetMerchantOffer(long merchantId, long contractId)
        {
            var apiMethod = string.Format("merchants/offer/{0}/{1}", merchantId, contractId);

            return BaseApiData.GetAPIResult<MerchantOffer>(apiMethod, () => new MerchantOffer()) ?? new MerchantOffer();
        }

    }
}
