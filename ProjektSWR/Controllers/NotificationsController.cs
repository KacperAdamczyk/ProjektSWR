using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProjektSWR.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace ProjektSWR.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notifications
        public ActionResult Index()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser == null)
                return null;
            var notifications = currentUser.Notifications
                .Where(n => n.Status == "unread")
                .Where(n => n.UserID == currentUser)
                .ToList();
            return View(notifications);
        }

        public ActionResult UnreadBadge()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser == null)
                return null;
            var count = currentUser.Notifications
                .Where(n => n.Status == "unread")
                .Count();
            return View(count);
        }

        [HttpPost]
        public ActionResult MarkRead(int? id)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var notification = currentUser
                .Notifications
                .Where(n => n.ID == id)
                .Single();
            if (notification == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            notification.Status = "read";
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}