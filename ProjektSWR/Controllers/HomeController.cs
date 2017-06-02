using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektSWR.Models;
using Microsoft.AspNet.Identity;

namespace ProjektSWR.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {            
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser != null)
            {
                ViewBag.isAdmin = currentUser.AdminID;
            }
            else
            {
                ViewBag.isAdmin = null;
            }
            return View(db.Events.Where(e => e.PrivateEventID == null).ToList());
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}