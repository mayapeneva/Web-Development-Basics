namespace Cakes.Models
{
    using System;
    using System.Collections.Generic;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}