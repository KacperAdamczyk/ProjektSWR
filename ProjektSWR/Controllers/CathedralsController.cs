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

namespace ProjektSWR.Controllers
{
    public class CathedralsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cathedrals
        public ActionResult Index()
        {
            return View(db.Cathedrals.ToList());
        }

        public JsonResult Cathedrals()
        {
           
            return Json(JsonConvert.SerializeObject(db.Cathedrals, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }), JsonRequestBehavior.AllowGet);
        }

        // GET: Cathedrals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cathedral cathedral = db.Cathedrals.Find(id);
            if (cathedral == null)
            {
                return HttpNotFound();
            }
            return View(cathedral);
        }

        // GET: Cathedrals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cathedrals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Department,Address,Page,PhoneNumber,Email")] Cathedral cathedral)
        {
            if (ModelState.IsValid)
            {
                db.Cathedrals.Add(cathedral);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cathedral);
        }

        // GET: Cathedrals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cathedral cathedral = db.Cathedrals.Find(id);
            if (cathedral == null)
            {
                return HttpNotFound();
            }
            return View(cathedral);
        }

        // POST: Cathedrals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Department,Address,Page,PhoneNumber,Email")] Cathedral cathedral)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cathedral).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cathedral);
        }

        // GET: Cathedrals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cathedral cathedral = db.Cathedrals.Find(id);
            if (cathedral == null)
            {
                return HttpNotFound();
            }
            return View(cathedral);
        }

        // POST: Cathedrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cathedral cathedral = db.Cathedrals.Find(id);
            db.Cathedrals.Remove(cathedral);
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
