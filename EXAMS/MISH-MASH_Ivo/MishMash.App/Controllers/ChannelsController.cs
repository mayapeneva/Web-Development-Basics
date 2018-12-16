namespace MishMash.App.Controllers
{
    using Base;
    using BindingModels;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class ChannelsController : BaseController
    {
        private readonly IChannelsService channelsService;

        public ChannelsController(IChannelsService channelsService)
        {
            this.channelsService = channelsService;
        }

        [Authorize("Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(ChannelBindingModel model)
        {
            if (this.ModelState.IsValid == false
                || this.channelsService.ChannelExists(model.Name))
            {
                return this.View();
            }

            var channel = this.channelsService.CreateChannel(model);
            if (channel == null)
            {
                return this.View();
            }

            return this.RedirectToAction("/");
        }

        [Authorize]
        public IActionResult Details()
        {
            var channelId = int.Parse(this.Request.QueryData["id"].ToString());
            var channel = this.channelsService.GetChannelById(channelId);
            this.Model.Data["Channel"] = channel;

            return this.View();
        }

        [Authorize]
        public IActionResult MyChannels()
        {
            var myChannels = this.channelsService.GetMyChannels(this.Identity);
            this.Model.Data["MyChannels"] = myChannels;

            return this.View();
        }

        [Authorize]
        public IActionResult Follow()
        {
            var channelId = int.Parse(this.Request.QueryData["id"].ToString());
            this.channelsService.FollowChannel(this.Identity, channelId);

            return this.RedirectToAction("/");
        }

        [Authorize]
        public IActionResult Unfollow()
        {
            var channelId = int.Parse(this.Request.QueryData["id"].ToString());
            this.channelsService.UnfollowChannel(this.Identity, channelId);

            return this.RedirectToAction("/Channels/MyChannels");
        }
    }
}