namespace MyExam.DataModel
{
    using System;
    using Base;
    using Enums;

    public class Report : BaseModel<int>
    {
        public ReportStatus Status { get; set; }

        public DateTime ReportedOn { get; set; }

        public virtual Task Task { get; set; }
        public int TaskId { get; set; }

        public virtual User Reporter { get; set; }
        public int ReporterId { get; set; }
    }
}