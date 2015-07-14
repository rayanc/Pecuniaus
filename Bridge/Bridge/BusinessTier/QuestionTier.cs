using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;
using System.Xml;
using System.Text;
using System.IO;

namespace Bridge.BusinessTier
{
    public class QuestionTier : IDisposable
    {
        #region Private Variables

        private IQuestion questionsRepository;

        #endregion

        #region Contructors

        public QuestionTier() : this(new QuestionRepository()) { }
        public QuestionTier(IQuestion questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// To select question and answers
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<QuestionsModel> RetrieveQuesAnswers(string entity, Int64? contractId)
        {
            return questionsRepository.ListQuesAnswers(entity, contractId);
        }

        /// <summary>
        /// To insert or Update the Answers
        /// </summary>
        /// <param name="listAnswers"></param>
        /// <returns></returns>
        public bool InsUpdateAnswers(List<QuestionsModel> listAnswers, Int64 taskTypeId, Int64 workflowId, Int16 isCompleted, string entity, string scriptFile)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;
            StringBuilder sbAnswerXml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sbAnswerXml, settings);
            writer.WriteStartElement("answerslist");
            foreach (QuestionsModel ans in listAnswers)
            {
                writer.WriteStartElement("answers");
                writer.WriteAttributeString("AnswerId", Convert.ToString(ans.answerId));
                writer.WriteAttributeString("QuestionId", Convert.ToString(ans.questionId));
                writer.WriteAttributeString("AnswerText", ans.answerDesc);
                writer.WriteAttributeString("ContractId", Convert.ToString(ans.contractId));
                writer.WriteAttributeString("MerchantId", Convert.ToString(ans.merchantId));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();

            return questionsRepository.InsUpdateAnswers(sbAnswerXml.ToString(), taskTypeId, workflowId, isCompleted, entity, scriptFile);
        }

        

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}