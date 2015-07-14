using Pecuniaus.Models.Merchant;

namespace Pecuniaus.ApiHelper
{
    public class MerchantProfileApi
    {
        public MPMerchantOwnerDetailModel GetOwnerDetails(long merchantId, long ownerId)
        {
            string apiMethod = string.Format("merchantprofile/{0}/Owners/details?OwnerId={1}", merchantId, ownerId);
            return BaseApiData.GetAPIResult<MPMerchantOwnerDetailModel>(apiMethod, () => new MPMerchantOwnerDetailModel()) ?? new MPMerchantOwnerDetailModel { };

        }

        public void UpdateOwner(long merchantId, MPMerchantOwnerDetailModel  model )
        {
            string apiMethod = string.Format("merchantprofile/{0}/Owner/update", merchantId);
            BaseApiData.PutAPIData(apiMethod, model);
              
        }
    }
}
