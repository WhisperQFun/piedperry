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
            List<Events> events = null;

            
            using (UserContext db = new UserContext())
            {
                events = db.Events.ToList<Events>();


            }
            /*events.Add(new Events());
            events[0].name = "Первое мероприятие";
            events[0].description = "Очень важное мероприятие";
            events[0].date = "19.12.2050";*/
            return View(events);
        }

        [Authorize(Roles = "hr,admin")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "hr,admin")]
        public ActionResult Edit(EventsModel model)
        {
           
                if (ModelState.IsValid)
                {
                    User user = null;
                    Events events = null;
                    Events events1 = new Events();
                    using (UserContext db = new UserContext())
                    {
                        events = db.Events.FirstOrDefault(u => u.name == model.name);
                        user = db.Users.Where(u => u.email == User.Identity.Name).FirstOrDefault();
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

                            events = db.Events.Where(u => u.name == model.name && u.date == model.date).FirstOrDefault();
                        }
                        // если пользователь удачно добавлен в бд
                        if (events != null)
                        {

                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Данное мероприятие уже существует");
                        return View(model);
                    }


                    return View(model);
                }
            return View(model);
        }
        



        [Authorize(Roles = "hr,admin")]
        public ActionResult Edit()
        {
            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                EventsModel model = new EventsModel();
                User user = null;
                Events events = null;
                Events events1 = null;
                using (UserContext db = new UserContext())
                {
                    events1.admin_id = Convert.ToInt32(Request.QueryString["id"]);
                    user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                    events = db.Events.FirstOrDefault(u => u.id == events1.admin_id);
                }
                if (events != null)
                {

                    model.name = events.name;
                    model.description = events.description;
                    model.date = events.date;
                    return View(model);
                }
                else
                {
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
                        else
                        {
                            return View(model);
                        }

                    }

                }

            }
            else
            {
                return View();
            }
            return View();
        }

            
            
        

        
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