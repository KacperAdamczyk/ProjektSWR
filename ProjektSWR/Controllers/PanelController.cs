using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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

            return Json(JsonConvert.SerializeObject(db.Users, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }), JsonRequestBehavior.AllowGet);
        }
        // GET: Panel
        public ActionResult Index()
        {
            return View();
        }

        // GET: Panel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Panel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Panel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Panel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
        // GET: /Panel/ManageUsers
        [AllowAnonymous]
        public ActionResult ManageUsers()
        {
            var viewModel = new ManageUsersModel();

            return View(viewModel);
        }

        // GET: /Panel/ManageEvents
        [AllowAnonymous]
        public ActionResult ManageEvents()
        {

            return View();
        }

        // GET: /Panel/ManageForums
        [AllowAnonymous]
        public ActionResult ManageForums()
        {

            return View();
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
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.CathedralID = c;
                var result = await UserManager.UpdateAsync(user);
                db.SaveChanges();
                // If we got this far, something failed, redisplay form
            }
            return View(model);
        } 
    }
}
