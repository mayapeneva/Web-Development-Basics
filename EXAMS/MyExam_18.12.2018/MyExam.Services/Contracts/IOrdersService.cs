namespace MyExam.Services.Contracts
{
    using System.Collections.Generic;
    using Common.DTOs;
    using Common.ViewModels;
    using SIS.Framework.Security;

    public interface IOrdersService
    {
        bool CreateOrder(IIdentity user, ProductDto product, int quantity);

        IEnumerable<OrderViewModel> GetAllUsersOrders(IIdentity user);
    }
}