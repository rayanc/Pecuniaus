using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections;
using MySql.Data.MySqlClient;

namespace Bridge.DataAccess
{
    /// <summary>
    /// Data Access App Block
    /// </summary>
    public class DataAccessBock : IDisposable
    {

        #region "Variables"

        private DatabaseContextClassic _appDataBase;

        private int _CommandTimeOut = 30;
        public int CommandTimeOut
        {
            get
            {
                return _CommandTimeOut;
            }
            set
            {
                _CommandTimeOut = value;
            }
        }

        #endregion

        #region "Constructors / Destructors"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDataBase"></param>
        public DataAccessBock(string pDataBase)
        {
            Init(pDataBase);
        }

        #endregion

        #region "Other Functions / Procedures"

        /// <summary>
        /// Database Context Classic
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public DatabaseContextClassic DatabaseContextFactory(string name)
        {
            string conectionName = System.Configuration.ConfigurationManager.AppSettings[name];
            if ((conectionName == null))
            {
                throw new ApplicationException("Unable to initialize database for the following Application Key : " + name);
            }

            return new DatabaseContextClassic(System.Configuration.ConfigurationManager.ConnectionStrings[conectionName].ConnectionString);
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="pConfigurationKey"></param>
        public void Init(string configurationKey)
        {
            _appDataBase = DatabaseContextFactory(configurationKey);
            _appDataBase.CreateConnection();
            _appDataBase.OpenConnection();
        }

        /// <summary>
        /// Create Command
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        private DbCommand CreateCommand(string spName)
        {
            return _appDataBase.GetStoredProcCommand(spName);
        }

        /// <summary>
        /// Apply StoreProc Command Data Reader
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public IDataReader ApplyStoreProcCommandDataReader(string spName, dynamic spInParams)
        {
            using (DbCommand commandWrapper = _appDataBase.GetStoredProcCommand(spName))
            {
                commandWrapper.CommandTimeout = CommandTimeOut;
                commandWrapper.Parameters.AddRange(AddInParameters(spInParams).ToArray());
                return commandWrapper.ExecuteReader();
            }
        }

        /// <summary>
        /// Apply StoreProc Command Data Reader
        /// </summary>
        /// <param name="spName"></param>           
        /// <returns></returns>
        public IDataReader ApplyStoreProcCommandDataReader(string spName)
        {
            using (DbCommand commandWrapper = _appDataBase.GetStoredProcCommand(spName))
            {
                commandWrapper.CommandTimeout = CommandTimeOut;
                return commandWrapper.ExecuteReader();
            }
        }

        /// <summary>
        /// Apply StoreProc Command Data Reader
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public IDataReader ApplyStoreProcCommandDataReader(string spName, dynamic spInParams, int pageSize, int pageNumber)
        {
            using (DbCommand commandWrapper = _appDataBase.GetStoredProcCommand(spName))
            {
                commandWrapper.CommandTimeout = CommandTimeOut;
                commandWrapper.Parameters.AddRange(AddInParameters(spInParams).ToArray());

                dynamic spInpaging = new { pageSize = pageSize, pageNumber = pageNumber };
                commandWrapper.Parameters.AddRange(AddInParameters(spInpaging).ToArray());
                return commandWrapper.ExecuteReader();
            }
        }

        /// <summary>
        /// Apply Store Proc Command Non Query
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public int ApplyStoreProcCommandNonQuery(string spName, dynamic spInParams)
        {
            using (DbCommand commandWrapper = _appDataBase.GetStoredProcCommand(spName))
            {
                commandWrapper.CommandTimeout = CommandTimeOut;
                commandWrapper.Parameters.AddRange(AddInParameters(spInParams).ToArray());
                return commandWrapper.ExecuteNonQuery();
            }

        }
        public Int64 ApplyStoreProcCommandNonQuery(string spName, dynamic spInParams, dynamic spOutParams)
        {
            Int64 returnParamter = 0;
            using (DbCommand commandWrapper = _appDataBase.GetStoredProcCommand(spName))
            {
                commandWrapper.CommandTimeout = CommandTimeOut;
                commandWrapper.Parameters.AddRange(AddInParameters(spInParams).ToArray());
                commandWrapper.Parameters.AddRange(AddOutParameters(spOutParams).ToArray());
                commandWrapper.ExecuteNonQuery();
                try
                {
                    returnParamter = Convert.ToInt64(commandWrapper.Parameters["merchantId"].Value.ToString());
                }
                catch (Exception)
                {

                    returnParamter = 0;
                }


                return returnParamter;
            }

        }

        /// <summary>
        /// Exceute a Query which return an Object as result
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public object ApplyStoreProcCommandScalar(string spName, dynamic spInParams)
        {
            using (DbCommand commandWrapper = _appDataBase.GetStoredProcCommand(spName))
            {
                commandWrapper.CommandTimeout = CommandTimeOut;
                commandWrapper.Parameters.AddRange(AddInParameters(spInParams).ToArray());
                return commandWrapper.ExecuteScalar();
            }

        }

        /// <summary>
        /// Exceute a Query which return a DataSet as result
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spInParams"></param>
        /// <returns></returns>
        public DataSet ApplyStoreProcCommandDataSet(string spName, dynamic spInParams)
        {
            DataSet dsReturn;
            DbParameter[] strParam = AddInParameters(spInParams).ToArray();
            DbDataAdapter dAdapter = _appDataBase.GetDBAdapter(spName, strParam, CommandTimeOut);
            dsReturn = new DataSet();
            dAdapter.Fill(dsReturn);
            return dsReturn;
        }

        private string ConvertToString(byte[] source)
        {
            string hex = BitConverter.ToString(source);
            return hex.Replace("-", "");
        }

        private byte[] ToByteArray(string source)
        {
            int length = source.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(source.Substring(i, 2), 16);
            }

            return bytes;
        }

