namespace MyExam.App.Controllers
{
    using Base;
    using Services.Contracts;
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;

    public class ReportsController : BaseController
    {
        private const string NoReportCreated = "Something went wrong and the task could not be reported.";
        private const string NoReports = "There are no reports available.";
        private const string NoReportFound = "We could not find the report details. Maybe it was changed in the meantime.";

        private readonly IReportsService reportsService;

        public ReportsController(IReportsService reportsService)
        {
            this.reportsService = reportsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var taskId = int.Parse(this.Request.QueryData["id"].ToString());
            var report = this.reportsService.CreateReport(this.Identity, taskId);
            if (report == null)
            {
                this.Model.Data["Error"] = NoReportCreated;
            }

            return this.RedirectToAction("/");
        }

        [Authorize("Admin")]
        public IActionResult All()
        {
            var reports = this.reportsService.GetAllReports();
            if (reports == null)
            {
                this.Model.Data["Error"] = NoReports;
                return this.View();
            }

            this.Model.Data["Reports"] = reports;

            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Details()
        {
            var reportId = int.Parse(this.Request.QueryData["id"].ToString());
            var report = this.reportsService.GetReportById(reportId);
            if (report == null)
            {
                this.Model.Data["Error"] = NoReportFound;
            }

            this.Model.Data["Report"] = report;

            return this.View();
        }
    }
}