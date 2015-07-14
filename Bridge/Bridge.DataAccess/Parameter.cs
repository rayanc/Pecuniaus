using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Bridge.DataAccess
{
    public class Parameter
    {
        #region "Properties"

        private string _paramName;
        public string ParamName
        {
            get { return _paramName; }
            set { _paramName = value; }
        }

        private DbType _dbtype;
        public DbType DBType
        {
            get { return _dbtype; }
            set { _dbtype = value; }
        }

        private object _paramValue;
        public object ParamValue
        {
            get { return _paramValue; }
            set { _paramValue = value; }
        }
        #endregion

        #region "Functions"

        public static Parameter paramNew(string parName, DbType parType, object parVal)
        {

            Parameter parameter = new Parameter();
            parameter.ParamName = parName;
            parameter.DBType = parType;
            parameter.ParamValue = parVal;
            return parameter;
        }
        #endregion
    }
}
