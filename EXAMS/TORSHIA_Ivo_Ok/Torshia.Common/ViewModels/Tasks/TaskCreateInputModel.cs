namespace Torshia.Common.ViewModels.Tasks
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TaskCreateInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Participants { get; set; }

        public string AF_Customers { get; set; }

        public string AF_Marketing { get; set; }

        public string AF_Finances { get; set; }

        public string AF_Internal { get; set; }

        public string AF_Management { get; set; }
    }
}