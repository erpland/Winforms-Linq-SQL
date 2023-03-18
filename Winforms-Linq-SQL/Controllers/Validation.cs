using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WinformsLinqSQL.Models;

namespace WinformsLinqSQL.Controllers
{
    public static class Validation
    {
        public static void ValidateCustomer(Customer customer)
        {
            string errorMsg = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext ctx = new ValidationContext(customer);
            bool isValid = Validator.TryValidateObject(customer, ctx, results, true);
            if (!isValid)
            {
                foreach (ValidationResult result in results)
                {
                    errorMsg += result.ErrorMessage + "\n";
                }
                throw new Exception(errorMsg);
            }
        }
    }
}
