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
        private const string NoTask = "We could not find the task details. Maybe it was changed in the meantime";

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

        [Authorize]
        public IActionResult Details()
        {
            var taskId = int.Parse(this.Request.QueryData["id"].ToString());
            var task = this.tasksService.GetTaskById(taskId);
            if (task == null)
            {
                this.Model.Data["Error"] = NoTask;
                return this.View();
            }

            this.Model.Data["Task"] = task;

            return this.View();
        }
    }
}