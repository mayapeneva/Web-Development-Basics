namespace Torshia.App
{
    using Data;
    using Services;
    using Services.Contracts;
    using SIS.Framework.Api;
    using SIS.Framework.Services;

    public class StartUp : IMvcApplication
    {
        public void Configure()
        {
        }

        public void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<TorshiaDbContext, TorshiaDbContext>();

            dependencyContainer.RegisterDependency<IHashService, HashService>();

            dependencyContainer.RegisterDependency<IUsersService, UsersService>();
            dependencyContainer.RegisterDependency<ITasksService, TasksService>();
            dependencyContainer.RegisterDependency<IReportsService, ReportsService>();
            //dependencyContainer.RegisterDependency<ISectorsService, SectorsService>();
        }
    }
}