
using System.Collections.Generic;
using System.Web.Mvc;
namespace Pecuniaus.Models.Contract
{
   public class QuestionModel
    {
       public QuestionModel()
       {
           DropDown = new List<SelectListItem>();
       }
        public long QuestionId { get; set; }
        public string QuestionDesc { get; set; }
        public string QuestionType { get; set; }
        public long AnswerId { get; set; }
        public string AnswerDesc { get; set; }
        public string Entity { get; set; }
        public long ContractId { get; set; }
        public long MerchantId { get; set; }

        public IEnumerable<SelectListItem> DropDown { get; set; }

        public string scriptFile { get; set; }
        public string ScriptFilePath { get; set; }
    }
}
