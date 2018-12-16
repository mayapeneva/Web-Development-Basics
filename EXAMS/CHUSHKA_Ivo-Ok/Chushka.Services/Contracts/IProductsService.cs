namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;
    using DataModels;
    using ViewModels.BindingModels;

    public interface IProductsService
    {
        void CreateProduct(ProductBindingModel model);

        void EditProduct(ProductBindingModel model, int productId);

        Product GetProductById(int productId);

        IEnumerable<Product> GetAllProducts();

        void DeleteProduct(int productId);
    }
}