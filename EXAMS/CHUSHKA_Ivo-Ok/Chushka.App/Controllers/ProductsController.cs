namespace Chushka.App.Controllers
{
    using Services.Contracts;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    using ViewModels.BindingModels;
    using ViewModels.ViewModels;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [Authorize]
        public IActionResult Details()
        {
            var productId = int.Parse(this.Request.QueryData["productId"].ToString());

            var product = this.productsService.GetProductById(productId);
            this.Model.Data["Product"] = new ProductViewModel
            {
                Id = productId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Type = product.Type.ToString()
            };

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(ProductBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View();
            }

            this.productsService.CreateProduct(model);

            return this.RedirectToAction("/");
        }

        [Authorize("Admin")]
        public IActionResult Edit()
        {
            var productId = int.Parse(this.Request.QueryData["productId"].ToString());
            var product = this.productsService.GetProductById(productId);

            this.Model.Data["Id"] = product.Id;
            this.Model.Data["Name"] = product.Name;
            this.Model.Data["Price"] = product.Price;
            this.Model.Data["Description"] = product.Description;
            this.Model.Data["Type"] = product.Type;

            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Edit(ProductBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View();
            }

            this.productsService.EditProduct(model, model.Id);

            return this.RedirectToAction($"/Products/Details?Id={model.Id}");
        }

        [Authorize("Admin")]
        public IActionResult Delete()
        {
            var productId = int.Parse(this.Request.QueryData["productId"].ToString());
            var product = this.productsService.GetProductById(productId);

            this.Model.Data["Id"] = product.Id;
            this.Model.Data["Name"] = product.Name;
            this.Model.Data["Price"] = product.Price;
            this.Model.Data["Description"] = product.Description;

            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Delete(ProductBindingModel model)
        {
            this.productsService.DeleteProduct(model.Id);

            return this.RedirectToAction("/");
        }
    }
}