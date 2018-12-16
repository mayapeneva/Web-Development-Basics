namespace Torshia.App.Controllers
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Models.Enums;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Controllers;

    public class BaseController : Controller
    {
        protected override IViewable View([CallerMemberName]string actionName = "")
        {
            if (this.Identity == null)
            {
                this.Model.Data["IsLoggedAdmin"] = "none";
                this.Model.Data["IsLoggedUser"] = "none";
                this.Model.Data["IsNotLogged"] = "block";
            }
            else
            {
                this.Model.Data["Username"] = this.Identity.Username;

                if (this.Identity.Roles.Contains(Role.Admin.ToString()))
                {
                    this.Model.Data["IsLoggedAdmin"] = "block";
                    this.Model.Data["IsLoggedUser"] = "none";
                    this.Model.Data["IsNotLogged"] = "none";
                }
                else
                {
                    this.Model.Data["IsLoggedAdmin"] = "none";
                    this.Model.Data["IsLoggedUser"] = "block";
                    this.Model.Data["IsNotLogged"] = "none";
                }
            }

            return base.View(actionName);
        }
    }
}