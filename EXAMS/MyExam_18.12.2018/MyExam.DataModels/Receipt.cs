namespace MyExam.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;

    public class Receipt : BaseModel<int>
    {
        public Receipt()
        {
            this.Orders = new HashSet<Order>();
        }

        public DateTime IssuedOn { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual User Cashier { get; set; }

        public int CashierId { get; set; }

        public decimal Total => this.Orders.Sum(o => o.Quantity * o.Product.Price);
    }
}