namespace PANDA.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using DataModels.Enums;
    using Serv.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using ViewModels;

    public class PackagesController : BaseController
    {
        private readonly IPackagesService packagesService;
        private readonly IUsersService usersService;

        public PackagesController(IPackagesService packagesService, IUsersService usersService)
        {
            this.packagesService = packagesService;
            this.usersService = usersService;
        }

        [Authorize]
        public IActionResult Details()
        {
            var packageId = int.Parse(this.Request.QueryData["id"].ToString());
            var package = this.packagesService.GetPackageById(this.Identity, packageId);
            var packageViewModel = new PackageDetailsViewModel
            {
                Address = package.ShippingAddress,
                Status = package.PackageStatus.ToString(),
                EstimatedDeliveryDate = package.EstimatedDeliveryDate != null ? package.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy") : "N/A",
                Weight = package.Weight,
                Recipient = package.Recipient.Username,
                Description = package.Description
            };

            this.Model.Data["Package"] = packageViewModel;

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Pending()
        {
            var pendingPackages = this.packagesService.GetAllPackagesByStatus(PackageStatus.Pending);
            var pendingViewModels = new List<PendingViewModel>();
            var counter = 1;
            foreach (var package in pendingPackages)
            {
                pendingViewModels.Add(new PendingViewModel
                {
                    Counter = counter++,
                    Description = package.Description,
                    Weight = package.Weight,
                    ShippingAddress = package.ShippingAddress,
                    Recipient = package.Recipient.Username,
                    Id = package.Id
                });
            }

            this.Model.Data["Pending"] = pendingViewModels;

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Shipped()
        {
            var shippedPackages = this.packagesService.GetAllPackagesByStatus(PackageStatus.Shipped);
            var shippedViewModels = new List<ShippedViewModel>();
            var counter = 1;
            foreach (var package in shippedPackages)
            {
                shippedViewModels.Add(new ShippedViewModel
                {
                    Counter = counter++,
                    Description = package.Description,
                    Weight = package.Weight,
                    EstimatedDeliveryDate = package.EstimatedDeliveryDate != null ? package.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyyy") : "N/A",
                    Recipient = package.Recipient.Username,
                    Id = package.Id
                });
            }

            this.Model.Data["Shipped"] = shippedViewModels;

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Delivered()
        {
            var deliveredViewModels = new List<DeliveredViewModel>();

            var deliveredPackages = this.packagesService.GetAllPackagesByStatus(PackageStatus.Delivered);
            var counter = 1;
            foreach (var package in deliveredPackages)
            {
                deliveredViewModels.Add(new DeliveredViewModel
                {
                    Counter = counter++,
                    Description = package.Description,
                    Weight = package.Weight,
                    ShippingAddress = package.ShippingAddress,
                    Recipient = package.Recipient.Username,
                    Id = package.Id
                });
            }

            var acquiredPackages = this.packagesService.GetAllPackagesByStatus(PackageStatus.Acquired);
            counter = 1;
            foreach (var package in acquiredPackages)
            {
                deliveredViewModels.Add(new DeliveredViewModel
                {
                    Counter = counter++,
                    Description = package.Description,
                    Weight = package.Weight,
                    ShippingAddress = package.ShippingAddress,
                    Recipient = package.Recipient.Username
                });
            }

            this.Model.Data["Delivered"] = deliveredViewModels;

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Create()
        {
            var users = this.usersService.GetAllUsers();
            var userViewModels = new List<UserViewModel>();
            var counter = 1;
            foreach (var user in users)
            {
                userViewModels.Add(new UserViewModel
                {
                    Counter = counter++,
                    Username = user.Username
                });
            }

            this.Model.Data["Users"] = userViewModels.OrderBy(u => u.Username);

            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(PackageBinidngModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View();
            }

            var userId = int.Parse(model.Recipient);

            this.packagesService.CreatePackage(model, userId);

            return this.RedirectToAction("/");
        }

        [Authorize("Admin")]
        public IActionResult Ship()
        {
            var packageId = int.Parse(this.Request.QueryData["id"].ToString());
            this.packagesService.ChangePackageStatus(packageId, PackageStatus.Shipped);

            return this.RedirectToAction("/Packages/Pending");
        }

        [Authorize("Admin")]
        public IActionResult Deliver()
        {
            var packageId = int.Parse(this.Request.QueryData["id"].ToString());
            this.packagesService.ChangePackageStatus(packageId, PackageStatus.Delivered);

            return this.RedirectToAction("/Packages/Shipped");
        }
    }
}