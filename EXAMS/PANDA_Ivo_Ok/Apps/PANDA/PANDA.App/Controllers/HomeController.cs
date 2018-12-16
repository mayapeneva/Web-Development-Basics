using SIS.Framework.ActionResults;

namespace PANDA.App.Controllers
{
    using System.Collections.Generic;
    using Common;
    using DataModels.Enums;
    using Microsoft.EntityFrameworkCore.Internal;
    using Serv.Contracts;
    using ViewModels;

    public class HomeController : BaseController
    {
        private readonly IPackagesService packagesService;

        public HomeController(IPackagesService packagesService)
        {
            this.packagesService = packagesService;
        }

        public IActionResult Index()
        {
            if (this.Identity == null)
            {
                return this.View();
            }

            var packages = this.packagesService.GetAllPackages(this.Identity);

            var pendingPackages = new List<PackViewModel>();
            var shippedPackages = new List<PackViewModel>();
            var deliveredPackages = new List<PackViewModel>();
            foreach (var pack in packages)
            {
                if (pack.PackageStatus == PackageStatus.Pending)
                {
                    pendingPackages.Add(new PackageViewModel
                    {
                        Id = pack.Id,
                        Description = pack.Description
                    });
                }

                if (pack.PackageStatus == PackageStatus.Shipped)
                {
                    shippedPackages.Add(new PackageViewModel
                    {
                        Id = pack.Id,
                        Description = pack.Description
                    });
                }

                if (pack.PackageStatus == PackageStatus.Delivered)
                {
                    deliveredPackages.Add(new DeliveredPackageViewModel
                    {
                        Id = pack.Id,
                        Description = pack.Description
                    });
                }
            }

            if (!pendingPackages.Any())
            {
                pendingPackages.Add(new PackViewModel { Description = Messages.NoPendingPacks });
            }
            this.Model.Data["Pending"] = pendingPackages;

            if (!shippedPackages.Any())
            {
                shippedPackages.Add(new PackViewModel { Description = Messages.NoShippedPacks });
            }
            this.Model.Data["Shipped"] = shippedPackages;

            if (!deliveredPackages.Any())
            {
                deliveredPackages.Add(new PackViewModel { Description = Messages.NoDeliveredPacks });
            }
            this.Model.Data["Delivered"] = deliveredPackages;

            return this.View();
        }
    }
}