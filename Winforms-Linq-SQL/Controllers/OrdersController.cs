using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsLinqSQL.Models.TableModels;
using WinformsLinqSQL.Repositories;

namespace WinformsLinqSQL.Controllers
{
    public class OrdersController
    {
        private static OrdersController instance;
        private OrderRepository repository;

        private OrdersController()
        {
            repository = OrderRepository.Instance;
        }
        public static OrdersController Instance()
        {
            if (instance == null)
            {
                instance = new OrdersController();
            }
            return instance;
        }
        public (List<OrderTableModel>, bool) GetAllOrdersData(out string errorMessage)
        {
            try
            {
                List<OrderTableModel> data = repository.GetAllData();
                errorMessage = string.Empty;
                return (data, true);
            }
            catch (DataAccessException ex)
            {
                errorMessage = ex.Message;
                return (null, false);
            }
        }
    }
}
