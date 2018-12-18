namespace MyExam.Services.Contracts
{
    using System.Collections.Generic;
    using Common.ViewModels;
    using SIS.Framework.Security;

    public interface IReceiptsService
    {
        ReceiptViewModel CreateReceipt(IIdentity user, IEnumerable<OrderViewModel> orders);

        IEnumerable<ReceiptAllViewModel> GetAllReceipts();

        IEnumerable<ReceiptAllViewModel> GetAllUsersReceipts(IIdentity user);

        ReceiptViewModel GetReceiptById(int id);
    }
}