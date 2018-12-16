namespace PANDA.Serv
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Contracts;
    using Data;
    using DataModels;
    using SIS.Framework.Security;

    public class ReceiptsService : BaseService, IReceiptsService
    {
        public ReceiptsService(PandaDbContext db)
            : base(db)
        {
        }

        public IEnumerable<Receipt> GetAllReceipts(IIdentity user)
        {
            return this.Db.Receipts.Where(r => r.Recipient.Username == user.Username);
        }

        public Receipt GetReceiptById(IIdentity user, int id)
        {
            return this.Db.Receipts.SingleOrDefault(r => r.Recipient.Username == user.Username && r.Id == id);
        }

        public void CreateReceipt(IIdentity identity, Package package)
        {
            var user = this.Db.Users.SingleOrDefault(u => u.Username == identity.Username);
            if (user != null)
            {
                var fee = Decimal.Parse(package.Weight.ToString(CultureInfo.InvariantCulture)) * 2.67M;
                this.Db.Receipts.Add(new Receipt
                {
                    Fee = fee,
                    IssuedOn = DateTime.UtcNow,
                    RecipientId = user.Id,
                    PackageId = package.Id
                });

                this.Db.SaveChanges();
            }
        }
    }
}