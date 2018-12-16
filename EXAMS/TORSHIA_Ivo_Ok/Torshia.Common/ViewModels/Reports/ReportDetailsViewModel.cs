namespace Torshia.Common.ViewModels.Reports
{
    using System;
    using Models.Enums;

    public class ReportDetailsViewModel
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public int Level { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime ReportDate { get; set; }

        public string ReporterName { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }

        public string TaskDescription { get; set; }
    }
}