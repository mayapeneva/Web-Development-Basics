namespace MyExam.Services.Contracts
{
    using BindingModels;

    public interface ITasksService
    {
        bool CreateTask(TaskBindingModel model);
    }
}