using Bridge.Models.Merchant;
using System.Collections.Generic;

namespace Bridge.Models
{
    public class VerificationModel
    {
        public string ScriptFile { get; set; }
        public MerchantDetailQuestionModel MerchantDetails { get; set; }
        public IEnumerable<QuestionsModel> questions { get; set; }

    }
}