
namespace Pecuniaus.Models.Merchant
{
    public class MPMerchantOwnerDetailModel
    {
        public MPMerchantOwnerDetailModel()
        {
            Owner = new MPMerchantOwnersModel();
            Address = new MPMerchantAddressInfoModel();
        }

        public long MerchantId { get; set; }
        public MPMerchantOwnersModel Owner { get; set; }
        public MPMerchantAddressInfoModel Address { get; set; }
    }
}
