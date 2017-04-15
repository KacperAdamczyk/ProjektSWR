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
using Newtonsoft.Json;

namespace ProjektSWR.Controllers
{
    class JmessageHeader
    {
        [JsonProperty] private int Id { set; get; }
        [JsonProperty] private string senderName { set; get; }
        [JsonProperty] private string Subject { set; get; }
        [JsonProperty] private DateTime sendDateT { set; get; }
        [JsonProperty] private DateTime receiveDateT { set; get; }
        public JmessageHeader(int Id, string senderName, string Subject, DateTime sendDateT, DateTime receiveDateT)
        {
            this.Id = Id;
            this.senderName = senderName;
            this.Subject = Subject;
            this.sendDateT = sendDateT;
            this.receiveDateT = receiveDateT;
        }
    }

    [Authorize]
    public class MessagesController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult JgetUsers()
        {
            var users = from u in db.Users select u.Email;
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            users = users.Where(u => u != currentUser.Email);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JgetMessageHeaders()
        {
            var messages = from m in db.Messages select m;
            string userId = User.Identity.GetUserId<string>();
            messages = messages.Where(m => (m.OdbiorcaID == userId && !m.Archiwizacja_odbiorca));

            List<JmessageHeader> Jmessage = new List<JmessageHeader>();
            //var user = from u in db.Users select u;
            foreach (var m in messages)
            {
                Jmessage.Add(new JmessageHeader(m.WiadomoscID, m.NadawcaID.Email, m.Temat, m.Data_nadania, m.Data_odbioru ?? DateTime.MinValue));
            }
            return Json(JsonConvert.SerializeObject(Jmessage), JsonRequestBehavior.AllowGet);
        }

        public JsonResult JgetSentMessages()
        {
            var messages = from m in db.Messages select m;
            string userId = User.Identity.GetUserId<string>();
            messages = messages.Where(m => (m.NadawcaID.Id == userId && !m.Archiwizacja_nadawca));
            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        // GET: Messages
        public ActionResult Index()
        {
            return View(db.Messages.ToList());
        }

        public ActionResult Inbox()
        {
            return View();
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public bool CreateMessage(string userName, string Temat, string Tresc)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(Temat) || String.IsNullOrEmpty(Tresc))
                return false;

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            ApplicationUser r = db.Users.FirstOrDefault(x => x.Email == userName);

            Message message = new Message()
            {
                NadawcaID = currentUser,
                OdbiorcaID = r.Id,
                Temat = Temat,
                Tresc = Tresc,
                Data_nadania = DateTime.Now
            };
            db.Messages.Add(message);
            db.SaveChanges();

            return true;
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_wiadomosci,ID_nadawcy,ID_odbiorcy,Temat,Tresc,Data_nadania,Data_odbioru,Archiwizacja_odbiorca,Archiwizacja_nadawca,ID_odpowiedzi")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
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
