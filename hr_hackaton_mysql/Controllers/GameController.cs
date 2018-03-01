using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hr_hackaton_mysql.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Text()
        {
            return View();
        }
    }
}