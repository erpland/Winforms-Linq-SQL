using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Repositories
{
    public abstract class BaseRepository
    {
        protected StoreDataContext db;
        protected BaseRepository()
        {

        }
    }
}
