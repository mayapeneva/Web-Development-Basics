namespace MyExam.App.Controllers
{
    using Base;
    using BindingModels;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class OrdersController : BaseController
    {
        private const string NoProduct = "Unfortunatelly there is no product with the barcode procided.";
        private const string NoOrderMade = "Unfortunatelly we could not create an order for you. You are either not logged in or the product is not available.";

        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;

        public OrdersController(IProductsService productsService, IOrdersService ordersService)
        {
            this.productsService = productsService;
            this.ordersService = ordersService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(OrderBindingModel orderModel)
        {
            if (this.ModelState.IsValid != true)
            {
                this.Model.Data["Error"] = NoProduct;
                return this.RedirectToAction("/");
            }

            var product = this.productsService.GetProductByBarcode(orderModel.Barcode);
            if (product == null)
            {
                this.Model.Data["Error"] = NoProduct;
                return this.RedirectToAction("/");
            }

            var result = this.ordersService.CreateOrder(this.Identity, product, orderModel.Quantity);
            if (!result)
            {
                this.Model.Data["Error"] = NoOrderMade;
                return this.RedirectToAction("/");
            }

            return this.RedirectToAction("/");
        }
    }
}