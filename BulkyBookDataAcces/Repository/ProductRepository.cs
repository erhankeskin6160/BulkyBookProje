using BulkyBook.DataAcces.Data;
using BulkyBook.DataAcces.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAcces.Repository
{
    public class ProductRepository : Repository<Product> ,IProductRepository

    {
        public readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

         

        public void Update(Product product)
        {
            var ProductFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);

            if (ProductFromDb!=null)
            {
                ProductFromDb.Title = product.Title;
                ProductFromDb.Description = product.Description;
                ProductFromDb.ISBN = product.ISBN;
                ProductFromDb.Author = product.Author;
                ProductFromDb.ListPrice = product.ListPrice;
                ProductFromDb.Price = product.Price;
                ProductFromDb.Price50 = product.Price50;
                ProductFromDb.Price100 = product.Price100;
                ProductFromDb.ImageUrl = product.ImageUrl;
                ProductFromDb.CategoryId = product.CategoryId;
                ProductFromDb.CoverTypeId = product.CoverTypeId;

                if(product.ImageUrl != null)
                {
                    ProductFromDb.ImageUrl = product.ImageUrl;
                }
                
            }
          }
    }


}
