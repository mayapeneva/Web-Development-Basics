namespace MyExam.App.Controllers
{
    using Base;
    using BindingModels;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class TasksController : BaseController
    {
        private const string UnsuccessfulTaskCreate = "Could not create a task with details provided";

        private readonly ITasksService tasksService;

        public TasksController(ITasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        [Authorize("Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(TaskBindingModel model)
        {
            if (this.ModelState.IsValid != true)
            {
                this.Model.Data["Error"] = UnsuccessfulTaskCreate;
                return this.View();
            }

            var result = this.tasksService.CreateTask(model);
            if (!result)
            {
                this.Model.Data["Error"] = UnsuccessfulTaskCreate;
                return this.View();
            }

            return this.RedirectToAction("/");
        }
    }
}