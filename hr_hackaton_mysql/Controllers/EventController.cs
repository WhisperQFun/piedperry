using hr_hackaton_mysql.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Data.Entity.Migrations;

namespace hr_hackaton_mysql.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "hr,admin")]
        public ActionResult Edit()
        {
            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                EventsModel model = new EventsModel();
                User user = null;
                Events events = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                    events = db.Events.FirstOrDefault(u => u.id == Convert.ToInt32(Request.QueryString["id"]));
                }
                if (events != null)
                {

                    model.name = events.name;
                    model.description = events.description;
                    model.date = events.date;
                    return View(model);
                }

            }



            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "hr,admin")]
        public ActionResult Add(EventsModel model)
        {
            if (ModelState.IsValid)
            {
                Events events = null;
                User user = null;
                string Get_data = "0";
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                    if (this.Request.QueryString["id"] != null)
                    {
                        Get_data = this.Request.QueryString["id"];
                    }
                    events = db.Events.FirstOrDefault(u=> u.id.ToString() == Get_data);
                    
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
                        if (events != null) { return RedirectToAction("Index", "Home"); }
                        

                    }
                    
                    if (events != null)
                    {
                        using (UserContext db = new UserContext())
                        {
                            
                            events.name = model.name;
                            events.description = model.description;
                            events.date = model.date;
                            

                            db.Events.AddOrUpdate(events);
                            db.SaveChanges();


                        }
                        
                        ModelState.AddModelError("", "Мероприятие обновленно");



                    }


                        
                        
                    
                }
                else
                {
                    
                    ModelState.AddModelError("", "Неудачная попытка");
                }
            }

            return View();

        }


    }
}