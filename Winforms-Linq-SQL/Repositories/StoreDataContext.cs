using System.Configuration;
using System.Data.Linq;

namespace WinformsLinqSQL.Repositories
{
    public class StoreDataContext : DataContext
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnectionString"].ConnectionString;
        public StoreDataContext() : base(connectionString)
        {
        }
        public StoreDataContext(string connectionString) : base(connectionString)
        {
        }

        public Table<Customer> Customers;
        public Table<Order> Orders;
        public Table<OrderDetails> OrderDetails;
        public Table<Product> Products;

    }
}
