namespace Torshia.Models
{
    using System.Collections.Generic;
    using Enums;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Tasks = new HashSet<UserTask>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}