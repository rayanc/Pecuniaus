
using System.Collections.Generic;
using System.Web.Mvc;
namespace Pecuniaus.Models
{
    public class NotesModel
    {

        public string[] SelectedEmail { get; set; }
        public IEnumerable<SelectListItem> EmailList { get; set; }
        public long ContractId { get; set; }
        public string InsertDate { get; set; }
        public long MerchantId { get; set; }
        public string Note { get; set; }
        public long NoteId { get; set; }
        public long NoteTypeId { get; set; }
        public string noteTypeDescription { get; set; }
        public string ScreenName { get; set; }
        public long WorkFlowId { get; set; }
        public long InsertUserId { get; set; }
        public string UserName { get; set; }

        public IEnumerable<SelectListItem> NoteTypes { get; set; }
    }
}
