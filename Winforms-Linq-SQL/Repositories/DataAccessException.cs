using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Repositories
{
    public class DataAccessException:Exception
    {
        public DataAccessException(string message) : base(message) { }
        public DataAccessException(string message, Exception innerException) : base(message, innerException) { }
        public DataAccessException(SqlException ex) : base(ex.Message, ex) { }
    }
}
