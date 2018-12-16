namespace Torshia.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.ViewModels.Tasks;
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;

    public class TasksService : BaseService, ITasksService
    {
        private readonly IUsersService usersServices;

        public TasksService(TorshiaDbContext db, IUsersService usersServices)
            : base(db)
        {
            this.usersServices = usersServices;
        }

        public ICollection<Task> GetAllNotReportedTasks()
        {
            return this.Db.Tasks.Where(t => !t.IsReported).ToList();
        }

        public Task GetTaskById(int taskId)
        {
            return this.Db.Tasks.Find(taskId);
        }

        public void CreateTask(TaskCreateInputModel model)
        {
            var participantsTokens = model.Participants.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            var prtpnts = new HashSet<UserTask>();
            foreach (var part in participantsTokens)
            {
                if (this.Db.Users.Any(u => u.Username == part))
                {
                    prtpnts.Add(new UserTask
                    {
                        UserId = this.Db.Users.First(u => u.Username == part).Id
                    });
                }
            }

            var sectors = new List<Sector>();
            if (model.AF_Customers != null)
            {
                sectors.Add(Sector.Customers);
            }

            if (model.AF_Finances != null)
            {
                sectors.Add(Sector.Finances);
            }

            if (model.AF_Internal != null)
            {
                sectors.Add(Sector.Internal);
            }

            if (model.AF_Management != null)
            {
                sectors.Add(Sector.Management);
            }

            if (model.AF_Marketing != null)
            {
                sectors.Add(Sector.Marketing);
            }

            var affSectors = new HashSet<TaskSector>();
            foreach (var sector in sectors)
            {
                affSectors.Add(new TaskSector
                {
                    Sector = sector
                });
            }

            var task = new Task
            {
                Title = model.Title,
                DueDate = model.DueDate,
                Description = model.Description,
                Participants = prtpnts,
                AffectedSectors = affSectors,
                IsReported = false
            };

            this.Db.Tasks.Add(task);
            this.Db.SaveChanges();
        }

        public void ReportTask(int taskId)
        {
            var task = this.Db.Tasks.Find(taskId);
            task.IsReported = true;

            this.Db.SaveChanges();
        }
    }
}