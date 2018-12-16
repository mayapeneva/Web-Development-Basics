namespace Torshia.App.Controllers
{
    using System.Collections.Generic;
    using Common;
    using Common.ViewModels.Users;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Security;

    public class UsersController : BaseController
    {
        private readonly IUsersService userService;
        private readonly IHashService hashService;

        public UsersController(IUsersService userService, IHashService hashService)
        {
            this.userService = userService;
            this.hashService = hashService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginInputModel model)
        {
            var hashedPassword = this.hashService.Hash(model.Password);
            if (!this.userService.UserExists(model.Username, hashedPassword))
            {
                return this.RedirectToAction("/Users/Register");
            }

            var user = this.userService.GetUserByUsernameAndPass(model.Username, hashedPassword);
            if (user == null)
            {
                return this.View();
            }

            var identity = new IdentityUser { Username = model.Username, Password = hashedPassword, Roles = new List<string> { user.Role.ToString() } };

            this.SignIn(identity);

            return this.RedirectToAction("/");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                //TODO: return Error;
            }

            if (!DataValidator.ValidateObject(model))
            {
                //TODO: return Error;
            }

            var hashedPassword = this.hashService.Hash(model.Password);
            this.userService.RegisterUser(model.Username, hashedPassword, model.Email);

            var identity = new IdentityUser
            {
                Username = model.Username,
                Password = hashedPassword,
                Email = model.Email
            };
            this.SignIn(identity);

            return this.RedirectToAction("/");
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.SignOut();

            return this.RedirectToAction("/");
        }
    }
}