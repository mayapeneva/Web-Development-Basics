namespace MyExam.App
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
            dependencyContainer.RegisterDependency<MyExamDbContext, MyExamDbContext>();

            dependencyContainer.RegisterDependency<IHashService, HashService>();

            dependencyContainer.RegisterDependency<IUsersService, UsersService>();
            dependencyContainer.RegisterDependency<IProductsService, ProductsService>();
            dependencyContainer.RegisterDependency<IOrdersService, OrdersService>();
            dependencyContainer.RegisterDependency<IReceiptsService, ReceiptsService>();

            base.ConfigureServices(dependencyContainer);
        }
    }
}