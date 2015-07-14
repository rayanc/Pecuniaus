using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.Repository;
using Bridge.Models;
using System.Data;


namespace Bridge.BusinessTier
{
    public interface INotes : IRepository<NotesModel, int>
    {
        IList<NotesModel> Retrieve(Int64 id);
        bool Insert(NotesModel objMod);
        bool UpdateNotes(NotesModel objMod);
        bool DeleteNote(int id);
        DataSet RetrieveNotes(Int64 merchantId);
        bool InsertTempNotes(NotesModel entity);
        IList<NotesModel> RetrieveTempNotes(Int64 merchantId);
    }
}
