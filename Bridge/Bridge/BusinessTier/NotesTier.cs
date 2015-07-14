using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;

namespace Bridge.BusinessTier
{
    public class NotesTier :INotes, IDisposable
    {

        #region Private Variables
        private INotes notesRepository;
        #endregion

        #region Contructors
        public NotesTier() : this(new NotesRepository()) { }

        public NotesTier(INotes notesRepository)
        {
            this.notesRepository = notesRepository;
        }
        #endregion

        #region "Functions"


            #endregion



        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public IList<NotesModel> Retrieve(Int64 id)
        {
            return notesRepository.Retrieve(id);
        }

        public bool Insert(NotesModel objMod)
        {
            return notesRepository.Insert(objMod);
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


        public DataSet RetrieveNotes(long merchantId)
        {
            return notesRepository.RetrieveNotes(merchantId);
        }
        public IList<NotesModel> RetrieveTempNotes(long merchantId)
        {
            return notesRepository.RetrieveTempNotes(merchantId);
        }


        public bool InsertTempNotes(NotesModel entity)
        {
            return notesRepository.InsertTempNotes(entity);
        }
    }
}