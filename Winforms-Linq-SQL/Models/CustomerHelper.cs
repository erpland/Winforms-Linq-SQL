using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Models
{
    public static class CustomerHelper
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnectionString"].ConnectionString;
        public static List<dynamic> GetAllData()
        {
            using (var db = new StoreDataContext(connectionString))
            {
                try
                {
                    var result = from c in db.Customers
                                 join o in db.Orders on c.Id equals o.CustomerId into orders
                                 from o in orders.DefaultIfEmpty()
                                 join od in db.OrderDetails on o.Id equals od.OrderId into orderDetails
                                 from od in orderDetails.DefaultIfEmpty()
                                 join p in db.Products on od.ProductId equals p.Id into products
                                 from p in products.DefaultIfEmpty()
                                 group new { c, od, p } by new { c.Id, c.FirstName, c.LastName, c.PhoneNumber, c.Address } into g
                                 select new
                                 {
                                     g.Key.Id,
                                     g.Key.FirstName,
                                     g.Key.LastName,
                                     g.Key.PhoneNumber,
                                     g.Key.Address,
                                     Total_Quantity = g.Sum(x => x.od != null ? x.od.Qty : 0),
                                     Total_Orders = g.Count(x => x.od.OrderId != null),
                                     Total_Price = g.Sum(x => x.p != null ? x.p.Price : 0)
                                 };
                    return result.ToList<dynamic>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to get data from database\r\n" + ex.Message);
                }
            }
        }

        public static void Insert(Customer customer)
        {
            using (var db = new StoreDataContext(connectionString))
            {
                try
                {
                    db.Customers.InsertOnSubmit(customer);
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to insert new customer\r\n" + ex.Message);
                }
            }
        }
        public static void Edit(Customer updatedCustomer)
        {
            using (var db = new StoreDataContext(connectionString))
            {
                try
                {
                    var customer = db.Customers.SingleOrDefault(c => c.Id == updatedCustomer.Id);
                    if (customer == null)
                    {
                        throw new Exception($"Customer {updatedCustomer.Id} does not exists");
                    }
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    customer.Address = updatedCustomer.Address;
                    customer.PhoneNumber = updatedCustomer.PhoneNumber;
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to save changes to customer -{updatedCustomer.FirstName} {updatedCustomer.LastName} \r\n {ex.Message}");
                }
            }
        }
        public static void Delete(int id)
        {
            using (var db = new StoreDataContext(connectionString))
            {
                try
                {
                    var customer = db.Customers.SingleOrDefault(c => c.Id == id);
                    if (customer == null)
                    {
                        throw new Exception($"ID: {id} does not exists");
                    }
                    db.Customers.DeleteOnSubmit(customer);

                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to delete customer with id - {id} \r\n {ex.Message}");
                }
            }
        }

        public static List<dynamic> SearchByValue(int id, string value)
        {
            using (var db = new StoreDataContext(connectionString))
            {
                try
                {
                    var result = from c in db.Customers
                                 where c.FirstName.StartsWith(value) || c.LastName.StartsWith(value) || c.PhoneNumber.Equals(value)|| c.Id == id
                                 join o in db.Orders on c.Id equals o.CustomerId into orders
                                 from o in orders.DefaultIfEmpty()
                                 join od in db.OrderDetails on o.Id equals od.OrderId into orderDetails
                                 from od in orderDetails.DefaultIfEmpty()
                                 join p in db.Products on od.ProductId equals p.Id into products
                                 from p in products.DefaultIfEmpty()
                                 group new { c, od, p } by new { c.Id, c.FirstName, c.LastName, c.PhoneNumber, c.Address } into g
                                 select new
                                 {
                                     g.Key.Id,
                                     g.Key.FirstName,
                                     g.Key.LastName,
                                     g.Key.PhoneNumber,
                                     g.Key.Address,
                                     Total_Quantity = g.Sum(x => x.od != null ? x.od.Qty : 0),
                                     Total_Orders = g.Count(x => x.od.OrderId != null),
                                     Total_Price = g.Sum(x => x.p != null ? x.p.Price : 0)
                                 };
                    return result.ToList<dynamic>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to get data from database\r\n" + ex.Message);
                }
            }
        }
    }
}

