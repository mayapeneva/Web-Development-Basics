namespace Chushka.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using DataModels;
    using SIS.Framework.Security;

    public class OrdersService : BaseService, IOrdersService
    {
        public OrdersService(ChushkaDbContext db)
            : base(db)
        {
        }

        public void CreateOrder(IIdentity user, int productId)
        {
            var client = this.Db.Users.SingleOrDefault(u => u.Username == user.Username);

            if (client != null)
            {
                var order = new Order
                {
                    ClientId = client.Id,
                    ProductId = productId,
                    OrderedOn = DateTime.UtcNow
                };

                this.Db.Orders.Add(order);
                this.Db.SaveChanges();
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return this.Db.Orders;
        }
    }
}