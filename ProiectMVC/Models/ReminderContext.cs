namespace ProiectMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ReminderContext : DbContext
    {
        public ReminderContext()
            : base("name=Reminder")
        {
        }

        public virtual DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}



Cristian.stan@teamnet.ro

Alexandru.moise@teamnet.ro