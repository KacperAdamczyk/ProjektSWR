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
    class MessageHeader
    {
        [JsonProperty] public int Id { set; get; }
        [JsonProperty] public List<string> Recipient { set; get; }
        [JsonProperty] public string Sender { set; get; }
        [JsonProperty] public string Subject { set; get; }
        [JsonProperty] public DateTime SendDate { set; get; }
        [JsonProperty] public DateTime? ReceivedDate { set; get; }
        public MessageHeader(int Id, string Sender, List<string> Recipient, string Subject, DateTime SendDate, DateTime? ReceivedDate)
        {
            this.Id = Id;
            this.Sender = Sender;
            this.Recipient = Recipient;
            this.Subject = Subject;
            this.SendDate = SendDate;
            this.ReceivedDate = ReceivedDate;
        }
    }

    [Authorize]
    public class MessagesController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Sent()
        {
            return View();
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Content()
        {
            return View();
        }

        public JsonResult Users()
        {
            var users = from u in db.Users select u.Email;
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            users = users.Where(u => u != currentUser.Email);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MessageHeaders()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            List<MessageHeader> Jmessage = new List<MessageHeader>();
            List<string> recipients = new List<string>();
            foreach (var m in currentUser.Recipients)
            {
                if (m.Archived)
                    continue;
                Jmessage.Add(new MessageHeader(m.MessageID.ID, m.MessageID.SenderID.Email, recipients, m.MessageID.Subject, m.MessageID.SendDate,
                    m.ReceivedDate));
            }
            
            Jmessage.Sort((x, y) => x.SendDate.CompareTo(y.SendDate));
            Jmessage.Reverse();
            return Json(JsonConvert.SerializeObject(Jmessage), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SentMessageHeaders()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            List<MessageHeader> Jmessage = new List<MessageHeader>();
            foreach (var m in currentUser.Messages)
            {
                if (m.Archived)
                    continue;

                List<string> recipients = new List<string>();
                foreach (var r in m.Recipients)
                {
                    recipients.Add(r.UserID.UserName);
                }
                DateTime? RecivedDate = db.Recipients.FirstOrDefault(r => r.MessageID.ID == m.ID).ReceivedDate;
                Jmessage.Add(new MessageHeader(m.ID, m.SenderID.Email, recipients, m.Subject, m.SendDate, RecivedDate));
            }
            Jmessage.Sort((x, y) => x.SendDate.CompareTo(y.SendDate));
            Jmessage.Reverse();
            return Json(JsonConvert.SerializeObject(Jmessage), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MessageContent(int id)
        {
            var js = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            var message = db.Messages.Find(id);
            if (message == null)
                return null;
            if (message.SenderID == currentUser)
                return Json(JsonConvert.SerializeObject(message.Content, Formatting.Indented, js), JsonRequestBehavior.AllowGet);

            var recipient = currentUser.Recipients.FirstOrDefault(m => m.MessageID == message);
            
            if (recipient != null)
            {
                if (recipient.ReceivedDate == null)
                {
                    recipient.ReceivedDate = DateTime.Now;
                    db.Entry(recipient).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(JsonConvert.SerializeObject(message.Content, Formatting.Indented, js), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult CreateMessage(List<string> UserName, string Subject, string Content)
        {
            if (UserName == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (UserName.Count() == 0 || String.IsNullOrEmpty(Subject) || String.IsNullOrEmpty(Content))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            List<ApplicationUser> recipientUser = new List<ApplicationUser>();//db.Users.FirstOrDefault(x => x.Email == UserName);
            foreach (var n in UserName)
            {
                if (recipientUser.Find(u => u.Email == n) != null)
                    continue;
                var r = db.Users.ToList().Find(x => x.Email == n);
                if (r != null)
                    recipientUser.Add(r);
            }

            Message message = new Message()
            {
                SenderID = currentUser,
                Subject = Subject,
                Content = Content,
                SendDate = DateTime.Now
            };
            db.Messages.Add(message);

            foreach (var r in recipientUser) {
                Recipient recipient = new Recipient()
                {
                    MessageID = message,
                    UserID = r,
                };
                db.Recipients.Add(recipient);
            }
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult DeleteInbox(List<int> id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id.Count == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            List<Message> messages = new List<Message>();
            foreach (var r in currentUser.Recipients)
            {
                foreach (var i in id)
                {
                    if (i == r.MessageID.ID)
                    {
                        r.Archived = true;
                        db.Entry(r).State = EntityState.Modified;
                    }
                }
            }
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult DeleteSent(List<int> id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (id.Count == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            List<Message> messages = new List<Message>();
            foreach (var i in id)
            {
                var m = db.Messages.Find(i);
                if (m != null && m.SenderID == currentUser)
                {
                    m.Archived = true;
                    db.Entry(m).State = EntityState.Modified;
                }
            }
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
