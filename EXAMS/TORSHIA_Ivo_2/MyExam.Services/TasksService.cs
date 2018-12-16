namespace MyExam.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using BindingModels;
    using Contracts;
    using Data;
    using DataModel;
    using DataModel.Enums;

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

            var task = new Task
            {
                Title = model.Title,
                DueDate = dueDate,
                Description = model.Description,
                Participants = model.Participants,
                AffectedSectors = affectedSectors,
                IsReported = false
            };

            this.Db.Tasks.Add(task);
            this.Db.SaveChanges();

            return true;
        }
    }
}