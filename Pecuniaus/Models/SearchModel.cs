using System.Collections.Generic;
using System.Web.Mvc;

namespace Pecuniaus.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            Users = new List<SelectListItem>();
        }

        public string MerchantInformation { get; set; }
        public IEnumerable<GeneralModel> TaskNameList { get; set; }
        public long TaskTypeId { get; set; }
        //public long TaskStatusId { get; set; }
        public IEnumerable<GeneralModel> TaskStatusList { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
  
        public long Merchantid { get; set; }
        public string BusinessName { get; set; }
        public string legalName { get; set; }
        public string RNC { get; set; }
        public string OwnerName { get; set; }
        public long WorkFlowID { get; set; }
        public string SearchType { get; set; }

        public bool ShowSF { get; set; }

        public string MerchantIdName { get; set; }
        public string ContractId { get; set; }
        public string TaskNameId { get; set; }
        public string TaskStatusId{ get; set; }
        public string UserId { get; set; }
    }

}
