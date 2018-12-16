namespace IRunesApp.Controllers
{
    using Common;
    using Models;
    using Services;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System;
    using System.Linq;

    public class UsersController : BaseController
    {
        private readonly HashService hashService;

        public UsersController()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View("Login");
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashService.Hash(password);
            var user = this.Db.Users.FirstOrDefault(u => u.Username == username && u.HashedPassword == hashedPassword);
            if (user == null)
            {
                return new RedirectResult("/users/login");
            }

            var response = new RedirectResult("/Home/Indexlogged");

            this.SignInUser(username, response, request);

            return response;
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View("Register");
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();
            var email = request.FormData["email"].ToString();

            // Validate
            this.ValidateRegisterUsername(username);
            this.ValidateRegisterPassword(password, confirmPassword);

            // Generate password hash
            var hashedPassword = this.hashService.Hash(password);

            // Create user
            this.CreateAndSaveUser(username, email, hashedPassword);

            var response = new RedirectResult("/");
            this.SignInUser(username, response, request);

            return response;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(Constants.HttpCookieKey))
            {
                return this.View("/home/index");
            }

            var response = new RedirectResult("/");
            var cookie = request.Cookies.GetCookie(Constants.HttpCookieKey);
            cookie.Delete();
            response.Cookies.Add(cookie);

            this.IsLoggedIn = false;

            return response;
        }

        private void CreateAndSaveUser(string userName, string email, string hashedPassword)
        {
            var user = new User()
            {
                Username = userName,
                Email = email,
                HashedPassword = hashedPassword
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
    }
}