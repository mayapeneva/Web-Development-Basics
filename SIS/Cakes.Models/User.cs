﻿namespace Cakes.Models
{
    using System;
    using System.Collections.Generic;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Order> Orders { get; set; }
    }
}