namespace MyExam.Services.Contracts
{
    using System.Collections.Generic;
    using SIS.Framework.Security;
    using ViewModels;

    public interface IReportsService
    {
        ReportDto CreateReport(IIdentity user, int taskId);

        IEnumerable<ReportViewModel> GetAllReports();

        ReportDetailsViewModel GetReportById(int reportId);
    }
}