namespace Torshia.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using Common.ViewModels.Reports;
    using Models;

    public interface IReportsService
    {
        void CreateReport(DateTime ReportedOn, int taskId, int reporterId);

        List<ReportViewModel> GetAllReports();

        Report GetReportById(int reportId);
    }
}