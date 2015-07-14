using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class QuestionsModel
    {
        public Int64 questionId { get; set; }
        public string questionDesc { get; set; }
        public string questionType { get; set; }
        public Int64 answerId { get; set; }
        public string answerDesc { get; set; }
        public string entity { get; set; }
        public Int64 contractId { get; set; }
        public Int64 merchantId { get; set; }
        public string scriptFile { get; set; }
    }

    public class QuestionsModel<T>
    {
        public Int64 questionId { get; set; }
        public Int64 answerId { get; set; }
        public string answerDesc { get; set; }
        public Int64 contractId { get; set; }
    }
}