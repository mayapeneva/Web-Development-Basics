using SIS.Framework.ActionResults;

namespace PANDA.App.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
