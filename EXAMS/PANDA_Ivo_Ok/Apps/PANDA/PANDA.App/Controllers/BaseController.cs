using SIS.Framework.Controllers;

namespace PANDA.App.Controllers
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using DataModels.Enums;
    using SIS.Framework.ActionResults;

    public class BaseController : Controller
    {
        protected override IViewable View([CallerMemberName]string actionName = "")
        {
            if (this.Identity == null)
            {
                this.Model["IsLoggedAdmin"] = "none";
                this.Model["IsLoggedUser"] = "none";
                this.Model["IsNotLogged"] = "block";
            }
            else
            {
                this.Model["Username"] = this.Identity.Username;

                if (this.Identity.Roles.Contains(Role.Admin.ToString()))
                {
                    this.Model["IsLoggedAdmin"] = "block";
                    this.Model["IsLoggedUser"] = "none";
                    this.Model["IsNotLogged"] = "none";
                }
                else
                {
                    this.Model["IsLoggedAdmin"] = "none";
                    this.Model["IsLoggedUser"] = "block";
                    this.Model["IsNotLogged"] = "none";
                }
            }

            return base.View(actionName);
        }
    }
}