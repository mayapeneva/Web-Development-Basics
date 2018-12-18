namespace MyExam.Services.Contracts
{
    using System.Collections.Generic;
    using BindingModels;
    using Common.DTOs;
    using Common.ViewModels;

    public interface IProductsService
    {
        void CreateProduct(ProductBindingModel productModel);

        IEnumerable<ProductViewModel> GetAllProducts();

        ProductDto GetProductByBarcode(string barcode);
    }
}