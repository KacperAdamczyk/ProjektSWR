using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektSWR.Models;
using Microsoft.AspNet.Identity;

namespace ProjektSWR.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private PrivateEvent currentPEvent = new PrivateEvent();

        public PrivateEvent getCurrentPrivateEvent()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            PrivateEvent currentPrivateEvent = new PrivateEvent();
            bool found = false;
            foreach (PrivateEvent myEvent in db.PrivateEvents)
            {
                if (myEvent.UserID == currentUser)
                {
                    currentPrivateEvent = myEvent;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                PrivateEvent newPrivateList = new PrivateEvent();
                newPrivateList.UserID = currentUser;
                newPrivateList.AdminID = db.Admins.FirstOrDefault();
                db.PrivateEvents.Add(newPrivateList);
                db.SaveChanges();
                currentPrivateEvent = newPrivateList;
            }

            return currentPrivateEvent;
        }

        // GET: Events
        public ActionResult Index()
        {
            //ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            //PrivateEvent currentPrivateEvent = new PrivateEvent();
            //bool found = false;
            //foreach (PrivateEvent myEvent in db.PrivateEvents)
            //{
            //    if(myEvent.UserID == currentUser)
            //    {
            //        currentPrivateEvent = myEvent;
            //        found = true;
            //        break;
            //    }
            //}

            //if(!found)
            //{
            //    PrivateEvent newPrivateList = new PrivateEvent();
            //    newPrivateList.UserID = currentUser;
            //    newPrivateList.AdminID = db.Admins.FirstOrDefault();
            //    db.PrivateEvents.Add(newPrivateList);
            //    db.SaveChanges();
            //    currentPrivateEvent = db.PrivateEvents.LastOrDefault();
            //}

            //PrivateEvent currentUserPrivateEvents = db.PrivateEvents.Where("UserID_id", currentUser.Id);

            //PrivateEvent nowe = db.PrivateEvents.FirstOrDefault();

            //Event test = nowe.Events.FirstOrDefault();

            //nowe.ToString();
            //PrivateEvent test = new PrivateEvent();
            //Event wyd = db.Events.Find(20);
            //test.Events.Add(wyd);
            //test.AdminID = db.Admins.Find(1);
            //test.UserID = db.Users.FirstOrDefault();
            //db.PrivateEvents.Add(test);
            //db.SaveChanges();

            //var admin = new Admin();
            //admin.ID = 1;
            //db.Admins.Add(admin);
            //db.SaveChanges();
            
            return View(this.getCurrentPrivateEvent().Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,StartDate,EndDate,Location,Details")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                CreateEventNotification(@event);
                return RedirectToAction("Index");
            }

            return View(@event);
        }
        // POST: Events/AjaxCreate
          [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "ID,Title,StartDate,EndDate,Location,Details")] Event @event)
        {
            if (ModelState.IsValid)
            {
                //this.getCurrentPrivateEvent().Events.Add(@event);
                db.Events.Add(@event);                
                db.SaveChanges();
                return Json(@event);
            }

            return View(@event);
        }

        // POST: Events/AjaxCreatePrivateEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreatePrivateEvent([Bind(Include = "ID,Title,StartDate,EndDate,Location,Details")] Event @event)
        {
            if (ModelState.IsValid)
            {
                this.getCurrentPrivateEvent().Events.Add(@event);
                Event changedEvent = this.getCurrentPrivateEvent().Events.LastOrDefault();
                //db.PrivateEvents.Add(@event);
                db.SaveChanges();
                Event response = new Event();
                response.ID = changedEvent.ID;
                response.Title = changedEvent.Title;
                response.Location = changedEvent.Location;
                response.Details = changedEvent.Details;
                response.StartDate = changedEvent.StartDate;
                response.EndDate = changedEvent.EndDate;
                return Json(response);
            }
            else
            {
                return Json(ModelState.Keys);
            }
        }

        private void CreateEventNotification(Event evnt)
        {
            var notification = new Notification()
            {
                Status = "unread",
                Contents = String.Format("Masz nowe wydarzenie: {0} dnia {1}",
                    evnt.Title, evnt.StartDate),
                EventID = evnt
            };
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,StartDate,EndDate,Location,Details")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            return View(@event);
        }

        // POST: Events/AjaxEdit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxEdit([Bind(Include = "ID,Title,StartDate,EndDate,Location,Details")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            return View(@event);
        }
        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Events/AjaxDelete/5
        [HttpPost, ActionName("AjaxDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxDeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return Json(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
