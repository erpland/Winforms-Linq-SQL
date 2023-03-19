using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                                 join od in db.OrderDetails on o.Id equals od.OrderId
                                 join p in db.Products on od.ProductId equals p.Id
                                 group new { c, od, p } by new { o.Id, o.Date,o.CustomerId, CustomerName = c.LastName + " " + c.FirstName, c.PhoneNumber, c.Address } into g
                                 select new OrderTableModel
                                 {
                                     OrderId = g.Key.Id,
                                     OrderDate = g.Key.Date,
                                     CustomerId = g.Key.CustomerId,
                                     CustomerName = g.Key.CustomerName,
                                     PhoneNumber = g.Key.PhoneNumber,
                                     Address = g.Key.Address,
                                     TotalQuantity = g.Sum(x => x.od.Qty),
                                     TotalPrice = g.Sum(x => x.od.Qty * x.p.Price)

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
            throw new NotImplementedException();
        }

        public void Edit(Order updatedOrder)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }



    }
}
