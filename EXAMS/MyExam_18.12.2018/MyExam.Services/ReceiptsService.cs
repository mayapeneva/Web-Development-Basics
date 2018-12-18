namespace MyExam.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Common.ViewModels;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enums;
    using Microsoft.EntityFrameworkCore;
    using SIS.Framework.Security;

    public class ReceiptsService : BaseService, IReceiptsService
    {
        public ReceiptsService(MyExamDbContext context)
            : base(context)
        {
        }

        public ReceiptViewModel CreateReceipt(IIdentity user, IEnumerable<OrderViewModel> orders)
        {
            var dbUser = this.Db.Users.SingleOrDefault(u => u.Username == user.Username);
            var dbOrders = this.Db.Orders.Where(o => orders.Any(or => or.Id == o.Id)).ToHashSet();
            foreach (var order in dbOrders)
            {
                order.Status = OrderStatus.Completed;
            }

            var receipt = new Receipt
            {
                IssuedOn = DateTime.UtcNow,
                Orders = dbOrders,
                CashierId = dbUser.Id
            };

            this.Db.Receipts.Add(receipt);
            this.Db.SaveChanges();

            return new ReceiptViewModel
            {
                Id = receipt.Id,
                Orders = orders,
                Total = receipt.Total,
                IssuedOn = receipt.IssuedOn.ToString("dd/MM/yyyy"),
                Cashier = receipt.Cashier.Username
            };
        }

        public IEnumerable<ReceiptAllViewModel> GetAllReceipts()
        {
            return this.Db.Receipts.Select(r => new ReceiptAllViewModel
            {
                Id = r.Id,
                Total = r.Total,
                IssuedOn = r.IssuedOn.ToString("dd/MM/yyyy"),
                Cashier = r.Cashier.Username
            });
        }

        public IEnumerable<ReceiptAllViewModel> GetAllUsersReceipts(IIdentity user)
        {
            return this.Db.Receipts.Where(r => r.Cashier.Username == user.Username).Select(r => new ReceiptAllViewModel
            {
                Id = r.Id,
                Total = r.Total,
                IssuedOn = r.IssuedOn.ToString("dd/MM/yyyy"),
                Cashier = r.Cashier.Username
            });
        }

        public ReceiptViewModel GetReceiptById(int id)
        {
            var receipt = this.Db.Receipts.Find(id);

            return new ReceiptViewModel
            {
                Id = receipt.Id,
                Orders = receipt.Orders.Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    ProductName = o.Product.Name,
                    Price = o.Product.Price,
                    Quantity = o.Quantity
                }),
                Total = receipt.Total,
                IssuedOn = receipt.IssuedOn.ToString("dd/MM/yyyy"),
                Cashier = receipt.Cashier.Username
            };
        }
    }
}