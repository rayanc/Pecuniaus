using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;


namespace Bridge.Repository
{
    public class QuestionRepository : IQuestion, IDisposable
    {
        #region Methods
        /// <summary>
        /// To select question and answers
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public IList<QuestionsModel> ListQuesAnswers(string entity, Int64? contractId)
        {
            QuestionsModel model = new QuestionsModel();
            return new DataAccess.DataAccess().ExecuteReader<QuestionsModel>("AVZ_QUS_spRetriveQuestions", new { Entity = entity, ContractId = contractId });
        }

        /// <summary>
        /// To Insert or Update Answers
        /// </summary>
        /// <param name="strAnswerXml"></param>
        /// <returns></returns>
        public bool InsUpdateAnswers(string strAnswerXml, Int64 taskTypeId, Int64 workflowId, Int16 isCompleted, string entity, string scriptFile)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_QUS_spInsUpdateAnswers", new { AnswersXml = strAnswerXml, taskTypeId = taskTypeId, workflowId = workflowId, isCompleted = isCompleted, entity = entity, scriptFile=scriptFile });
        }

        public bool Update(QuestionsModel model)
        {
            return true;
        }

        public bool Remove(int id)
        {
            return true;
        }

        public bool Create(QuestionsModel model)
        {
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}