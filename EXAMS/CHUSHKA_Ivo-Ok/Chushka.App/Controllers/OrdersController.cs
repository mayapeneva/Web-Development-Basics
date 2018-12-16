namespace Chushka.App.Controllers
{
    using System.Collections.Generic;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using ViewModels.ViewModels;

    public class OrdersController : BaseController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var productId = int.Parse(this.Request.QueryData["productId"].ToString());
            this.ordersService.CreateOrder(this.Identity, productId);

            return this.RedirectToAction("/");
        }

        //[Authorize("Admin")]
        public IActionResult All()
        {
            var orders = this.ordersService.GetAllOrders();
            var ordersViewModel = new List<OrderViewModel>();
            var counter = 1;
            foreach (var order in orders)
            {
                ordersViewModel.Add(new OrderViewModel
                {
                    Number = counter++,
                    Id = order.Id,
                    Customer = order.Client.Username,
                    Product = order.Product.Name,
                    OrderedOn = order.OrderedOn.ToString("HH:mm dd/MM/yyyy")
                });
            }

            this.Model.Data["Orders"] = ordersViewModel;

            return this.View();
        }
    }
}