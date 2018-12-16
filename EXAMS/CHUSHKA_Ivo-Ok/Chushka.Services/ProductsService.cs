namespace Chushka.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enum;
    using ViewModels.BindingModels;

    public class ProductsService : BaseService, IProductsService
    {
        public ProductsService(ChushkaDbContext db)
            : base(db)
        {
        }

        public Product GetProductById(int productId)
        {
            return this.Db.Products.Find(productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.Db.Products;
        }

        public void CreateProduct(ProductBindingModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Type = (ProductType)Enum.Parse(typeof(ProductType), model.ProductType)
            };

            this.Db.Products.Add(product);
            this.Db.SaveChanges();
        }

        public void EditProduct(ProductBindingModel model, int productId)
        {
            var product = this.Db.Products.SingleOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;

                var ifParsed = Enum.TryParse(typeof(ProductType), model.ProductType, out object prodType);
                if (ifParsed)
                {
                    product.Type = (ProductType)prodType;
                }

                this.Db.SaveChanges();
            }
        }

        public void DeleteProduct(int productId)
        {
            var product = this.Db.Products.Find(productId);
            this.Db.Products.Remove(product);
            this.Db.SaveChanges();
        }
    }
}