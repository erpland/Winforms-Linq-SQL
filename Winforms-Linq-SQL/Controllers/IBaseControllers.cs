using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Models
{
    public interface IBaseControllers<TEntity, TTableModel>
    {
        (List<TTableModel>,bool) GetAll(out string errorMessage);
        bool Insert(TEntity entity, out string errorMessage);
        bool Update(TEntity entity, out string errorMessage);
        bool Delete(int id, out string errorMessage);
    }
}
