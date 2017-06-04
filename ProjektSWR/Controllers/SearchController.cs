using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektSWR.Models
{
    public class SearchController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Search
        public ActionResult Index()
        {
            Search m = new Search();
            m.users = db.Users.ToList();
            return View(m);
        }
    }
}