namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;
    using DataModels;
    using SIS.Framework.Security;

    public interface IOrdersService
    {
        void CreateOrder(IIdentity user, int productId);

        IEnumerable<Order> GetAllOrders();
    }
}