namespace MyExam.DataModels
{
    using System.Collections.Generic;
    using Base;
    using Enums;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
            this.Receipts = new HashSet<Receipt>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}