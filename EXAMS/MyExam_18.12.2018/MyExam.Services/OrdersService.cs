namespace MyExam.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Common.DTOs;
    using Common.ViewModels;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enums;
    using SIS.Framework.Security;

    public class OrdersService : BaseService, IOrdersService
    {
        public OrdersService(MyExamDbContext context)
            : base(context)
        {
        }

        public bool CreateOrder(IIdentity user, ProductDto product, int quantity)
        {
            var dbUser = this.Db.Users.SingleOrDefault(u => u.Username == user.Username);
            if (dbUser == null)
            {
                return false;
            }

            var dbProduct = this.Db.Products.SingleOrDefault(p => p.Id == product.Id);
            if (dbProduct == null)
            {
                return false;
            }

            var order = new Order
            {
                Status = OrderStatus.Active,
                ProductId = dbProduct.Id,
                Quantity = quantity,
                CashierId = dbUser.Id
            };

            this.Db.Orders.Add(order);
            this.Db.SaveChanges();

            return true;
        }

        public IEnumerable<OrderViewModel> GetAllUsersOrders(IIdentity user)
        {
            var orders = this.Db.Orders.Where(o => o.Cashier.Username == user.Username && o.Status == OrderStatus.Active);
            if (!orders.Any())
            {
                return null;
            }

            return orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                ProductName = o.Product.Name,
                Quantity = o.Quantity,
                Price = o.Product.Price
            });
        }
    }
}