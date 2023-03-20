using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using WinformsLinqSQL.Models.TableModels;

namespace WinformsLinqSQL.Repositories
{
    public sealed class OrderRepository : BaseRepository, IRepository<Order, OrderTableModel>
    {
        private static readonly OrderRepository instance = new OrderRepository();
        private OrderRepository() : base() { }

        public static OrderRepository Instance
        {
            get { return instance; }
        }

        public List<OrderTableModel> GetAllData()
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    var result = from c in db.Customers
                                 join o in db.Orders on c.Id equals o.CustomerId
                                 join od in db.OrderDetails on o.Id equals od.OrderId into odGroup
                                 from odLeft in odGroup.DefaultIfEmpty()
                                 join p in db.Products on (odLeft == null ? (int?)null : odLeft.ProductId) equals p.Id into pGroup
                                 from pLeft in pGroup.DefaultIfEmpty()
                                 group new { c, od = odLeft, p = pLeft } by new { o.Id, o.Date, o.CustomerId, CustomerName = c.LastName + " " + c.FirstName, c.PhoneNumber, c.Address } into g
                                 select new OrderTableModel
                                 {
                                     OrderId = g.Key.Id,
                                     OrderDate = g.Key.Date,
                                     CustomerId = g.Key.CustomerId,
                                     CustomerName = g.Key.CustomerName,
                                     PhoneNumber = g.Key.PhoneNumber,
                                     Address = g.Key.Address,
                                     TotalQuantity = g.Sum(x => x.od == null ? 0 : x.od.Qty),
                                     TotalPrice = g.Sum(x => (x.od == null ? 0 : x.od.Qty) * (x.p == null ? 0 : x.p.Price))
                                 };
                    return result.ToList<OrderTableModel>();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n {ex.Message}", ex);
                }
            }
        }

        public void Insert(Order order)
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    db.Orders.InsertOnSubmit(order);
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

        public void Edit(Order updatedOrder)
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    var order = db.Orders.SingleOrDefault(c => c.Id == updatedOrder.Id);
                    if (order == null)
                    {
                        throw new DataAccessException($"Order {updatedOrder.Id} does not exists");
                    }
                    order.CustomerId = updatedOrder.CustomerId;
                    order.Date = updatedOrder.Date;
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
                    var order = db.Orders.SingleOrDefault(o => o.Id == id);
                    if (order == null)
                    {
                        throw new DataAccessException($"ID: {id} does not exists");
                    }
                    db.Orders.DeleteOnSubmit(order);

                    db.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n{ex.Message}", ex);
                }
                catch (ChangeConflictException ex)
                {
                    throw new ChangeConflictException($"Conflict in deleting order with id {id} \r\n {ex.Message}", ex);
                }
            }
        }



    }
}
