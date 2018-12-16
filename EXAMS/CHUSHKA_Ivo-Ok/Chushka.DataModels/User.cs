namespace Chushka.DataModels
{
    using System.Collections.Generic;
    using Enum;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}