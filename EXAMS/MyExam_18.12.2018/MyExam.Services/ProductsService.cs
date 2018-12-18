namespace MyExam.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using BindingModels;
    using Common.DTOs;
    using Common.ViewModels;
    using Contracts;
    using Data;
    using DataModels;

    public class ProductsService : BaseService, IProductsService
    {
        public ProductsService(MyExamDbContext context)
            : base(context)
        {
        }

        public void CreateProduct(ProductBindingModel productModel)
        {
            var product = new Product
            {
                Name = productModel.Name,
                Picture = productModel.Picture,
                Price = productModel.Price,
                Barcode = productModel.Barcode
            };

            this.Db.Products.Add(product);
            this.Db.SaveChanges();
        }

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            var products = this.Db.Products;
            if (!products.Any())
            {
                return null;
            }

            return products.Select(p => new ProductViewModel
            {
                Name = p.Name,
                Price = p.Price.ToString("F2"),
                Barcode = p.Barcode,
                Picture = p.Picture == string.Empty ? p.Picture : "~Resources/product.ong"
            });
        }

        public ProductDto GetProductByBarcode(string barcode)
        {
            var product = this.Db.Products.SingleOrDefault(p => p.Barcode == barcode);
            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Barcode = product.Barcode
            };
        }
    }
}