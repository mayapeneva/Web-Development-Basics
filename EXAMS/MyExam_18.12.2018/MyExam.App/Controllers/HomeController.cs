namespace MyExam.App.Controllers
{
    using System.Linq;
    using Base;
    using Services.Contracts;
    using SIS.Framework.ActionResults;

    public class HomeController : BaseController
    {
        private const string NoOrders = "Unfortunatelly you have no orders made.";

        private readonly IOrdersService ordersService;

        public HomeController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult Index()
        {
            if (this.Identity == null)
            {
                return this.View();
            }

            var orders = this.ordersService.GetAllUsersOrders(this.Identity);
            if (orders == null)
            {
                this.Model.Data["Error"] = NoOrders;
                return this.View("IndexLoggedIn");
            }

            var total = orders.Sum(o => o.Quantity * o.Price);
            this.Model.Data["Total"] = total;

            this.Model.Data["Orders"] = orders;

            return this.View("IndexLoggedIn");
        }
    }
}