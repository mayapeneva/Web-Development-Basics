namespace MyExam.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using BindingModels;
    using Contracts;
    using Data;
    using DataModel;
    using DataModel.Enums;
    using SIS.Framework.Security;
    using ViewModels;

    public class TasksService : BaseService, ITasksService
    {
        public TasksService(MyExamDbContext context)
            : base(context)
        {
        }

        public bool CreateTask(TaskBindingModel model)
        {
            var ifParsed = DateTime.TryParse(model.DueDate, out DateTime dueDate);
            if (!ifParsed)
            {
                return false;
            }

            var task = new Task
            {
                Title = model.Title,
                DueDate = dueDate,
                Description = model.Description,
                Participants = model.Participants,
                IsReported = false
            };

            if (model.AffectedSectors != null)
            {
                var affectedSectors = new HashSet<TaskSector>();
                foreach (var sector in model.AffectedSectors)
                {
                    ifParsed = Enum.TryParse(typeof(Sector), sector, out var parsedSector);
                    if (!ifParsed)
                    {
                        return false;
                    }

                    affectedSectors.Add(new TaskSector
                    {
                        Sector = (Sector)parsedSector
                    });
                }

                task.AffectedSectors = affectedSectors;
            }

            this.Db.Tasks.Add(task);
            this.Db.SaveChanges();

            return true;
        }

        public IEnumerable<TaskViewModel> GetAllUsersTasks(IIdentity user)
        {
            var result = new List<TaskViewModel>();
            var tasks = this.Db.Tasks.Where(t => !t.IsReported);
            foreach (var task in tasks)
            {
                var participants = task.Participants.Split(", ");
                if (participants.Any(p => p == user.Username))
                {
                    result.Add(new TaskViewModel
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Level = participants.Length
                    });
                }
            }

            if (!result.Any())
            {
                return null;
            }

            return result;
        }

        public TaskDetailsViewModel GetTaskById(int taskId)
        {
            var task = this.Db.Tasks.Single(t => t.Id == taskId);
            if (task == null)
            {
                return null;
            }

            return new TaskDetailsViewModel
            {
                Title = task.Title,
                Level = task.AffectedSectors.Count,
                DueDate = task.DueDate.ToString("dd/MM/yyyy"),
                Participants = task.Participants,
                AffectedSectors = string.Join(", ", task.AffectedSectors.Select(s => s.Sector.ToString())),
                Description = task.Description
            };
        }
    }
}