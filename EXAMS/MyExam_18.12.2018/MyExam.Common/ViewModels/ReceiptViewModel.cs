namespace MyExam.Common.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class ReceiptViewModel
    {
        public int Id { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }

        public decimal Total { get; set; }

        public string IssuedOn { get; set; }

        public string Cashier { get; set; }
    }
}