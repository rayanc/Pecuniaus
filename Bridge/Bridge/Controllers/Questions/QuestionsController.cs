using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models;
using Bridge.BusinessTier;

namespace Bridge.Controllers.Questions
{
    [RoutePrefix("questions")]
    public class QuestionsController : ApiController
    {
        #region Methods
        /// <summary>
        /// To retrieve all questions and answers related to a entity(verification script, landlord verification): ContractId is nullable
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage RetriveQuestions(string entity, Int64? contractId=0)
        {
            IList<QuestionsModel> response;
            using (QuestionTier dT = new QuestionTier())
            {
                response = dT.RetrieveQuesAnswers(entity,contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// To Insert or Update the Answers
        /// </summary>
        /// <param name="listAnswers"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateAnswers")]
        public HttpResponseMessage UpdateAnswers(List<QuestionsModel> listAnswers, Int64 taskTypeId, Int64 workflowId, Int16 isCompleted, string entity = "Contract", string scriptFile="")
        {
            using (QuestionTier mt = new QuestionTier())
            {
                if (mt.InsUpdateAnswers(listAnswers, taskTypeId, workflowId, isCompleted, entity, scriptFile))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
        }
        #endregion

    }
}
