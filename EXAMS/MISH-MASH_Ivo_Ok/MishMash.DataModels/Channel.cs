namespace MishMash.DataModels
{
    using System.Collections.Generic;
    using Base;
    using Enums;

    public class Channel : BaseModel<int>
    {
        public Channel()
        {
            this.Followers = new HashSet<UsersChannels>();
            this.Tags = new HashSet<Tag>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ChannelType Type { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<UsersChannels> Followers { get; set; }
    }
}