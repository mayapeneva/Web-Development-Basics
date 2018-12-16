namespace PANDA.App.Controllers
{
    using System.Collections.Generic;
    using DataModels.Enums;
    using Serv.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using ViewModels;

    public class ReceiptsController : BaseController
    {
        private readonly IReceiptsService receiptsService;
        private readonly IPackagesService packagesService;

        public ReceiptsController(IReceiptsService receiptsService, IPackagesService packagesService)
        {
            this.receiptsService = receiptsService;
            this.packagesService = packagesService;
        }

        [Authorize]
        public IActionResult All()
        {
            var receipts = this.receiptsService.GetAllReceipts(this.Identity);
            var receiptsViewModel = new List<ReceiptViewModel>();
            foreach (var receipt in receipts)
            {
                receiptsViewModel.Add(new ReceiptViewModel
                {
                    Id = receipt.Id,
                    Fee = receipt.Fee,
                    IssuedOn = receipt.IssuedOn != null ? receipt.IssuedOn.Value.ToString("dd/MM/yyyy") : "N/A",
                    Recipient = receipt.Recipient.Username
                });
            }

            this.Model.Data["Receipts"] = receiptsViewModel;

            return this.View();
        }

        [Authorize]
        public IActionResult Details()
        {
            var receiptId = int.Parse(this.Request.QueryData["id"].ToString());

            var receipt = this.receiptsService.GetReceiptById(this.Identity, receiptId);
            var receiptViewModel = new ReceiptDetailsViewModel
            {
                Id = receipt.PackageId,
                IssuedOn = receipt.IssuedOn != null ? receipt.IssuedOn.Value.ToString("dd/MM/yyyy") : "N/A",
                DeliveryAddress = receipt.Package.ShippingAddress,
                PackageWeight = receipt.Package.Weight,
                PackageDescription = receipt.Package.Description,
                Recipient = receipt.Recipient.Username,
                Fee = receipt.Fee
            };

            this.Model.Data["Receipt"] = receiptViewModel;

            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            var packageId = int.Parse(this.Request.QueryData["packageId"].ToString());
            var package = this.packagesService.GetPackageById(this.Identity, packageId);
            this.receiptsService.CreateReceipt(this.Identity, package);
            this.packagesService.ChangePackageStatus(packageId, PackageStatus.Acquired);

            return this.RedirectToAction("/");
        }
    }
}