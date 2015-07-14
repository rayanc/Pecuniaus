using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using System.Data;

namespace Bridge.Repository
{
    public class NotesRepository : INotes, IDisposable
    {
        public DataSet RetrieveNotes(Int64 merchantId)
        {
            
            return new DataAccess.DataAccess().ExecuteDataSet("avz_notes_spRetrieveNotes", new { merchantId=merchantId});
            
        }

        public IList<NotesModel> Retrieve(Int64 merchantId)
        {
            return new DataAccess.DataAccess().ExecuteReader<NotesModel>("avz_notes_spRetrieveNotes", new { merchantId = merchantId });
        }

        public bool Insert(NotesModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_notes_spInsertNotes", new { 
                merchantId = entity.merchantId, 
                screenName = entity.screenName, 
                noteTypeId=entity.noteTypeId,
                contractId=entity.contractId,
                note=entity.note,
                workFlowId=entity.workFlowId,
                insertUserId= entity.InsertUserId                
            });
        }
        /// <summary>
        /// Insert notes in temp table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertTempNotes(NotesModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_notes_spInsertTempNotes", new
            {
                merchantId = entity.merchantId,
                screenName = entity.screenName,
                noteTypeId = entity.noteTypeId,
                contractId = entity.contractId,
                note = entity.note,
                workFlowId = entity.workFlowId,
                insertUserId= entity.InsertUserId
            });
        }
       public IList<NotesModel> RetrieveTempNotes(Int64 merchantId)
        {
            return new DataAccess.DataAccess().ExecuteReader< NotesModel>("avz_notes_spRetrieveTempNotes", new { merchantId = merchantId });

        }
        public bool UpdateNotes(NotesModel objMod)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public bool Create(NotesModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(NotesModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
      
    }
}