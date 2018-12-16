namespace Cakes.WebApp.Controllers
{
    using Common;
    using Models;
    using Services;
    using Services.Contracts;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System;
    using System.Linq;

    public class AccountController : BaseController
    {
        private readonly IHashService hashService;

        public AccountController()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View("Register");
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();

            // Validate
            this.ValidateRegisterUsername(userName);
            this.ValidateRegisterPassword(password, confirmPassword);

            // Generate password hash
            var hashedPassword = this.hashService.Hash(password);

            // Create user
            this.CreateAndSaveUser(userName, hashedPassword);

            // Login User
            this.DoLogin(request);

            // Redirect to home page
            return new RedirectResult("/");
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View("Login");
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString();
            var password = request.FormData["password"].ToString();

            // Validate
            if (!this.Db.Users.Any(u => u.Username == username))
            {
                return new RedirectResult("/register");
            }
            else
            {
                this.ValidateLoginUser(username, password);
            }

            // Save cookie/session for user
            var cookieContent = this.UserCookieService.GetUserCookie(username);

            var response = new RedirectResult("/");
            response.Cookies.Add(new HttpCookie(Constants.CookieName, cookieContent, 7));

            return response;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(Constants.CookieName))
            {
                return new RedirectResult("/");
            }

            var cookie = request.Cookies.GetCookie(Constants.CookieName);
            cookie.Delete();

            var response = new RedirectResult("/");
            response.Cookies.Add(cookie);

            return response;
        }

        private void CreateAndSaveUser(string userName, string hashedPassword)
        {
            var user = new User()
            {
                Name = userName,
                Username = userName,
                Password = hashedPassword
            };

            try
            {
                this.Db.Users.Add(user);
                this.Db.SaveChanges();
            }
            catch (Exception exception)
            {
                this.ServerError(exception.Message);
            }
        }

        private void ValidateRegisterPassword(string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                this.BadRequestError(Messages.InvalidPassword);
            }

            if (password != confirmPassword)
            {
                this.BadRequestError(Messages.PasswordsDontMatch);
            }
        }

        private void ValidateRegisterUsername(string userName)
        {
            if (string.IsNullOrEmpty(userName) || userName.Length < 4)
            {
                this.BadRequestError(Messages.InvalidUsername);
            }

            if (this.Db.Users.Any(u => u.Username == userName))
            {
                this.BadRequestError(Messages.UserExists);
            }
        }

        private void ValidateLoginUser(string username, string password)
        {
            var user = this.Db.Users.First(u => u.Username == username);
            var hashedPassword = this.hashService.Hash(password);
            if (user.Password != hashedPassword)
            {
                this.BadRequestError(Messages.WrongPassword);
            }
        }
    }
}