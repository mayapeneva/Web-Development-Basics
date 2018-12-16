namespace Cakes.WebApp.Controllers
{
    using Common;
    using Data;
    using Services;
    using Services.Contracts;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System.IO;

    public abstract class BaseController
    {
        protected BaseController()
        {
            this.Db = new CakesDbContext();
            this.UserCookieService = new UserCookieService();
        }

        protected CakesDbContext Db { get; set; }

        protected IUserCookieService UserCookieService { get; set; }

        protected IHttpResponse View(string viewName)
        {
            var content = File.ReadAllText("Views/" + viewName + ".html");
            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

        protected string GetUser(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(Constants.CookieName))
            {
                return null;
            }

            var cookie = request.Cookies.GetCookie(Constants.CookieName);
            var cookieContent = cookie.Value;

            return this.UserCookieService.GetUserData(cookieContent);
        }

        protected IHttpResponse BadRequestError(string errorMessage)
        {
            return new HtmlResult($"<h1>{errorMessage}</h1>", HttpResponseStatusCode.BadRequest);
        }

        protected IHttpResponse ServerError(string errorMessage)
        {
            return new HtmlResult($"<h1>{errorMessage}</h1>", HttpResponseStatusCode.InternalServerError);
        }
    }
}