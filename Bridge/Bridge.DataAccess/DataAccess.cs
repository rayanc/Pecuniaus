using System.Data;
using System.Collections.Generic;
using System;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace Bridge.DataAccess
{
    public class DataAccess
    {
        private readonly string DATABASE_APP_KEY;
        private Database appDatabase;
        /// <summary>
        /// Data Access constructor
        /// </summary>
        public DataAccess() : this("ConnectionStringName") { }

        /// <summary>
        /// Data Access constructor
        /// </summary>
        /// <param name="DatabaseKey"></param>
        public DataAccess(string DatabaseAppKey)
        {
            DATABASE_APP_KEY = DatabaseAppKey;
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            appDatabase = factory.Create("AVZ");
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public IList<T> ExecuteReader<T>(string spName, dynamic spInParams)
        {
            IList<T> result = new List<T>();
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                using (IDataReader dataReader = dataAccess.ApplyStoreProcCommandDataReader(spName, spInParams))
                {
                    while (dataReader.Read())
                    {
                        result.Add(dataAccess.GetValues<T>(dataReader));
                    }
                }
            }

            return result;
        }

        ///
        public Tuple<IList<T>, IList<T2>> ExecuteReader<T, T2>(string spName, dynamic spInParams)
        {
            IList<T> result1 = new List<T>();
            IList<T2> result2 = new List<T2>();
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                using (IDataReader dataReader = dataAccess.ApplyStoreProcCommandDataReader(spName, spInParams))
                {
                    while (dataReader.Read())
                    {
                        result1.Add(dataAccess.GetValues<T>(dataReader));
                    }
                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            result2.Add(dataAccess.GetValues<T2>(dataReader));
                        }
                    }
                }
            }
            return new Tuple<IList<T>, IList<T2>>(result1, result2);
        }

        /// <summary>
        /// Execute Reader Multiple with Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public Dictionary<string, string> ExecuteReaderMultiple(string spName, dynamic spInParams)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                using (IDataReader dataReader = dataAccess.ApplyStoreProcCommandDataReader(spName, spInParams))
                {
                    while (dataReader.Read())
                    {
                        for (int iCount = 0; iCount < dataReader.FieldCount; iCount++)
                        {
                            result.Add(Convert.ToString(dataReader.GetName(iCount)), Convert.ToString(dataReader.GetValue(iCount)));
                        }
                    }
                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            for (int iCount = 0; iCount < dataReader.FieldCount; iCount++)
                            {
                                result.Add(Convert.ToString(dataReader.GetName(iCount)), Convert.ToString(dataReader.GetValue(iCount)));
                            }
                        }
                        if (dataReader.NextResult())
                        {
                            while (dataReader.Read())
                            {
                                for (int iCount = 0; iCount < dataReader.FieldCount; iCount++)
                                {
                                    result.Add(Convert.ToString(dataReader.GetName(iCount)), Convert.ToString(dataReader.GetValue(iCount)));
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Execute Reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <returns></returns>
        public IList<T> ExecuteReader<T>(string spName)
        {
            IList<T> result = new List<T>();
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                using (IDataReader dataReader = dataAccess.ApplyStoreProcCommandDataReader(spName))
                {
                    while (dataReader.Read())
                    {
                        result.Add(dataAccess.GetValues<T>(dataReader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string spName, dynamic spInParams)
        {
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                dataAccess.ApplyStoreProcCommandNonQuery(spName, spInParams);
                return true;
            }
        }
        public Int64 ExecuteNonQuery(string spName, dynamic spInParams, dynamic spOutParams)
        {
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                return dataAccess.ApplyStoreProcCommandNonQuery(spName, spInParams, spOutParams);

            }
        }

        /// <summary>
        /// Execute DataSet
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string spName, dynamic spInParams)
        {
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                return dataAccess.ApplyStoreProcCommandDataSet(spName, spInParams);
            }
        }

        /// <summary>
        /// Execute Scalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string spName, dynamic spInParams)
        {
            using (DataAccessBock dataAccess = new DataAccessBock(DATABASE_APP_KEY))
            {
                dataAccess.AddInParameters(spInParams);
                return (T)dataAccess.ApplyStoreProcCommandScalar(spName, spInParams);
            }
        }

        /// <summary>
        /// Flag for disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // called via myClass.Dispose(). 
                    // OK to use any private object references
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }







    }
}
