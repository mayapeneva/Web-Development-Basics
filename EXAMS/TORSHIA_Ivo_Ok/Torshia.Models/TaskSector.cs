namespace Torshia.Models
{
    using Enums;

    public class TaskSector : BaseModel<int>
    {
        public int TaksId { get; set; }
        public virtual Task Task { get; set; }

        public Sector Sector { get; set; }
    }
}