using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Models
{
    [Table(Name = "orders")]
    public class Order
    {
        [Column(IsPrimaryKey = true,Name = "order_id")]
        public int Id { get; set; }
        [Column(Name = "cutomer_id")]
        public int CustomerId { get; set; }
        [Column(Name ="order_date")]
        public DateTime Date { get; set; }

    }
}
