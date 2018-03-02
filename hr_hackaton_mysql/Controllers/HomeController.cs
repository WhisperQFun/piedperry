using hr_hackaton_mysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace hr_hackaton_mysql.Controllers
{
    

    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            User user = new User();
            
            
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                
            }
            return View(user);
        }

        [Authorize(Roles ="admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        
    }
}