        #endregion

        #region "Add In Parameters"

        /// <summary>
        /// AddInParameters
        /// </summary>
        /// <param name="spInParam"></param>
        public IList<DbParameter> AddInParameters(dynamic spInParam)
        {
            IList<DbParameter> result = new List<DbParameter>();
            if (spInParam != null)
            {
                DbType temp = DbType.String;
                object value;
                foreach (PropertyInfo prop in spInParam.GetType().GetProperties())
                {
                    value = prop.GetValue(spInParam, null);
                    if (value == null)
                        continue;

                    temp = DbType.String;
                    switch (value.GetType().ToString())
                    {
                        case "System.Byte":
                            temp = DbType.Byte;
                            break;
                        case "System.Int16":
                            temp = DbType.Int16;
                            break;
                        case "System.Int32":
                            temp = DbType.Int32;
                            break;
                        case "System.Int64":
                            temp = DbType.Int64;
                            break;
                        case "System.Decimal":
                            temp = DbType.Decimal;
                            break;
                        case "System.Double":
                            temp = DbType.Double;
                            break;
                        case "System.Single":
                            temp = DbType.Single;
                            break;
                        case "System.Guid":
                            temp = DbType.Guid;
                            break;
                        case "System.Boolean":
                            temp = DbType.Boolean;
                            break;
                        case "System.DateTime":
                            temp = DbType.DateTime;
                            break;
                        case "System.TimeSpan":
                            temp = DbType.Time;
                            break;
                        case "System.String":
                            temp = DbType.String;
                            break;
                        case "System.Byte[]":
                            temp = DbType.Binary;
                            break;

                        default:
                            throw new NotImplementedException("The type: " + value.GetType().ToString() + " has not been implemented");
                    }

                    result.Add(_appDataBase.AddInParameter(prop.Name, temp, ref value));
                }
            }
            return result;
        }



        /// <summary>
        /// AddInParameters
        /// </summary>
        /// <param name="spInParam"></param>
        public IList<MySqlParameter> AddOutParameters(dynamic spInParam)
        {
            IList<MySqlParameter> result = new List<MySqlParameter>();
            if (spInParam != null)
            {
                DbType temp = DbType.String;
                object value;
                int size = 0;
                foreach (PropertyInfo prop in spInParam.GetType().GetProperties())
                {
                    value = prop.GetValue(spInParam, null);
                    if (value == null)
                        continue;
                    temp = DbType.String;
                    switch (value.GetType().ToString())
                    {
                        case "System.Boolean":
                            temp = DbType.Boolean;
                            break;
                        case "System.DateTime":
                            temp = DbType.DateTime;
                            break;
                        case "System.String":
                            temp = DbType.String;
                            break;
                        case "System.Int32":
                            temp = DbType.Int32;
                            break;
                        case "System.Int64":
                            temp = DbType.Int64;
                            break;
                        case "System.Double":
                            temp = DbType.Double;
                            break;
                        case "System.Decimal":
                            temp = DbType.Decimal;
                            break;
                        default:
                            throw new NotImplementedException("The type: " + value.GetType().ToString() + " has not been implemented");
                    }
                    size = size = Marshal.SizeOf(value.GetType());
                    result.Add(_appDataBase.AddOutParameter(prop.Name, temp, size));
                }
            }
            return result;
        }


        #endregion


        #region "Get Reader Values"

        /// <summary>
        /// Get Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T GetValue<T>(object value)
        {
            return (T)(Convert.IsDBNull(value) ? null : value);
        }


