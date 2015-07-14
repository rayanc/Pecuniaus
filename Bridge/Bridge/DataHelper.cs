using Bridge.DataAccess;
using System;
using System.Data;
using System.Reflection;

namespace Bridge
{
    class DataHelper
    {
        public static void FillObjectFromDataRow(object objectToFill, DataRow dr)
        {
            PropertyInfo property;
            Type entityType = objectToFill.GetType();
            //object dri;
            foreach (DataColumn col in dr.Table.Columns)
            {
                //Try for anycase
                try
                {
                    property = entityType.GetProperty(col.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }
                catch (AmbiguousMatchException)
                {
                    //If it is an Ambiguous Match, that is we have both properties on this object then get the specific match
                    property = entityType.GetProperty(col.ColumnName);
                }
                if (property != null && dr[col] != DBNull.Value && property.CanWrite)
                {
                    property.SetValue(objectToFill, Convertor.ChangeType(dr[col], property.PropertyType), null);
                }
            }

        }
    }
}
