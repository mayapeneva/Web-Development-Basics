namespace Torshia.App.Controllers
{
    using System;
    using System.Linq;
    using Common;
    using Common.ViewModels.Tasks;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class TasksController : BaseController
    {
        private readonly ITasksService tasksService;
        private readonly IReportsService reportService;
        private readonly IUsersService userService;

        public TasksController(ITasksService tasksService, IReportsService reportService, IUsersService userService)
        {
            this.tasksService = tasksService;
            this.reportService = reportService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(TaskCreateInputModel model)
        {
            if (!DataValidator.ValidateObject(model))
            {
                //TODO: return Error;
            }

            this.tasksService.CreateTask(model);

            return this.RedirectToAction("/");
        }

        [Authorize]
        public IActionResult Details()
        {
            var taskId = int.Parse(this.Request.QueryData["id"].ToString());
            var task = this.tasksService.GetTaskById(taskId);

            this.Model.Data["Title"] = task.Title;
            this.Model.Data["Level"] = task.AffectedSectors.Count;
            this.Model.Data["DueDate"] = task.DueDate.ToShortDateString();
            this.Model.Data["Participants"] = string.Join(", ", task.Participants.Select(p => p.User.Username));
            this.Model.Data["AffectedSectors"] = string.Join(", ", task.AffectedSectors.Select(s => s.Sector.ToString()));
            this.Model.Data["Description"] = task.Description;

            return this.View();
        }

        [Authorize]
        public IActionResult Report()
        {
            var taskId = int.Parse(this.Request.QueryData["id"].ToString());
            this.tasksService.ReportTask(taskId);

            var reporterId = this.userService.GetUserByUsernameAndPass(this.Identity.Username, this.Identity.Password).Id;

            this.reportService.CreateReport(DateTime.UtcNow, taskId, reporterId);

            return this.RedirectToAction("/");
        }
    }
}