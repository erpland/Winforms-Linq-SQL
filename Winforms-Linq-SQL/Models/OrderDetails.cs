using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Repositories
{
    [Table(Name = "order_details")]
    public class OrderDetails
    {
        [Column(IsPrimaryKey = true, Name = "product_id")]
        public int ProductId { get; set; }
        [Column(IsPrimaryKey = true, Name = "order_id")]
        public int OrderId { get; set; }
        [Column(IsPrimaryKey = true, Name = "prodcut_qty")]
        public int Qty { get; set; }
    }
}
