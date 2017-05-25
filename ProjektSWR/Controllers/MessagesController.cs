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
        [JsonProperty] public List<DateTime?> ReceivedDate { set; get; }
        [JsonProperty] public int ResponseId { set; get; }
        public MessageHeader(int Id, string Sender, List<string> Recipient, string Subject, DateTime SendDate, List<DateTime?> ReceivedDate, int ResponseId)
        {
            this.Id = Id;
            this.Sender = Sender;
            this.Recipient = Recipient;
            this.Subject = Subject;
            this.SendDate = SendDate;
            this.ReceivedDate = ReceivedDate;
            this.ResponseId = ResponseId;
        }
    }

    class MessageContent
    {
        [JsonProperty] public int Id { set; get; }
        [JsonProperty] public string Sender { set; get; }
        [JsonProperty] public string Content { set; get; }
        [JsonProperty] public int ResponseId { set; get; }

        public MessageContent(int Id, string Sender, string Content, int ResponseId)
        {
            this.Id = Id;
            this.Sender = Sender;
            this.Content = Content;
            this.ResponseId = ResponseId;
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

                int responseId = -1;
                if (m.MessageID.ResponseID != null)
                    responseId = m.MessageID.ResponseID.ID;
                Jmessage.Add(new MessageHeader(m.MessageID.ID, m.MessageID.SenderID.Email, recipients, m.MessageID.Subject, m.MessageID.SendDate,
                    new List<DateTime?> { m.ReceivedDate }, responseId));
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
                List<DateTime?> recipients_dates = new List<DateTime?>();
                foreach (var r in m.Recipients)
                {
                    recipients.Add(r.UserID.UserName);
                    recipients_dates.Add(r.ReceivedDate);
                }

                int responseId = -1;
                if (m.ResponseID != null)
                    responseId = m.ResponseID.ID;

                var rec = db.Recipients.FirstOrDefault(x => x.MessageID.ID == m.ID);
                Jmessage.Add(new MessageHeader(m.ID, m.SenderID.Email, recipients, m.Subject, m.SendDate, recipients_dates, responseId));
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
            {
                int ResponseId = -1;
                if (message.ResponseID != null)
                    ResponseId = message.ResponseID.ID;
                return Json(JsonConvert.SerializeObject(new MessageContent(message.ID, message.SenderID.Email, message.Content, ResponseId),
                    Formatting.Indented, js), JsonRequestBehavior.AllowGet);
            }
                

            var recipient = currentUser.Recipients.FirstOrDefault(m => m.MessageID == message);
            
            if (recipient != null)
            {
                if (recipient.ReceivedDate == null)
                {
                    recipient.ReceivedDate = DateTime.Now;
                    db.Entry(recipient).State = EntityState.Modified;
                    db.SaveChanges();
                }
                int ResponseId = -1;
                if (message.ResponseID != null)
                    ResponseId = message.ResponseID.ID;
                return Json(JsonConvert.SerializeObject(new MessageContent(message.ID, message.SenderID.Email, message.Content, ResponseId),
                    Formatting.Indented, js), JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateMessage(List<string> UserName, string Subject, string Content, List<int> ResponseId)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            if (UserName == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UserName.RemoveAll(x => x == "");
            UserName.RemoveAll(x => x == currentUser.Email);

            if (UserName.Count() == 0 || String.IsNullOrEmpty(Subject) || String.IsNullOrEmpty(Content))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Message ResponseMessage = null;
            if (ResponseId[0] >= 0)
            {
                ResponseMessage = db.Messages.Find(ResponseId[0]);
                if (ResponseMessage == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ApplicationUser> recipientUser = new List<ApplicationUser>();//db.Users.FirstOrDefault(x => x.Email == UserName);
            foreach (var n in UserName)
            {
                if (recipientUser.Find(u => u.Email == n) != null)
                    continue;
                var r = db.Users.ToList().Find(x => x.Email == n);
                if (r != null)
                    recipientUser.Add(r);
            }

            if (Subject.Length > 50)
            {
                Subject = Subject.Substring(0, 50);
            }
            Message message = new Message()
            {
                SenderID = currentUser,
                Subject = Subject,
                Content = Content,
                SendDate = DateTime.Now,
                ResponseID = ResponseMessage
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
