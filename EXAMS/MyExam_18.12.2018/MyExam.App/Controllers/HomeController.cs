namespace MyExam.App.Controllers
{
    using System.Runtime.CompilerServices;
    using Base;
    using SIS.Framework.ActionResults;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}