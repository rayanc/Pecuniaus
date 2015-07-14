using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models;
using Bridge.BusinessTier;

namespace Bridge.Controllers.Notification
{
    [RoutePrefix("notification")]
    public class NotificationController : ApiController
    {
        #region Notification
        [HttpGet]
        [Route("retrieve")]
        public HttpResponseMessage RetrieveNotifications()
        {
            IList<NotificationModel> response;
            using (NotificationTier mt = new NotificationTier())
            {
                response = mt.RetrieveNotifications();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddNotification(NotificationModel obj)
        {
            using (NotificationTier mt = new NotificationTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.AddNotification(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage UpdateNotificationStatus(NotificationModel obj)
        {
            using (NotificationTier mt = new NotificationTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateNotificationStatus(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        #endregion

        #region Notification Email Groups
        [HttpGet]
        [Route("retrievenotificationgroups")]
        public HttpResponseMessage RetrieveNotificationGroups()
        {
            IList<NotificationGroupsModel> response;
            using (NotificationTier mt = new NotificationTier())
            {
                response = mt.RetrieveNotificationGroups();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPost]
        [Route("managenotificationgroups")]
        public HttpResponseMessage ManageNotificationGroups(NotificationGroupsModel obj)
        {
            using (NotificationTier mt = new NotificationTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.ManageNotificationGroups(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        #endregion

        #region Processor

        [HttpGet]
        [Route("GetAllProcessors")]
        public HttpResponseMessage GetAllProcessors() 
        {
            IList<ProcessorLookupModel> response;
            using (ProcessorTier mt = new ProcessorTier())
            {
                response = mt.GetAllProcessors();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("AddUpdateProcessor")]
        public HttpResponseMessage AddUpdateProcessor(ProcessorLookupModel obj)
        {
            using (ProcessorTier mt = new ProcessorTier()) 
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.AddUpdateProcessor(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }

        [HttpPost]
        [Route("CheckProcessorExist")] 
        public HttpResponseMessage CheckProcessorExist(ProcessorLookupModel processor)
        {
            bool response;
            using (ProcessorTier usertier = new ProcessorTier())
            {
                response = usertier.CheckProcessorExist(processor);
                if (response)
                { return this.Request.CreateResponse(HttpStatusCode.OK, response); }
                else
                    return this.Request.CreateResponse(HttpStatusCode.Accepted, response);

            }
        }

        #endregion

    }
}
