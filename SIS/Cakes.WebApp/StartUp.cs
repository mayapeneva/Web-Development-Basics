namespace Cakes.WebApp
{
    using Controllers;
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Routing;

    public class StartUp
    {
        public static void Main()
        {
            var serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/register"] = request => new AccountController().Register(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/register"] = request => new AccountController().DoRegister(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/login"] = request => new AccountController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/login"] = request => new AccountController().DoLogin(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/logout"] = request => new AccountController().Logout(request);

            var server = new Server(80, serverRoutingTable);
            server.Run();
        }
    }
}