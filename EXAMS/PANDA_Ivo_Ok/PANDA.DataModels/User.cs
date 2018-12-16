namespace PANDA.DataModels
{
    using System.Collections.Generic;
    using Enums;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Packages = new HashSet<Package>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
    }
}