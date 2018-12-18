namespace MyExam.App.Controllers
{
    using Base;
    using BindingModels;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class ProductsController : BaseController
    {
        private const string ProductNotCreated1 = "Unfortunatelly we could not create a product, as some of the details provided are not valid.";
        private const string ProductNotCreated2 = "Unfortunatelly we could not create a product with the details provided. The name, picture or barcode are duplicates.";
        private const string NoProducts = "Unfortunatelly there are no products to display.";

        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [Authorize("Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(ProductBindingModel productModel)
        {
            if (this.ModelState.IsValid != true)
            {
                this.Model.Data["Error"] = ProductNotCreated1;
                return this.View();
            }

            this.productsService.CreateProduct(productModel);

            return this.RedirectToAction("/Products/All");
        }

        [Authorize]
        public IActionResult All()
        {
            var products = this.productsService.GetAllProducts();
            if (products == null)
            {
                this.Model.Data["Error"] = NoProducts;
                return this.View();
            }

            this.Model.Data["Products"] = products;

            return this.View();
        }
    }
}