using WinformsLinqSQL.Models;
using WinformsLinqSQL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Controllers
{
    public class CustomersController
    {
        private static CustomersController instance;

        private CustomersController()
        {
        }
        public static CustomersController Instance()
        {
            if (instance == null)
            {
                instance = new CustomersController();
            }
            return instance;
        }
        public List<dynamic> GetAllCutomerData()
        {
                List<dynamic> data = CustomerHelper.GetAllData();
                return data;
        }
        public void InsertNewCustomer(Customer customer)
        {
            Validation.ValidateCustomer(customer);
            CustomerHelper.Insert(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            Validation.ValidateCustomer(customer);
            CustomerHelper.Edit(customer);
        }
        public void DeleteCustomer(int id)
        {
            CustomerHelper.Delete(id);
        }

        public List<dynamic> SearchCustomer(int id, string value)
        {
            List<dynamic> data = CustomerHelper.SearchByValue(id, value);
            return data;
        }
    }
}
