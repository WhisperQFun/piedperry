using hr_hackaton_mysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hr_hackaton_mysql.Logic;
using System.Data.Entity.Migrations;

namespace hr_hackaton_mysql.Controllers
{
    

    public class GameController : Controller
    {
        // GET: Game
        [Authorize]
        public ActionResult Index()
        {
            using (UserContext db = new UserContext())
            {
                var utf8 = new UTF8();
                List<list_game> ls = new List<list_game>();
                ls.Add(new list_game());
                ls[0].category_name = "Xоррор";
                ls[0].category_name_english = "horror";
                ls.Add(new list_game());
                ls[1].category_name = "Космос";
                ls[1].category_name_english = "space";
                ls.Add(new list_game());
                ls[2].category_name = "Выживание";
                ls[2].category_name_english = "survie";
                ls.Add(new list_game());
                ls[3].category_name = "Королевская битва";
                ls[3].category_name_english = "king_royal";
                ls.Add(new list_game());
                ls[4].category_name = "Детектив";
                ls[4].category_name_english = "detective";
                ls.Add(new list_game());
                ls[5].category_name = "Джунгли";
                ls[5].category_name_english = "jungle";

                return View(ls);

            }
            
        }
        [Authorize]
        public ActionResult Play()
        {
            Game games_1 = new Game();
            Game games = new Game();
            GameModel model = new GameModel();
            User user = null;
            if (this.Request.QueryString != null && this.Request.QueryString["category"] != null && this.Request.QueryString["answer"] != null && this.Request.QueryString["id_game"] != null 
                
                && this.Request.QueryString["score"] != null && this.Request.QueryString["prev_id"] != null)
            {
                var str = this.Request.QueryString["id_game"];
                games_1.id = Convert.ToInt32(str);
                games_1.type = this.Request.QueryString["category"];
                str = this.Request.QueryString["score"];
                games_1.score = Convert.ToInt32(str);
                using (UserContext db = new UserContext())
                {
                    games = db.Game.FirstOrDefault(u => u.type == games_1.type && u.id == games_1.id);
                    user = db.Users.FirstOrDefault(u => u.email == User.Identity.Name);
                }
                if (this.Request.QueryString["id_game"] == "beta")
                {
                    games = new Game();
                    games.text = "Вы прошли альфа версию! Ваш результат составляет:" + user.rating;
                    return View(games);

                }
                if (games_1.id.ToString() == "0" || games.item_1 == "0" || games.item_2 == "0")
                {
                    games = new Game();
                    games.text = "  Вы закончили игру! Ваш результат составляет:" + user.rating;
                    return View(games);
                }
                
                    
                    

                    if (this.Request.QueryString["answer"] == games.right_answer && this.Request.QueryString["prev_id"]!="0" )
                    {
                        using (UserContext db = new UserContext())
                        {
                        var temp_str = this.Request.QueryString["prev_id"];
                        games_1.id = Convert.ToInt32(temp_str);
                        user = db.Users.FirstOrDefault(u=> u.email == User.Identity.Name);
                        user.rating = user.rating + 1;
                        db.Users.AddOrUpdate(user);
                        db.SaveChanges();
                    }
                }





                if (games.id.ToString() == "0" )
                {
                    games.text = "  Вы закончили игру! Ваш результат составляет:"+user.rating;
                    return View(games);
                }
                


                if (games != null)
                {
                    
                    model.id = Convert.ToString(games.id);
                    model.text = games.text;
                    model.chose_1 = games.chose_1;
                    model.chose_2 = games.chose_2;
                    model.item_1 = games.item_1;
                    model.item_2 = games.item_2;
                    model.type = games.type;
                    model.right_answer = games.right_answer;
                    model.score = games.score;
                    return View(games);
                }
                else
                {
                    return RedirectToAction("Index", "Game");
                }

                
            }
            else
            {

                return RedirectToAction("Index", "Game");

            }


            
            


        }
    }
}