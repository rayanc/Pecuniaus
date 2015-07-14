using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Repository;
using Bridge.Models;


namespace Bridge.BusinessTier
{
    public interface IQuestion : IRepository<QuestionsModel, int>
    {
        #region Methods
        /// <summary>
        /// To select question and answers
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        IList<QuestionsModel> ListQuesAnswers(string entity, Int64? contractId);

        /// <summary>
        /// To insert or Update the Answers
        /// </summary>
        /// <param name="strAnswerXml"></param>
        /// <returns></returns>
        bool InsUpdateAnswers(string strAnswerXml, Int64 taskTypeId, Int64 workflowId, Int16 isCompleted, string entity, string scriptFile);
        #endregion

    }
}