using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Repositories
{
    internal interface IRepository<TEntity, TTableModel>
    {
        List<TTableModel> GetAllData();
        void Insert(TEntity entity);
        void Edit(TEntity updatedEntity);
        void Delete(int id);
    }
}
