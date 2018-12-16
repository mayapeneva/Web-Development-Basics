namespace Torshia.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.ViewModels.Reports;
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;

    public class ReportsService : BaseService, IReportsService
    {
        public ReportsService(TorshiaDbContext db)
            : base(db)
        {
        }

        public void CreateReport(DateTime reportedOn, int taskId, int reporterId)
        {
            var random = new Random();
            var status = random.Next(0, 100) > 25 ? Status.Completed : Status.Archived;

            var report = new Report
            {
                Status = status,
                ReportedOn = reportedOn,
                TaskId = taskId,
                ReporterId = reporterId
            };

            this.Db.Reports.Add(report);
            this.Db.SaveChanges();
        }

        public List<ReportViewModel> GetAllReports()
        {
            var reports = this.Db.Reports.ToList();

            var counter = 1;
            var allReports = new List<ReportViewModel>();
            foreach (var report in reports)
            {
                allReports.Add(new ReportViewModel
                {
                    Id = report.Id,
                    Number = counter++,
                    TaskName = report.Task.Title,
                    Level = report.Task.AffectedSectors.Count,
                    Status = report.Status
                });
            }

            return allReports;
        }

        public Report GetReportById(int reportId)
        {
            return this.Db.Reports.Find(reportId);
        }
    }
}