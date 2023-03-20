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
    public class ProductsController : IBaseControllers<Product, Product>
    {
        private static readonly ProductsController instance = new ProductsController();
        private ProductsRepository repository;
        private ProductsController()
        {
            repository = ProductsRepository.Instance;
        }
        public static ProductsController Instance
        {
            get { return instance; }
        }
        public (List<Product>, bool) GetAll(out string errorMessage)
        {
            try
            {
                List<Product> data = repository.GetAllData();
                errorMessage = string.Empty;
                return (data, true);
            }
            catch (DataAccessException ex)
            {
                errorMessage = ex.Message;
                return (null, false);
            }
        }
        public bool Insert(Product product, out string errorMessage)
        {
            try
            {
                Validation.ValidateFields(product);
                repository.Insert(product);
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
        public bool Update(Product product, out string errorMessage)
        {
            try
            {
                Validation.ValidateFields(product);
                repository.Edit(product);
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
