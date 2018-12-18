namespace MyExam.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts;
    using Data;
    using DataModel;
    using DataModel.Enums;
    using SIS.Framework.Security;
    using ViewModels;

    public class ReportsService : BaseService, IReportsService
    {
        public ReportsService(MyExamDbContext context)
            : base(context)
        {
        }

        public ReportDto CreateReport(IIdentity user, int taskId)
        {
            var dbUser = this.Db.Users.Single(u => u.Username == user.Username);
            if (user == null)
            {
                return null;
            }

            var task = this.Db.Tasks.SingleOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return null;
            }

            task.IsReported = true;

            var random = new Random();
            var percent = random.Next(1, 100);
            var status = (ReportStatus)1;
            if (percent < 25)
            {
                status = (ReportStatus)2;
            }

            var report = new Report
            {
                TaskId = taskId,
                ReporterId = dbUser.Id,
                ReportedOn = DateTime.UtcNow,
                Status = status
            };

            this.Db.Reports.Add(report);
            this.Db.SaveChanges();

            return new ReportDto { TaskId = taskId };
        }

        public IEnumerable<ReportViewModel> GetAllReports()
        {
            var number = 1;
            var dbReports = this.Db.Reports;
            if (!dbReports.Any())
            {
                return null;
            }

            var reports = new List<ReportViewModel>();
            foreach (var report in dbReports)
            {
                reports.Add(new ReportViewModel
                {
                    Id = report.Id,
                    Number = number++,
                    Task = report.Task.Title,
                    Level = report.Task.AffectedSectors.Count,
                    Status = report.Status.ToString()
                });
            }

            return reports;
        }

        public ReportDetailsViewModel GetReportById(int reportId)
        {
            var report = this.Db.Reports.Find(reportId);
            if (report == null)
            {
                return null;
            }

            return new ReportDetailsViewModel
            {
                Id = report.Id,
                Task = report.Task.Title,
                Level = report.Task.AffectedSectors.Count,
                Status = report.Status.ToString(),
                DueDate = report.Task.DueDate.ToString("dd/MM/yyyy"),
                ReportedOn = report.ReportedOn.ToString("dd/MM/yyyy"),
                Reporter = report.Reporter.Username,
                Participants = report.Task.Participants,
                AffectedSectors = string.Join(", ", report.Task.AffectedSectors.Select(s => s.Sector.ToString()))
            };
        }
    }
}