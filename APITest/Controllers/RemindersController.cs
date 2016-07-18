using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using APITest.Models;

namespace APITest.Controllers
{
    public class RemindersController : ApiController
    {
        private ReminderContext db = new ReminderContext();

        // GET: api/Reminders
        [HttpGet]
        [Route("reminders")]
        public IQueryable<Reminder> GetReminders()
        {
            return db.Reminders.OrderBy(x => x.Date);
        }

        // GET: api/Reminders/5
        [HttpGet]
        [Route("reminders/{id}")]
        [ResponseType(typeof(Reminder))]
        public IHttpActionResult GetReminder(int id)
        {
            Reminder reminder = db.Reminders.Find(id);
            if (reminder == null)
            {
                return NotFound();
            }

            return Ok(reminder);
        }

        // PUT: api/Reminders/5
        [HttpPut]
        [Route("reminders/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReminder(int id, Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reminder.Id)
            {
                return BadRequest();
            }

            db.Entry(reminder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reminders
        [HttpPost]
        [Route("reminders")]
        [ResponseType(typeof(Reminder))]
        public IHttpActionResult PostReminder(Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reminders.Add(reminder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reminder.Id }, reminder);
        }

        // DELETE: api/Reminders/5
        [HttpDelete]
        [Route("reminders/{id}")]
        [ResponseType(typeof(Reminder))]
        public IHttpActionResult DeleteReminder(int id)
        {
            Reminder reminder = db.Reminders.Find(id);
            if (reminder == null)
            {
                return NotFound();
            }

            db.Reminders.Remove(reminder);
            db.SaveChanges();

            return Ok(reminder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReminderExists(int id)
        {
            return db.Reminders.Count(e => e.Id == id) > 0;
        }
    }
}