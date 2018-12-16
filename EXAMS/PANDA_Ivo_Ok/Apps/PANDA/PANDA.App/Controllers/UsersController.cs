namespace PANDA.App.Controllers
{
    using System.Collections.Generic;
    using Serv.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Security;
    using ViewModels;

    public class UsersController : BaseController
    {
        private readonly IUsersService userService;
        private readonly IHashService hashService;

        public UsersController(IUsersService userService, IHashService hashService)
        {
            this.userService = userService;
            this.hashService = hashService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterBindingModel model)
        {
            if (this.ModelState.IsValid != true
                || model.Password != model.ConfirmPassword
                || this.userService.UserExists(model.Username, model.Password))
            {
                return this.View();
            }

            var hashedPassword = this.hashService.Hash(model.Password);
            this.userService.CreateUser(model.Username, hashedPassword, model.Email);

            var user = this.userService.GetUserByUsernameAndPass(model.Username, hashedPassword);
            var identity = new IdentityUser
            {
                Username = model.Username,
                Password = hashedPassword,
                Email = model.Email,
                Roles = new List<string> { user.Role.ToString() }
            };
            this.SignIn(identity);

            return this.RedirectToAction("/Users/Login");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginBindingModel model)
        {
            if (this.ModelState.IsValid != true)
            {
                return this.View();
            }

            var hashedPassword = this.hashService.Hash(model.Password);
            if (!this.userService.UserExists(model.Username, hashedPassword))
            {
                return this.View();
            }

            var user = this.userService.GetUserByUsernameAndPass(model.Username, hashedPassword);

            var identity = new IdentityUser
            {
                Username = model.Username,
                Password = hashedPassword,
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