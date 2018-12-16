namespace MishMash.App
{
    using Data;
    using Services;
    using Services.Contracts;
    using SIS.Framework.Api;
    using SIS.Framework.Services;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<MishMashDbContext, MishMashDbContext>();

            dependencyContainer.RegisterDependency<IHashService, HashService>();

            dependencyContainer.RegisterDependency<IUsersService, UsersService>();
            dependencyContainer.RegisterDependency<IChannelsService, ChannelsService>();
            //dependencyContainer.RegisterDependency<IOrdersService, OrdersService>();

            base.ConfigureServices(dependencyContainer);
        }
    }
}