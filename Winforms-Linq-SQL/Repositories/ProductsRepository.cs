using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;

namespace WinformsLinqSQL.Repositories
{
    public sealed class ProductsRepository : BaseRepository, IRepository<Product, Product>
    {
        private static readonly ProductsRepository instance = new ProductsRepository();
        private ProductsRepository() : base() { }

        public static ProductsRepository Instance
        {
            get { return instance; }
        }
        public List<Product> GetAllData()
        {
            using (db = new StoreDataContext())
            {
                try
                {
                    var products = db.Products;
                    return products.ToList();
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException($"Error communicating with the database\r\n {ex.Message}", ex);
                }
            }
        }
        public void Insert(Product product)
        {
            using (db = new StoreDataContext())
            {

                try
                {
                    db.Products.InsertOnSubmit(product);
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

        public void Edit(Product updatedProduct)
        {
            using (db = new StoreDataContext())
            {

                try
                {
                    var product = db.Products.SingleOrDefault(c => c.Id == updatedProduct.Id);
                    if (product == null)
                    {
                        throw new DataAccessException($"Product {updatedProduct.Id} does not exists");
                    }
                    product.Id = updatedProduct.Id;
                    product.Name = updatedProduct.Name;
                    product.Price = updatedProduct.Price;
                    product.InStock = updatedProduct.InStock;
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
                    var product = db.Products.SingleOrDefault(p => p.Id == id);
                    if (product == null)
                    {
                        throw new DataAccessException($"ID: {id} does not exists");
                    }
                    db.Products.DeleteOnSubmit(product);

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
