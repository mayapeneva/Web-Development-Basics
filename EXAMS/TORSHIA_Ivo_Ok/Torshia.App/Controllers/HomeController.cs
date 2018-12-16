namespace Torshia.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.ViewModels.Tasks;
    using Services.Contracts;
    using SIS.Framework.ActionResults;

    public class HomeController : BaseController
    {
        private readonly ITasksService tasksService;

        public HomeController(ITasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        public IActionResult Index()
        {
            if (this.Identity == null)
            {
                return this.View();
            }

            var tasks = this.tasksService.GetAllNotReportedTasks()
                .Select(t => new TaskViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Level = t.AffectedSectors.Count
                }).ToList();

            var taskViewModelWrappers = new List<TaskViewModelWrapper>();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (i % 5 == 0)
                {
                    taskViewModelWrappers.Add(new TaskViewModelWrapper());
                }

                taskViewModelWrappers[taskViewModelWrappers.Count - 1].Tasks.Add(tasks[i]);
            }

            this.Model.Data["TaskWrappers"] = taskViewModelWrappers;

            return this.View();
        }
    }
}