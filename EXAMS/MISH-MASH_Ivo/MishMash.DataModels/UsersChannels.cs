namespace MishMash.DataModels
{
    public class UsersChannels
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Channel Channel { get; set; }
        public int ChannelId { get; set; }
    }
}