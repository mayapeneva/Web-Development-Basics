namespace MyExam.BindingModels
{
    using System.Collections.Generic;

    public class TaskBindingModel
    {
        public string Title { get; set; }

        public string DueDate { get; set; }

        public string Description { get; set; }

        public string Participants { get; set; }

        public List<string> AffectedSectors { get; set; }
    }
}