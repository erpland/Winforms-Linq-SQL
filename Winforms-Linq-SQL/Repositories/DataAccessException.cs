using System;
using System.Data.SqlClient;

namespace WinformsLinqSQL.Repositories
{
    public class DataAccessException:Exception
    {
        public DataAccessException(string message) : base(message) { }
        public DataAccessException(string message, Exception innerException) : base(message, innerException) { }
        public DataAccessException(SqlException ex) : base(ex.Message, ex) { }
    }
}
