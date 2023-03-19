using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Repositories
{
    [Table(Name = "products")]
    public class Product
    {
        [Column(IsPrimaryKey = true, Name = "product_id")]
        public int Id { get; set; }
        [Column(Name = "name")]
        public string Name { get; set; }
        [Column(Name = "price")]
        public double Price { get; set; }
        [Column(Name = "inStock")]
        public bool InStock { get; set; }

    }
}
