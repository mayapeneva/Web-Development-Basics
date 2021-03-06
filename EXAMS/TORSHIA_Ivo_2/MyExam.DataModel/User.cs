﻿namespace MyExam.DataModel
{
    using System.Collections.Generic;
    using Base;
    using Enums;

    public class User : BaseModel<int>
    {
        public User()
        {
            this.Reports = new HashSet<Report>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}