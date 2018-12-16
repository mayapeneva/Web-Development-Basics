namespace Torshia.Services.Contracts
{
    using System.Collections.Generic;
    using Common.ViewModels.Tasks;
    using Models;

    public interface ITasksService
    {
        ICollection<Task> GetAllNotReportedTasks();

        Task GetTaskById(int taskId);

        void CreateTask(TaskCreateInputModel model);

        void ReportTask(int taskId);
    }
}