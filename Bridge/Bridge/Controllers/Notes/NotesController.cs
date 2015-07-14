using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Bridge.BusinessTier;
using System.Net;
using Bridge.Models;
using System.Data;
namespace Bridge.Controllers
{
    [RoutePrefix("notes")]
    public class NotesController : ApiController
    {

      [HttpGet]
      [Route("retrivenotes")]
      [Queryable]
      public IQueryable<NotesModel> RetriveNotes(Int64 merchantId)
        {
            IList<NotesModel> notes;
            using (NotesTier mt = new NotesTier())
            {
                notes = mt.Retrieve(merchantId);

                //return this.Request.CreateResponse(HttpStatusCode.OK, notes);
                return notes.AsQueryable();


            }
            //DataSet  notes;
            //using (NotesTier mt = new NotesTier())
            //{
            //    notes = mt.RetrieveNotes(merchantId);
            //    if (notes.Tables[0].Rows.Count > 0)
            //    {
            //        return this.Request.CreateResponse(HttpStatusCode.OK,
            //            new List<NotesModel>(){
            //         new NotesModel()
            //        {
            //            //noteId = notes.Tables[0].Rows[0]["noteId"],
            //            //notetypeId = notes.Tables[0].Rows[0]["notetypeId"],
            //            merchantId = Convert.ToInt32(notes.Tables[0].Rows[0]["merchantId"]),
            //            contractId = Convert.ToInt32(notes.Tables[0].Rows[0]["contractId"]),
            //            note = Convert.ToString(notes.Tables[0].Rows[0]["note"]),
            //            workFlowId = Convert.ToInt32(notes.Tables[0].Rows[0]["workFlowId"]),
            //            screenName = Convert.ToString(notes.Tables[0].Rows[0]["screenName"])
            //        }});
            //    }
            //    else
            //    {
            //        return this.Request.CreateResponse(HttpStatusCode.OK);
            //    }
                
            //}
        }
      [HttpPost]
      [Route("save")]
      public HttpResponseMessage InsertNote([FromBody]NotesModel model)
      {
          using (NotesTier mt = new NotesTier())
          {
              if (mt.Insert(model))
                  return this.Request.CreateResponse(HttpStatusCode.OK);

              else
              {
                  return this.Request.CreateResponse(HttpStatusCode.BadRequest);
              }
          }
      }

        #region Temp Notes 

      [HttpGet]
      [Route("retrivenotes/temp")]
      public HttpResponseMessage RetriveTempNotes(Int64 merchantId)
      {
          IList<NotesModel> notes;
          using (NotesTier mt = new NotesTier())
          {
              notes = mt.RetrieveTempNotes(merchantId);

              return this.Request.CreateResponse(HttpStatusCode.OK, notes);


          }
      }
      [HttpPost]
      [Route("save/temp")]
      public HttpResponseMessage InsertTempNote([FromBody]NotesModel model)
      {
          using (NotesTier mt = new NotesTier())
          {
              if (mt.InsertTempNotes(model))
                  return this.Request.CreateResponse(HttpStatusCode.OK);

              else
              {
                  return this.Request.CreateResponse(HttpStatusCode.BadRequest);
              }
          }
      }
        #endregion
    }
}
