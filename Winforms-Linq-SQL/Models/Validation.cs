using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WinformsLinqSQL.Repositories;

namespace WinformsLinqSQL.Models
{
    public static class Validation
    {
        public static void ValidateFields<T>(T data)
        {
            string errorMsg = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext ctx = new ValidationContext(data);
            bool isValid = Validator.TryValidateObject(data, ctx, results, true);
            if (!isValid)
            {
                foreach (ValidationResult result in results)
                {
                    errorMsg += result.ErrorMessage + "\n";
                }
                throw new ValidationException(errorMsg);
            }
        }
    }
}
