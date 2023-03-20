using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Repositories
{
    [Table(Name = "products")]
    public class Product
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "product_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Product name is a requierd field!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Product name must be between 2-50 charcters")]
        [Column(Name = "name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is a requierd field!")]
        [RegularExpression(@"^(?:\d*\.\d+|\d+\.\d*|\d+)$", ErrorMessage = "Price must contain only numbers!")]
        [Column(Name = "price")]
        public double Price { get; set; }


        [Required(ErrorMessage = "In stock is a requierd field!")]
        [Column(Name = "in_stock")]
        public bool InStock { get; set; }

        public Product()
        {
            
        }

        public Product(string name, double price, bool inStock)
        {
            Name = name;
            Price = price;
            InStock = inStock;
        }

        public Product(int id, string name, double price, bool inStock)
        {
            Id = id;
            Name = name;
            Price = price;
            InStock = inStock;
        }
    }
}
