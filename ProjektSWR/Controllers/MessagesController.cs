﻿using System;
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
        [JsonProperty] private string UserName { set; get; }
        [JsonProperty] private string Subject { set; get; }
        [JsonProperty] private DateTime SendDate { set; get; }
        [JsonProperty] private DateTime ReceivedDate { set; get; }
        public JmessageHeader(int Id, string UserName, string Subject, DateTime SendDate, DateTime ReceivedDate)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Subject = Subject;
            this.SendDate = SendDate;
            this.ReceivedDate = ReceivedDate;
        }
    }

    [Authorize]
    public class MessagesController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult JgetUsers()
        {
            var users = from u in db.Users select u.Email;
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            users = users.Where(u => u != currentUser.Email);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JgetMessageHeaders()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            List<JmessageHeader> Jmessage = new List<JmessageHeader>();
            foreach (var m in currentUser.Recipients)
            {
                Jmessage.Add(new JmessageHeader(m.ID, m.MessageID.SenderID.Email, m.MessageID.Subject, m.MessageID.SendDate,
                    m.ReceivedDate ?? DateTime.MinValue));
            }
            return Json(JsonConvert.SerializeObject(Jmessage), JsonRequestBehavior.AllowGet);
        }

        public JsonResult JgetSentMessages()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            List<JmessageHeader> Jmessage = new List<JmessageHeader>();
            foreach (var m in currentUser.Messages)
            {
                DateTime? RecivedDate = db.Recipients.FirstOrDefault(r => r.MessageID.ID == m.ID).ReceivedDate;
                Jmessage.Add(new JmessageHeader(m.ID, m.SenderID.Email, m.Subject, m.SendDate, RecivedDate ?? DateTime.MinValue));
            }
            return Json(JsonConvert.SerializeObject(Jmessage), JsonRequestBehavior.AllowGet);
        }

        // GET: Messages
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

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public bool CreateMessage(string UserName, string Subject, string Content)
        {
            if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Subject) || String.IsNullOrEmpty(Content))
                return false;

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            ApplicationUser recipientUser = db.Users.FirstOrDefault(x => x.Email == UserName);

            Message message = new Message()
            {
                SenderID = currentUser,
                Subject = Subject,
                Content = Content,
                SendDate = DateTime.Now
            };
            db.Messages.Add(message);

            Recipient recipient = new Recipient()
            {
                MessageID = message,
                UserID = recipientUser,
            };
            db.Recipients.Add(recipient);
            db.SaveChanges();

            return true;
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
