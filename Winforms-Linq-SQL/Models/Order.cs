using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WinformsLinqSQL.Repositories
{
    [Table(Name = "orders")]
    public class Order
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true,Name = "order_id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer id is a requierd field!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Customer Id must contain only numbers!")]
        [Column(Name = "customer_id")]
        public int CustomerId { get; set; }

        [Column(Name ="order_date")]
        [Required(ErrorMessage = "Order date is a requierd field!")]
        public DateTime Date { get; set; }
        public Order()
        {
            
        }

        public Order(int id, int customerId, DateTime date)
        {
            Id = id;
            CustomerId = customerId;
            Date = date;
        }

        public Order(int customerId, DateTime date)
        {
            CustomerId = customerId;
            Date = date;
        }
    }
}
