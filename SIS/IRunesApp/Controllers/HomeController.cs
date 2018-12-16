namespace IRunesApp.Controllers
{
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;

    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            if (this.IsLoggedIn)
            {
                var username = request.Session.GetParameter("username");
                this.ViewBag["username"] = username.ToString();

                return this.View("IndexLogged");
            }

            return this.View("Index");
        }
    }
}