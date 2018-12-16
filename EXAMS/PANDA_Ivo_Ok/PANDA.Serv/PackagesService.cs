namespace PANDA.Serv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enums;
    using SIS.Framework.Security;
    using ViewModels;

    public class PackagesService : BaseService, IPackagesService
    {
        public PackagesService(PandaDbContext db)
            : base(db)
        {
        }

        public IEnumerable<Package> GetAllPackages(IIdentity user)
        {
            return this.Db.Packages.Where(p => p.Recipient.Username == user.Username);
        }

        public IEnumerable<Package> GetAllPackagesByStatus(PackageStatus status)
        {
            return this.Db.Packages.Where(p => p.PackageStatus == status);
        }

        public Package GetPackageById(IIdentity user, int id)
        {
            return this.Db.Packages.SingleOrDefault(p => p.Recipient.Username == user.Username && p.Id == id);
        }

        public void CreatePackage(PackageBinidngModel model, int recipientId)
        {
            var package = new Package
            {
                Description = model.Description,
                Weight = model.Weight,
                ShippingAddress = model.ShippingAddress,
                PackageStatus = PackageStatus.Pending,
                RecipientId = recipientId
            };

            this.Db.Packages.Add(package);
            this.Db.SaveChanges();
        }

        public void ChangePackageStatus(int packageId, PackageStatus status)
        {
            var package = this.Db.Packages.Find(packageId);
            if (status == PackageStatus.Shipped)
            {
                package.PackageStatus = status;
                var random = new Random();
                package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(random.Next(20, 40));
            }
            else
            {
                package.PackageStatus = status;
            }

            this.Db.SaveChanges();
        }
    }
}