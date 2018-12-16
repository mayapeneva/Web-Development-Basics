namespace MishMash.DataModels
{
    using System.Collections.Generic;
    using Base;
    using Enums;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.FollowedChannels = new HashSet<UsersChannels>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<UsersChannels> FollowedChannels { get; set; }
    }
}