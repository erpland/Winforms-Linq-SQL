using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Configuration;

namespace WinformsLinqSQL.Repositories
{
    public class StoreDataContext : DataContext
    {
        public StoreDataContext(string connectionString) : base(connectionString)
        {
        }

        public Table<Customer> Customers;
        public Table<Order> Orders;
        public Table<OrderDetails> OrderDetails;
        public Table<Product> Products;

    }
}
