namespace MyExam.App
{
    using Services;
    using Services.Contracts;
    using SIS.Framework.Api;
    using SIS.Framework.Services;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            //dependencyContainer.RegisterDependency<DbContext, DbContext>();

            dependencyContainer.RegisterDependency<IHashService, HashService>();

            dependencyContainer.RegisterDependency<IUsersService, UsersService>();
            //dependencyContainer.RegisterDependency<IProductsService, ProductsService>();
            //dependencyContainer.RegisterDependency<IOrdersService, OrdersService>();

            base.ConfigureServices(dependencyContainer);
        }
    }
}