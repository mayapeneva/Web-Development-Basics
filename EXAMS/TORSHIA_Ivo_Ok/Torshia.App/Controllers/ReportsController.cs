namespace Torshia.App.Controllers
{
    using System.Linq;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;

    public class ReportsController : BaseController
    {
        private readonly IReportsService reportService;

        public ReportsController(IReportsService reportService)
        {
            this.reportService = reportService;
        }

        [Authorize("Admin")]
        public IActionResult All()
        {
            var reports = this.reportService.GetAllReports();
            this.Model.Data["AllReports"] = reports;

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Details()
        {
            var reportId = int.Parse(this.Request.QueryData["id"].ToString());
            var report = this.reportService.GetReportById(reportId);

            this.Model.Data["Id"] = report.Id;
            this.Model.Data["TaskName"] = report.Task.Title;
            this.Model.Data["Level"] = report.Task.AffectedSectors.Count;
            this.Model.Data["Status"] = report.Status;
            this.Model.Data["DueDate"] = report.Task.DueDate.ToShortDateString();
            this.Model.Data["ReportDate"] = report.ReportedOn.ToShortDateString();
            this.Model.Data["ReporterName"] = report.Reporter.Username;
            this.Model.Data["Participants"] = string.Join(", ", report.Task.Participants.Select(p => p.User.Username));
            this.Model.Data["AffectedSectors"] = string.Join(", ", report.Task.AffectedSectors.Select(s => s.Sector.ToString()));
            this.Model.Data["TaskDescription"] = report.Task.Description;

            return this.View();
        }
    }
}