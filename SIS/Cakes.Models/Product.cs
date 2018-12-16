﻿namespace Cakes.Models
{
    using System.Collections.Generic;

    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}