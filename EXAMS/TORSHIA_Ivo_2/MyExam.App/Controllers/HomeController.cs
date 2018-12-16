namespace MyExam.App.Controllers
{
    using System.Runtime.CompilerServices;
    using Base;
    using SIS.Framework.ActionResults;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.Identity != null)
            {
                return this.View("IndexLogged");
            }

            return this.View();
        }
    }
}