namespace MyExam.App.Controllers
{
    using System.Runtime.CompilerServices;
    using Base;
    using Services.Contracts;
    using SIS.Framework.ActionResults;

    public class HomeController : BaseController
    {
        private const string NoTasks = "You have no tasks";

        private readonly ITasksService taskService;

        public HomeController(ITasksService taskService)
        {
            this.taskService = taskService;
        }

        public IActionResult Index()
        {
            if (this.Identity != null)
            {
                var tasks = this.taskService.GetAllUsersTasks(this.Identity);
                if (tasks == null)
                {
                    this.Model.Data["Error"] = NoTasks;
                    return this.View();
                }

                this.Model.Data["Tasks"] = tasks;

                return this.View("IndexLogged");
            }

            return this.View();
        }
    }
}