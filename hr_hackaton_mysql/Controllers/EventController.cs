using hr_hackaton_mysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hr_hackaton_mysql.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(EventsModel model)
        {
            if (ModelState.IsValid)
            {
                Events events = null;
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                    if (user != null)
                    {
                        events = db.Events.FirstOrDefault(u => u.admin_id == user.id);
                    }
                    
                }
                if (events == null)
                {
                    // создаем нового пользователя
                    using (UserContext db = new UserContext())
                    {
                        db.Events.Add(new Events
                        {
                           name = model.name,
                           description = model.description,
                           date = model.date,
                           admin_id = user.id

                        });
                        db.SaveChanges();

                        
                    }
                    // если пользователь удачно добавлен в бд
                    if (events != null)
                    {
                        
                        return RedirectToAction("Index", "Events");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Успешное редактирование");
                }
            }

            return View(model);

        }


    }
}