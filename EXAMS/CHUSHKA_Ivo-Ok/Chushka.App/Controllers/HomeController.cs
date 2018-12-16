using SIS.Framework.ActionResults;

namespace Chushka.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Services.Contracts;
    using ViewModels.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var productsViewModel = new List<ProductIndexViewModel>();
            var products = this.productsService.GetAllProducts();
            foreach (var product in products)
            {
                var description = string.Empty;
                if (product.Description.Length > 50)
                {
                    description = product.Description.Substring(0, 50) + "...";
                }
                else
                {
                    description = product.Description;
                }

                productsViewModel.Add(new ProductIndexViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = description,
                });
            }

            this.Model.Data["Products"] = productsViewModel;

            return this.View();
        }
    }
}