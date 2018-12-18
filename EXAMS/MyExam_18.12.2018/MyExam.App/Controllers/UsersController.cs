namespace MyExam.App.Controllers
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
        private const string UnsuccessfulRegistration = "The details you have provided in order to register are not correct.";
        private const string UnsuccessfulLogin = "The details you have provided in order to log in are not correct.";

        private readonly IUsersService userService;
        private readonly IReceiptsService receiptsService;

        public UsersController(IUsersService userService, IReceiptsService receiptsService)
        {
            this.userService = userService;
            this.receiptsService = receiptsService;
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
                this.Model.Data["Error"] = UnsuccessfulRegistration;
                return this.View();
            }

            var user = this.userService.CreateUser(model);
            if (user == null)
            {
                this.Model.Data["Error"] = UnsuccessfulRegistration;
                return this.View();
            }

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
                this.Model.Data["Error"] = UnsuccessfulLogin;
                return this.View();
            }

            if (!this.userService.UserExists(model.Username, model.Password))
            {
                this.Model.Data["Error"] = UnsuccessfulLogin;
                return this.View();
            }

            var user = this.userService.GetUserByUsernameAndPass(model.Username, model.Password);
            if (user == null)
            {
                this.Model.Data["Error"] = UnsuccessfulLogin;
                return this.View();
            }

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

        [Authorize]
        public IActionResult Profile()
        {
            var receipts = this.receiptsService.GetAllUsersReceipts(this.Identity);

            this.Model.Data["Username"] = this.Identity.Username;
            this.Model.Data["Receipts"] = receipts;

            return this.View();
        }
    }
}