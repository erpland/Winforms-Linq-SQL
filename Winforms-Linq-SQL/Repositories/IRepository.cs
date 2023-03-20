using System.Collections.Generic;

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
