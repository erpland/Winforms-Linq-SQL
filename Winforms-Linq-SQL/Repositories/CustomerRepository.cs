using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsLinqSQL.Models.TableModels;

namespace WinformsLinqSQL.Repositories
{
    public sealed class CustomerRepository : BaseRepository, IRepository<Customer,CustomerTableModel>
    {
        private static readonly CustomerRepository instance = instance = new CustomerRepository();
        private CustomerRepository() : base() { }
        public static CustomerRepository Instance
        {
            get { return instance; }
        }
        public List<CustomerTableModel> GetAllData()
        {
            using (db = new StoreDataContext())
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
                                 select new CustomerTableModel
                                 {
                                     Id = g.Key.Id,
                                     FirstName = g.Key.FirstName,
                                     LastName = g.Key.LastName,
                                     PhoneNumber = g.Key.PhoneNumber,
                                     Address = g.Key.Address,
                                     TotalQuantity = g.Sum(x => x.od != null ? x.od.Qty : 0),
                                     TotalOrders = g.Count(x => x.od.OrderId != null),
                                     TotalPrice = g.Sum(x => x.p != null && x.od != null ? x.p.Price * x.od.Qty : 0)
                                 };
                    return result.ToList<CustomerTableModel>();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n {ex.Message}", ex);
                }
            }
        }

        public void Insert(Customer customer)
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    db.Customers.InsertOnSubmit(customer);
                    db.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n{ex.Message}", ex);
                }
                catch (ChangeConflictException ex)
                {
                    throw new ChangeConflictException($"Conflict in inserting data\r\n {ex.Message}", ex);
                }
            }
        }
        public void Edit(Customer updatedCustomer)
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    var customer = db.Customers.SingleOrDefault(c => c.Id == updatedCustomer.Id);
                    if (customer == null)
                    {
                        throw new DataAccessException($"Customer {updatedCustomer.Id} does not exists");
                    }
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    customer.Address = updatedCustomer.Address;
                    customer.PhoneNumber = updatedCustomer.PhoneNumber;
                    db.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n{ex.Message}", ex);
                }
                catch (ChangeConflictException ex)
                {
                    throw new ChangeConflictException($"Conflict in editing data\r\n {ex.Message}", ex);
                }
            }

        }
        public void Delete(int id)
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    var customer = db.Customers.SingleOrDefault(c => c.Id == id);
                    if (customer == null)
                    {
                        throw new DataAccessException($"ID: {id} does not exists");
                    }
                    db.Customers.DeleteOnSubmit(customer);

                    db.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n{ex.Message}", ex);
                }
                catch (ChangeConflictException ex)
                {
                    throw new ChangeConflictException($"Conflict in deleting customer with id {id} \r\n {ex.Message}", ex);
                }
            }
        }


        //not used search was filtered locally
        public List<dynamic> SearchByValue(int id, string value)
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    var result = from c in db.Customers
                                 where c.FirstName.StartsWith(value) || c.LastName.StartsWith(value) || c.Address.StartsWith(value) || c.PhoneNumber.Equals(value) || c.Id == id
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
                                     Total_Price = g.Sum(x => x.p != null && x.od != null ? x.p.Price * x.od.Qty : 0)
                                 };
                    return result.ToList<dynamic>();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n{ex.Message}", ex);
                }
            }
        }
    }
}

