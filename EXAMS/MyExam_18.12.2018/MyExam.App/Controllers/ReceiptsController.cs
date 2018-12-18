namespace MyExam.App.Controllers
{
    using Base;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class ReceiptsController : BaseController
    {
        private const string NoOrders = "Unfortunatelly you have no orders made.";

        private readonly IOrdersService ordersService;
        private readonly IReceiptsService receiptsService;

        public ReceiptsController(IOrdersService ordersService, IReceiptsService receiptsService)
        {
            this.ordersService = ordersService;
            this.receiptsService = receiptsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var orders = this.ordersService.GetAllUsersOrders(this.Identity);
            if (orders == null)
            {
                this.Model.Data["Error"] = NoOrders;
                return this.View("IndexLoggedIn");
            }

            var receipt = this.receiptsService.CreateReceipt(this.Identity, orders);

            this.Model.Data["Receipt"] = receipt;
            this.Model.Data["Orders"] = receipt.Orders;

            return this.RedirectToAction("/Receipts/Details");
        }

        public IActionResult Details()
        {
            var id = int.Parse(this.Request.QueryData["id"].ToString());

            var receipt = this.receiptsService.GetReceiptById(id);

            this.Model.Data["Receipt"] = receipt;

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult All()
        {
            var receipts = this.receiptsService.GetAllReceipts();

            this.Model.Data["Receipts"] = receipts;

            return this.View();
        }
    }
}