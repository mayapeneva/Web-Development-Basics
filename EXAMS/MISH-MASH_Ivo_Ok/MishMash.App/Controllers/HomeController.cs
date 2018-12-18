namespace MishMash.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using ViewModels;

    public class HomeController : BaseController
    {
        private readonly IChannelsService channelsService;

        public HomeController(IChannelsService channelsService)
        {
            this.channelsService = channelsService;
        }

        public IActionResult Index()
        {
            if (this.Identity != null)
            {
                var followedChannels = this.channelsService.GetAllFollowedChannels(this.Identity);
                var suggestedChannels = this.channelsService.GetAllSuggestedChannels(this.Identity);
                var otherChannels = this.channelsService.GetAllOtherChannels(this.Identity);

                this.Model.Data["FollowedChannels"] = followedChannels ?? new List<FollowedChannelViewModel>
                {
                    new FollowedChannelViewModel
                    {
                        Name = string.Empty,
                        Type = string.Empty,
                        FollowersCount = 0
                    }
                };
                this.Model.Data["SuggestedChannels"] = suggestedChannels ?? new List<ChannelViewModel>
                {
                    new ChannelViewModel
                    {
                        Id = 0,
                        Name = string.Empty,
                        Type = string.Empty,
                        FollowersCount = 0
                    }
                };
                this.Model.Data["OtherChannels"] = otherChannels ?? new List<ChannelViewModel>
                {
                    new ChannelViewModel
                    {
                        Id = 0,
                        Name = string.Empty,
                        Type = string.Empty,
                        FollowersCount = 0
                    }
                };
            }

            return this.View();
        }
    }
}