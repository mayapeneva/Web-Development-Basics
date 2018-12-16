namespace MishMash.Services.Contracts
{
    using System.Collections.Generic;
    using BindingModels;
    using SIS.Framework.Security;
    using ViewModels;

    public interface IChannelsService
    {
        IEnumerable<FollowedChannelViewModel> GetAllFollowedChannels(IIdentity user);

        IEnumerable<ChannelViewModel> GetAllSuggestedChannels(IIdentity user);

        IEnumerable<ChannelViewModel> GetAllOtherChannels(IIdentity user);

        ChannelDetailsViewModel GetChannelById(int channelId);

        IEnumerable<MyChannelViewModel> GetMyChannels(IIdentity user);

        void UnfollowChannel(IIdentity user, int channelId);

        void FollowChannel(IIdentity user, int channelId);

        bool ChannelExists(string channelName);

        ChannelBindingModel CreateChannel(ChannelBindingModel model);
    }
}