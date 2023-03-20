using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsLinqSQL.Models.TableModels;
using WinformsLinqSQL.Repositories;

namespace WinformsLinqSQL.Models
{
    public class OrdersController:IBaseControllers<Order,OrderTableModel>
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
        public (List<OrderTableModel>, bool) GetAll(out string errorMessage)
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
        public bool Insert(Order order, out string errorMessage)
        {
            try
            {
                Validation.ValidateFields(order);
                repository.Insert(order);
                errorMessage = string.Empty;
                return true;
            }
            catch (ValidationException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            catch (DataAccessException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Update(Order order, out string errorMessage)
        {
            try
            {
                Validation.ValidateFields(order);
                repository.Edit(order);
                errorMessage = string.Empty;
                return true;

            }
            catch (ValidationException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
            catch (DataAccessException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete(int id, out string errorMessage)
        {
            try
            {
                repository.Delete(id);
                errorMessage = string.Empty;
                return true;
            }
            catch (DataAccessException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
