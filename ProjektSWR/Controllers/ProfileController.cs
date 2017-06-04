using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektSWR.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ProjektSWR.Controllers
{
    public class ProfileController : Controller
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

        //GET: Profile/Id
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = id;
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catId = user.CathedralID.ID;
            var m = new ProfileModel
            {
                FirstName = db.Users.Find(userId).FirstName,
                LastName = db.Users.Find(userId).LastName,
                AcademicDegree = db.Users.Find(userId).AcademicDegree,
                Photo = db.Users.Find(userId).Photo,
                DateOfBirth = db.Users.Find(userId).DateOfBirth,
                Description = db.Users.Find(userId).Description,
                Email = db.Users.Find(userId).Email,
                PhoneNumber = db.Users.Find(userId).PhoneNumber,
                CathedralName = db.Cathedrals.Find(catId).Department,

            };

            ViewBag.EventsList = this.getCurrentPrivateEvent().Events.ToList();
            return View(m);
        }

    }
}