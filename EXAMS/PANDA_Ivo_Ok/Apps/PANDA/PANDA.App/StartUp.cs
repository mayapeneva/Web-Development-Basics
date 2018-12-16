using SIS.Framework.Api;

namespace PANDA.App
{
    using Data;
    using Serv;
    using Serv.Contracts;
    using SIS.Framework.Services;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<PandaDbContext, PandaDbContext>();

            dependencyContainer.RegisterDependency<IHashService, HashService>();

            dependencyContainer.RegisterDependency<IUsersService, UsersService>();
            dependencyContainer.RegisterDependency<IPackagesService, PackagesService>();
            dependencyContainer.RegisterDependency<IReceiptsService, ReceiptsService>();
            ////dependencyContainer.RegisterDependency<ISectorsService, SectorsService>();

            base.ConfigureServices(dependencyContainer);
        }
    }
}