namespace ProiectMVC.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Reminder
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool Check { get; set; }

    }
}