        /// <summary>
        /// Get Values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public T GetValues<T>(IDataReader dataReader)
        {
            T result = Activator.CreateInstance<T>();
            PropertyInfo property = null;
            Type entityType = result.GetType();
            //object dri;
            for (int i = 0, j = dataReader.FieldCount; i < j; i++)
            {
                //Try for anycase
                try
                {
                    property = entityType.GetProperty(dataReader.GetName(i), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }
                catch (AmbiguousMatchException)
                {
                    //If it is an Ambiguous Match, that is we have both properties on this object then get the specific match
                    property = entityType.GetProperty(dataReader.GetName(i));
                }

                if (property != null && dataReader[i] != DBNull.Value && property.CanWrite)
                {
                    property.SetValue(result, Convertor.ChangeType(dataReader[i], property.PropertyType), null);

                }

                //if (property != null)
                //{
                //    dri = dataReader[i];
                //    switch (property.PropertyType.FullName)
                //    {
                //        case "System.Int64":
                //            property.SetValue(result, Convert.IsDBNull(dri) ? 0 : Convert.ToInt64(dri), null);
                //            break;
                //        case "System.Int32":
                //            property.SetValue(result, Convert.IsDBNull(dri) ? 0 : Convert.ToInt32(dri), null);
                //            break;
                //        case "System.Boolean":
                //            property.SetValue(result, Convert.IsDBNull(dri) ? false : Convert.ToBoolean(dri), null);
                //            break;
                //        case "System.DateTime":
                //            DateTime datetime;
                //            DateTime.TryParse(dri.ToString(), out datetime);

                //            property.SetValue(result,datetime);
                //            break;
                //        default:
                //            property.SetValue(result, Convert.IsDBNull(dri) ? null : dri, null);
                //            break;
                //    }
                //}
            }
            return result;
        }
        #endregion

        #region "IDisposable Support"

        // To detect redundant calls
        private bool _disposedValue = false;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                }
                _appDataBase.Dispose();
                // TODO: free your own state (unmanaged objects).
                // TODO: set large fields to null.
                _appDataBase = null;

            }
            this._disposedValue = true;
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }


    /// <summary>
    /// Database Context Classic
    /// </summary>
    public class DatabaseContextClassic : IDisposable
    {

        private IDbConnection _dbConnection;
        public string ConnectionString { get; set; }


        /// <summary>
        /// Database Context Classic
        /// </summary>
        /// <param name="pConnString"></param>
        public DatabaseContextClassic(string connString)
        {
            ConnectionString = connString;
        }

        /// <summary>
        /// Add In Parameter
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pDbType"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public MySqlParameter AddInParameter(string name, System.Data.DbType dbType, ref object value)
        {
            return new MySqlParameter
            {
                ParameterName = "iN" + name,
                Value = value ?? DBNull.Value,
                DbType = dbType,
                Direction = ParameterDirection.Input
            };
        }

        /// <summary>
        /// Add Out Parameter
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pDbType"></param>
        /// <param name="pSize"></param>
        /// <returns></returns>
        public MySqlParameter AddOutParameter(string pName, System.Data.DbType pDbType, int pSize)
        {
            return new MySqlParameter
            {
                ParameterName = pName,
                DbType = pDbType,
                Size = pSize,
                Direction = ParameterDirection.Output
            };
        }

        /// <summary>
        /// CreateConnection
        /// </summary>
        public void CreateConnection()
        {
            //_dbConnection = new SqlConnection(ConnectionString);
            _dbConnection = new MySqlConnection(ConnectionString);

        }

        /// <summary>
        /// CloseConnection
        /// </summary>
        public void CloseConnection()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Get Stored Proc Command
        /// </summary>
        /// <param name="pStoredProcedureName"></param>
        /// <returns></returns>
        public DbCommand GetStoredProcCommand(string pStoredProcedureName)
        {
            return new MySqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = pStoredProcedureName,
                Connection = _dbConnection as MySqlConnection
            };
        }

        /// <summary>
        /// Get DB Adapter
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="spInParams"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public DbDataAdapter GetDBAdapter(string procedureName, DbParameter[] spInParams, int commandTimeout)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(spInParams);
            command.Connection = _dbConnection as MySqlConnection;
            command.CommandTimeout = commandTimeout;
            return new MySqlDataAdapter()
            {
                SelectCommand = command
            };
        }

        /// <summary>
        /// OpenConnection
        /// </summary>
        public void OpenConnection()
        {
            _dbConnection.Open();
        }

        /// <summary>
        /// Get Open Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetOpenConnection()
        {
            return _dbConnection;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_dbConnection != null)
            { _dbConnection.Dispose(); }
        }
    }
}


