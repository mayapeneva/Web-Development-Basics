namespace MyExam.Services.Contracts
{
    using System.Collections.Generic;
    using BindingModels;
    using SIS.Framework.Security;
    using ViewModels;

    public interface ITasksService
    {
        bool CreateTask(TaskBindingModel model);

        IEnumerable<TaskViewModel> GetAllUsersTasks(IIdentity user);

        TaskDetailsViewModel GetTaskById(int taskId);
    }
}