namespace PANDA.Serv.Contracts
{
    using System.Collections.Generic;
    using DataModels;
    using SIS.Framework.Security;

    public interface IReceiptsService
    {
        IEnumerable<Receipt> GetAllReceipts(IIdentity user);

        Receipt GetReceiptById(IIdentity user, int id);

        void CreateReceipt(IIdentity identity, Package package);
    }
}