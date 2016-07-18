using ProiectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace ProiectMVC.Controllers
{
    public class RemindersController : Controller
    {
        // GET: Reminders
        [HttpGet]
        [Route("Reminders")]
        public async Task<ActionResult> Index()
        {
            var reminderList = await ServiceGetAllReminders();
            return View(reminderList);
        }

        async Task<List<Reminder>> ServiceGetAllReminders()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61983");

                HttpResponseMessage raspuns = await client.GetAsync("/reminders");

                if (raspuns.IsSuccessStatusCode)
                {
                    return await raspuns.Content.ReadAsAsync<List<Reminder>>();
                }

                return null;
            }
        }

        // GET: Reminders/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var reminder = await ServiceGetReminder(id);
            return View(reminder);
        }

        async Task<Reminder> ServiceGetReminder(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61983");

                HttpResponseMessage raspuns = await client.GetAsync("/reminders/" + id);

                if (raspuns.IsSuccessStatusCode)
                {
                    return await raspuns.Content.ReadAsAsync<Reminder>();
                }

                return null;
            }
        }



        // GET: Reminders/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reminders/Create
        [HttpPost]
        public async Task<ActionResult> Create(Reminder reminder)
        {
            bool textDateOk = true;
            bool notDuplicate = true;
            if (reminder.Text == "" || reminder.Text == null || reminder.Date == null)
                textDateOk = false;

            var reminderList = await ServiceGetAllReminders();
            foreach (var item in reminderList)
            {
                if(reminder.Date == item.Date)
                {
                    notDuplicate = false;
                }
            }

            if (textDateOk && notDuplicate)
            {
                try
                {
                    await ServicePostReminder(reminder);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(reminder);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"Nu poate fi introdus un reminder fara text/data sau un reminder duplicat (acceasi data)!");
            }
        }

        async Task<Uri> ServicePostReminder(Reminder reminder)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61983");

                HttpResponseMessage raspuns = await client.PostAsJsonAsync("/reminders", reminder);

                if (raspuns.IsSuccessStatusCode)
                {
                    return raspuns.Headers.Location;
                }

                return null;
            }
        }


        // GET: Reminders/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var reminder = await ServiceGetReminder(id);
            return View(reminder);
        }

        // POST: Reminders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Text,Date,Check")]Reminder reminder)
        {
            try
            {
                await ServiceEditReminder(reminder.Id, reminder);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        async Task<Uri> ServiceEditReminder(int id, Reminder reminder)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61983");

                HttpResponseMessage raspuns = await client.PutAsJsonAsync("/reminders/" + id, reminder);

                if (raspuns.IsSuccessStatusCode)
                {
                    return raspuns.Headers.Location;
                }

                return null;
            }
        }


        // GET: Reminders/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var reminder = await ServiceGetReminder(id);
            return View(reminder);
        }

        // POST: Reminders/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await ServiceDeleteReminder(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        async Task<Uri> ServiceDeleteReminder(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61983");

                HttpResponseMessage raspuns = await client.DeleteAsync("/reminders/" + id);

                if (raspuns.IsSuccessStatusCode)
                {
                    return raspuns.Headers.Location;
                }

                return null;
            }
        }
    }
}
