using hr_hackaton_mysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Migrations;

namespace hr_hackaton_mysql.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.email == model.email && u.password == model.password);
                    

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.email == model.email);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (UserContext db = new UserContext())
                    {
                        db.Users.Add(new User { email = model.email, password = model.password, first_name = model.first_name,
                            last_name = model.last_name, middle_name = model.middle_name, UserGender = model.UserGender, about_me = model.about_me,
                            birthday_date = model.birthday_date,role_id = 2 });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.email == model.email && u.password == model.password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        [Authorize]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Resume()
        {
            ResumeModel model = new ResumeModel();
            User user = null;
            Resume resume = null;
            using (UserContext db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                resume = db.Resume.FirstOrDefault(u => u.user_id == user.id);
            }
            if (resume != null)
            {

                model.name = resume.name;
                model.description = resume.description;
                model.wanted_salary = resume.wanted_salary;
                return View(model);
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Resume(ResumeModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                Resume resume = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                    resume = db.Resume.FirstOrDefault(u => u.user_id == user.id);
                }
                if (resume == null)
                {
                    // создаем нового пользователя
                    using (UserContext db = new UserContext())
                    {
                        db.Resume.Add(new Resume
                        {
                            name = model.name,
                            description = model.description,
                            wanted_salary = model.wanted_salary,
                            user_id = user.id

                        });
                        db.SaveChanges();

                        resume = db.Resume.Where(u => u.id == user.id && u.name == model.name).FirstOrDefault();
                    }
                    // если резюме сохранилось
                    if (resume != null)
                    {
                        
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    using (UserContext db = new UserContext())
                    {
                        resume.name = model.name;
                        resume.description = model.description;
                        resume.wanted_salary = model.wanted_salary;
                        resume.user_id = user.id;

                        db.Resume.AddOrUpdate(resume);
                        db.SaveChanges();

                        
                    }
                    ModelState.AddModelError("", "Резюме обновленно");
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

    }
    
}