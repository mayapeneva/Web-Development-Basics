namespace MishMash.App.Controllers
{
    using System.Collections.Generic;
    using Base;
    using BindingModels;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Security;

    public class UsersController : BaseController
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterBindingModel model)
        {
            if (this.ModelState.IsValid == false
                || model.Password != model.ConfirmPassword
                || this.userService.UserExists(model.Username, model.Password))
            {
                return this.View();
            }

            this.userService.CreateUser(model);

            var user = this.userService.GetUserByUsernameAndPass(model.Username, model.Password);

            var identity = new IdentityUser
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                Roles = new List<string> { user.Role.ToString() }
            };

            return this.RedirectToAction("/Users/Login");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View();
            }

            if (!this.userService.UserExists(model.Username, model.Password))
            {
                return this.View();
            }

            var user = this.userService.GetUserByUsernameAndPass(model.Username, model.Password);

            var identity = new IdentityUser
            {
                Username = model.Username,
                Password = model.Password,
                Email = user.Email,
                Roles = new List<string> { user.Role.ToString() }
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