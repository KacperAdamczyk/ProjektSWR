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

        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = id;

            var catId = db.Users.Find(userId).CathedralID.ID;
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
            return View(m);
        }
    }
}