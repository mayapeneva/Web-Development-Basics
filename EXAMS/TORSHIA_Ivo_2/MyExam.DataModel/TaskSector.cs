namespace MyExam.DataModel
{
    using Base;
    using Enums;

    public class TaskSector : BaseModel<int>
    {
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        public Sector Sector { get; set; }
    }
}