namespace MishMash.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using BindingModels;
    using Contracts;
    using Data;
    using DataModels;
    using DataModels.Enums;
    using SIS.Framework.Security;
    using ViewModels;

    public class ChannelsService : BaseService, IChannelsService
    {
        public ChannelsService(MishMashDbContext db)
            : base(db)
        {
        }

        public IEnumerable<FollowedChannelViewModel> GetAllFollowedChannels(IIdentity user)
        {
            var followedChannels = this.GetFollowedChannels(user).ToList();
            if (!followedChannels.Any())
            {
                return null;
            }

            return followedChannels.Select(c => new FollowedChannelViewModel
            {
                Name = c.Name,
                Type = c.Type.ToString(),
                FollowersCount = c.Followers.Count
            });
        }

        private IEnumerable<DataModels.Channel> GetFollowedChannels(IIdentity user)
        {
            return this.Db.Channels.Where(c => c.Followers.Any(f => f.User.Username == user.Username));
        }

        public IEnumerable<ChannelViewModel> GetAllSuggestedChannels(IIdentity user)
        {
            List<Channel> suggestedChannels = this.GetSuggestedChannels(user).ToList();
            if (!suggestedChannels.Any())
            {
                return null;
            }

            return suggestedChannels.Select(c => new ChannelViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type.ToString(),
                FollowersCount = c.Followers.Count
            });
        }

        private List<Channel> GetSuggestedChannels(IIdentity user)
        {
            var suggestedChannels = new List<Channel>();
            var followedTags = this.GetFollowedTags(user);

            var notFollowedChannnels =
                this.GetNotFollowedChannels(user);

            foreach (var channel in notFollowedChannnels)
            {
                foreach (var tag in channel.Tags)
                {
                    if (followedTags.Any(t => t == tag.Name))
                    {
                        suggestedChannels.Add(channel);
                        break;
                    }
                }

                if (suggestedChannels.Any(c => c.Name == channel.Name))
                {
                    break;
                }
            }

            return suggestedChannels;
        }

        private List<string> GetFollowedTags(IIdentity user)
        {
            var followedTags = new List<string>();
            var followedChannelsTags = this.GetFollowedChannels(user).Select(c => c.Tags).ToList();
            foreach (var fct in followedChannelsTags)
            {
                foreach (var tag in fct)
                {
                    followedTags.Add(tag.Name);
                }
            }

            return followedTags;
        }

        private IEnumerable<DataModels.Channel> GetNotFollowedChannels(IIdentity user)
        {
            return this.Db.Channels.Where(c => c.Followers.All(f => f.User.Username != user.Username));
        }

        public IEnumerable<ChannelViewModel> GetAllOtherChannels(IIdentity user)
        {
            var notFollowedChannnels =
                this.GetNotFollowedChannels(user);

            var suggestedChannels = this.GetSuggestedChannels(user);

            var otherChannels = notFollowedChannnels.Where(c => !suggestedChannels.Contains(c)).ToList();
            if (!otherChannels.Any())
            {
                return null;
            }

            return otherChannels.Select(c => new ChannelViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type.ToString(),
                FollowersCount = c.Followers.Count
            });
        }

        public ChannelDetailsViewModel GetChannelById(int channelId)
        {
            var channel = this.Db.Channels.Find(channelId);

            return new ChannelDetailsViewModel
            {
                Name = channel.Name,
                Type = channel.Type.ToString(),
                Followers = channel.Followers.Count,
                Tags = string.Join(", ", channel.Tags.Select(t => t.Name)),
                Description = channel.Description
            };
        }

        public IEnumerable<MyChannelViewModel> GetMyChannels(IIdentity user)
        {
            var myChannels = this.GetFollowedChannels(user);

            var counter = 1;
            return myChannels.Select(c => new MyChannelViewModel
            {
                Counter = counter++,
                Id = c.Id,
                Name = c.Name,
                Type = c.Type.ToString(),
                Followers = c.Followers.Count
            });
        }

        public void UnfollowChannel(IIdentity user, int channelId)
        {
            var userChannel =
                this.Db.UsersChannels.SingleOrDefault(uc =>
                    uc.User.Username == user.Username && uc.ChannelId == channelId);
            if (userChannel != null)
            {
                this.Db.UsersChannels.Remove(userChannel);
                this.Db.SaveChanges();
            }
        }

        public void FollowChannel(IIdentity user, int channelId)
        {
            var dBuser = this.Db.Users.SingleOrDefault(u => u.Username == user.Username);

            if (dBuser != null)
            {
                var userChannel = new UsersChannels
                {
                    UserId = dBuser.Id,
                    ChannelId = channelId
                };

                this.Db.UsersChannels.Add(userChannel);
                this.Db.SaveChanges();
            }
        }

        public bool ChannelExists(string channelName)
        {
            return this.Db.Channels.Any(c => c.Name == channelName);
        }

        public ChannelBindingModel CreateChannel(ChannelBindingModel model)
        {
            var ifParsed = Enum.TryParse(typeof(ChannelType), model.ChannelType, out var channelType);
            if (!ifParsed)
            {
                return null;
            }

            var tags = model.Tags.Split(", ").ToList().Select(t => new Tag { Name = t }).ToHashSet();
            if (tags == null)
            {
                return null;
            }

            var channel = new Channel
            {
                Name = model.Name,
                Description = model.Description,
                Type = (ChannelType)channelType,
                Tags = tags
            };

            this.Db.Channels.Add(channel);
            this.Db.SaveChanges();

            return model;
        }
    }
}