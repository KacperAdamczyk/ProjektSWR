﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity.Validation;
using Microsoft.Owin.Security;
using ProjektSWR.Models;
using Newtonsoft.Json;

namespace ProjektSWR.Controllers
{
    public class PanelController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public JsonResult Users()
        {

            var users = db.Users.Select(x => new { x.FirstName, x.LastName, x.Email, x.CathedralID.Department, x.Id, x.LockoutEndDateUtc, x.LockoutEnabled, x.UserConfirmed, x.AdminID });
            return Json(users, JsonRequestBehavior.AllowGet);
            /*
            return Json(JsonConvert.SerializeObject(db.Users, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }), JsonRequestBehavior.AllowGet); */
        }
        public JsonResult Katedry()
        {
            var katedry = db.Cathedrals.Select(x => new { x.Department });
            return Json(katedry, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Kalendarz()
        {
            var users = db.Events.Select(x => new { x.Title, x.StartDate, x.EndDate, x.Location, x.Details });
            //return Json(users, JsonRequestBehavior.AllowGet);

            return Json(JsonConvert.SerializeObject(users, Formatting.Indented,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManageEvents()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if(currentUser.AdminID != null)
                return View(db.Events.ToList());
            else
                return RedirectToAction("../Home/Index");
        }
        public ActionResult EventsList()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
                return View(db.Events.ToList());
            else
                return RedirectToAction("../Home/Index");
        }
        // GET: Panel
        public ActionResult Index()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
                return View();
            else
                return RedirectToAction("../Home/Index");
        }

        // GET: Panel/Details/5
        public ActionResult Details(int id)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
                return View();
            else
                return RedirectToAction("../Home/Index");
        }

        // GET: Panel/Create
        public ActionResult Create()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
                return View();
            else
                return RedirectToAction("../Home/Index");
        }

        // POST: Panel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                if (currentUser.AdminID != null)
                    return View();
                else
                    return RedirectToAction("../Home/Index");
            }
        }

        // GET: Panel/Edit/5
        public ActionResult Edit(int id)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
                return View();
            else
                return RedirectToAction("../Home/Index");
        }

        // POST: Panel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                if (currentUser.AdminID != null)
                    return View();
                else
                    return RedirectToAction("../Home/Index");
            }
        }

        // GET: Panel/Delete/5
        public ActionResult Delete(int id)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
                return View();
            else
                return RedirectToAction("../Home/Index");
        }

        // POST: Panel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                if (currentUser.AdminID != null)
                    return View();
                else
                    return RedirectToAction("../Home/Index");
            }

        }
        // GET: /Panel/ManageUsers
        [AllowAnonymous]
        public ActionResult ManageUsers()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            var viewModel = new ManageUsersModel();

            if (true)
                return View(viewModel);
            else
                return RedirectToAction("../Home/Index");
        }

        //
        // POST: /Panel/ManageUsers
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManageUsers(ManageUsersModel model)
        {
            if (ModelState.IsValid)
            {
                
                var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var user = UserManager.FindById(model.Id);
                Cathedral c = db.Cathedrals.FirstOrDefault(x => x.Department == model.CathedralName);
                if (user.AdminID != null)
                    model.isAdmin = true;
                else
                    model.isAdmin = false;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.CathedralID = c;

                //Działa przy debugowaniu krok po kroku, ale tak normalnie to nie działa?!?
                if (model.isAdmin == true)
                {
                    Admin op = new Admin();
                    int val = 0;
                    Int32.TryParse(user.Id, out val);
                    op.ID = val;
                    user.AdminID = op;
                }
                else
                {
                    user.AdminID = null;
                }
                    
                //


                db.Entry(user).State = EntityState.Modified;
                var result = await UserManager.UpdateAsync(user);
                db.SaveChanges();
                // If we got this far, something failed, redisplay form
            }
            return View(model);
        }
        // GET: /Panel/LockUsers
        [AllowAnonymous]
        public ActionResult LockUsers()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            var viewModel = new ManageUsersModel();

            if (currentUser.AdminID != null)
                return View(viewModel);
            else
                return RedirectToAction("../Home/Index");
        }


        //
        // POST: /Panel/LockUsers
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LockUsers(ManageUsersModel model)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
            {
                if (ModelState.IsValid)
                {
                    var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    var user = UserManager.FindById(model.Id);
                    Cathedral c = db.Cathedrals.FirstOrDefault(x => x.Department == model.CathedralName);
                    user.CathedralID = c;
                    user.LockoutEndDateUtc = model.LockDate;
                    user.LockoutEnabled = true;
                    var result = await UserManager.UpdateAsync(user);
                    db.SaveChanges();
                    // If we got this far, something failed, redisplay form
                }
                return View(model);
            }
            else
                return RedirectToAction("../Home/Index");

        }
        // GET: /Panel/UnlockUsers
        [AllowAnonymous]
        public ActionResult UnlockUsers()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            var viewModel = new ManageUsersModel();

                if (currentUser.AdminID != null)
                return View(viewModel);
            else
                return RedirectToAction("../Home/Index");
        }


        //
        // POST: /Panel/UnlockUsers
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnlockUsers(ManageUsersModel model)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
            {
                if (ModelState.IsValid)
                {
                    var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    var user = UserManager.FindById(model.Id);
                    Cathedral c = db.Cathedrals.FirstOrDefault(x => x.Department == model.CathedralName);
                    user.CathedralID = c;
                    user.LockoutEnabled = false;
                    var result = await UserManager.UpdateAsync(user);
                    db.SaveChanges();
                    // If we got this far, something failed, redisplay form
                }
                return View(model);
            }
            else
                return RedirectToAction("../Home/Index");
        }
        // GET: Events/Create
        public ActionResult EventsCreate()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
                return View();
            else
                return RedirectToAction("../Home/Index");
        }

        // POST: Events/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventsCreate([Bind(Include = "ID,Title,StartDate,EndDate,Location,Details")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("EventsList");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult EventsEdit(int? id)
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
        public ActionResult EventsEdit([Bind(Include = "ID,Title,StartDate,EndDate,Location,Details")] Event @event)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(@event).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("EventsList");
                }
                return View(@event);
            }
            else
                return RedirectToAction("../Home/Index");

        }

        // GET: Events/Delete/5
        public ActionResult EventsDelete(int? id)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            if (currentUser.AdminID != null)
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
            else
                return RedirectToAction("../Home/Index");
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("EventsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("EventsList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult UserList()
        {
            return View(db.Users.ToList());
        }
        public ActionResult UserProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser @user =  UserManager.FindById(id);
            if (@user == null)
            {
                return HttpNotFound();
            }
            return View(@user); 
        }

        public ActionResult ConfirmUsers()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmUsers(ManageUsersModel model)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated == true)
            {
                if (ModelState.IsValid)
                {
                    var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    var user = UserManager.FindById(model.Id);
                    Cathedral c = db.Cathedrals.FirstOrDefault(x => x.Department == model.CathedralName);
                    user.CathedralID = c;
                    user.UserConfirmed = true;
                    var result = await UserManager.UpdateAsync(user);
                    db.SaveChanges();
                    // If we got this far, something failed, redisplay form
                }
                return View(model);
            }
            else
                return RedirectToAction("../Home/Index");
        }
    }
}

