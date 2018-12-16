namespace MyExam.DataModel
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Enums;

    public class Task : BaseModel<int>
    {
        public Task()
        {
            this.AffectedSectors = new HashSet<TaskSector>();
        }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsReported { get; set; }

        public string Description { get; set; }

        public string Participants { get; set; }

        public virtual ICollection<TaskSector> AffectedSectors { get; set; }
    }
}