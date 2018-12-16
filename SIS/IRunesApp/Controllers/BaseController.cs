namespace IRunesApp.Controllers
{
    using Common;
    using Data;
    using Services;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class BaseController
    {
        protected IDictionary<string, string> ViewBag;

        public BaseController()
        {
            this.Db = new RunesDbContext();
            this.UserCookieService = new UserCookieService();
            this.ViewBag = new Dictionary<string, string>();
        }

        protected RunesDbContext Db { get; }

        public UserCookieService UserCookieService { get; set; }

        public bool IsLoggedIn { get; set; } = false;

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
            var filePath = Constants.FolderViewsRelativePath
                           + this.GetCurrentControllerName() + "/"
                           + viewName + Constants.HtmlFileExtension;

            if (!File.Exists(filePath))
            {
                return new BadRequestResult(string.Format(Messages.ViewNotFound, viewName), HttpResponseStatusCode.Found);
            }

            string allContent = BuildViewContent(filePath);

            return new HtmlResult(allContent, HttpResponseStatusCode.Ok);
        }

        private string BuildViewContent(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            var layoutContent = File.ReadAllText(Constants.LayoutRelativePath);

            if (this.IsLoggedIn)
            {
                this.ViewBag["isNotAuthenticated"] = "d-none";
                this.ViewBag["isAuthenticated"] = "";
            }
            else
            {
                this.ViewBag["isNotAuthenticated"] = "";
                this.ViewBag["isAuthenticated"] = "d-none";
            }

            foreach (var item in this.ViewBag)
            {
                var dataPlaceholder = $"{{{{{item.Key}}}}}";
                if (fileContent.Contains(dataPlaceholder))
                {
                    fileContent = fileContent.Replace(dataPlaceholder, item.Value);
                }

                if (layoutContent.Contains(dataPlaceholder))
                {
                    layoutContent = layoutContent.Replace(dataPlaceholder, item.Value);
                }
            }

            return layoutContent.Replace("@RenderBody", fileContent);
        }

        private string GetCurrentControllerName() => this.GetType().Name.Replace(Constants.ControllerName, string.Empty);

        //public bool IsAuthenticated(IHttpRequest request)
        //{
        //    return request.Session.ContainsParameter("username");
        //}

        public void SignInUser(string username, IHttpResponse response, IHttpRequest request)
        {
            request.Session.AddParameter("username", username);
            var userCookie = this.UserCookieService.GetUserCookie(username);
            this.IsLoggedIn = true;
            response.Cookies.Add(new HttpCookie(Constants.HttpCookieKey, userCookie, 7));
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