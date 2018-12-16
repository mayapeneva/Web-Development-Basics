namespace IRunesApp
{
    using Controllers;
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Results;
    using SIS.WebServer.Routing;

    public class Launcher
    {
        public static void Main()
        {
            var serverRoutingTable = new ServerRoutingTable();
            ConfigureRouting(serverRoutingTable);

            var server = new Server(80, serverRoutingTable);

            server.Run();
        }

        private static void ConfigureRouting(ServerRoutingTable serverRoutingTable)
        {
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"] = request =>
                new RedirectResult("/");
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request =>
                new HomeController().Index(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"] = request =>
                new UsersController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"] = request =>
                new UsersController().DoLogin(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] = request =>
                new UsersController().Register(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/register"] = request => new UsersController().DoRegister(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/all"] = request =>
                new AlbumsController().All(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/create"] = request =>
                new AlbumsController().Create(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/albums/create"] = request => new AlbumsController().DoCreate(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/details"] = request => new AlbumsController().Details(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/create"] = request =>
                new TracksController().Create(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/tracks/create"] = request => new TracksController().DoCreate(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/details"] =
                request => new TracksController().Details(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/logout"] = request => new UsersController().Logout(request);
        }
    }
}