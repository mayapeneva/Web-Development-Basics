namespace Torshia.Common.ViewModels.Reports
{
    using Models.Enums;

    public class ReportViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string TaskName { get; set; }

        public int Level { get; set; }

        public Status Status { get; set; }
    }
}