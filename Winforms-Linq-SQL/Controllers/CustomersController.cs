using WinformsLinqSQL.Repositories;
using WinformsLinqSQL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using WinformsLinqSQL.Models.TableModels;

namespace WinformsLinqSQL.Controllers
{
    public class CustomersController
    {
        private static CustomersController instance;
        private CustomerRepository repository;

        private CustomersController()
        {
            repository = CustomerRepository.Instance;
        }
        public static CustomersController Instance()
        {
            if (instance == null)
            {
                instance = new CustomersController();
            }
            return instance;
        }
        public (List<CustomerTableModel>, bool) GetAllCustomerData(out string errorMessage)
        {
            try
            {
                List<CustomerTableModel> data = repository.GetAllData();
                errorMessage = string.Empty;
                return (data, true);
            }
            catch (DataAccessException ex)
            {
                errorMessage = ex.Message;
                return (null, false);
            }
        }
        public bool InsertNewCustomer(Customer customer, out string errorMessage)
        {
            try
            {
                Validation.ValidateCustomer(customer);
                repository.Insert(customer);
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
        public bool UpdateCustomer(Customer customer, out string errorMessage)
        {
            try
            {
                Validation.ValidateCustomer(customer);
                repository.Edit(customer);
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
        public bool DeleteCustomer(int id, out string errorMessage)
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

        //not used search was filtered locally
        public (List<dynamic>, bool) SearchCustomer(int id, string value, out string errorMessage)
        {
            try
            {
                List<dynamic> data = repository.SearchByValue(id, value);
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
