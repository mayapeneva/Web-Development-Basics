namespace SoftUniHttpServer
{
    public class StartUp
    {
        public static void Main()
        {
            IHttpServer server = new HttpServer();

            server.Start();
        }
    }
}