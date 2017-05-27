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

namespace ProjektSWR.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Forum
        public ActionResult Index()
        {
            return View(db.Forums.ToList());
        }

        // GET: Forum/Details/5
        public ActionResult Cat(int? i)
        {
            if (i == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forums.Find(i);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        public ActionResult Thr(int? t)
        {
            if (t == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category forum = db.Categories.Find(t);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        public ActionResult Rep(int? th)
        {
            if (th == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread forum = db.Threads.Find(th);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // GET: Forum/Create
        public ActionResult CreateThread()
        {
            return View();
        }
        public ActionResult CreateReply(int? th)
        {
            if (th == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var Thread = db.Threads.Find(th);
            if (Thread == null)
                return HttpNotFound();

            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            Reply reply = new Reply
            {
                Email = currentUser?.Email,
                ThreadID = Thread
            };
            return View(reply);
        }

        // POST: Forum/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateThread([Bind(Include = "ID, Name, MainMessage, AdminID_ID, CategoryID_ID, UserID_Id")] Thread forum)
        {
            if (ModelState.IsValid)
            {
                db.Threads.Add(forum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forum);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReply(int? th, [Bind(Include = "Email, Answer")] Reply forum)
        {
            if (th == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var Thread = db.Threads.Find(th);
            if (Thread == null)
                return HttpNotFound();

            forum.ThreadID = Thread;

                db.Replys.Add(forum);
                db.SaveChanges();
                return RedirectToAction("Index");  
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forums.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Forum/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forum);
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forums.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forums.Find(id);
            db.Forums.Remove(forum);
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
