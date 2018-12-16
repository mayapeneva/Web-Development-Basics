namespace Torshia.Common.ViewModels.Tasks
{
    using System.Collections.Generic;

    public class TaskViewModelWrapper
    {
        public TaskViewModelWrapper()
        {
            this.Tasks = new List<TaskViewModel>();
        }

        public List<TaskViewModel> Tasks { get; set; }

        public string[] Empty => new string[5 - this.Tasks.Count];
    }
}