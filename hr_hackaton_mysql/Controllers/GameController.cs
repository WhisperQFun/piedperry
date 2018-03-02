using hr_hackaton_mysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hr_hackaton_mysql.Logic;



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
                ls[2].category_name_english = utf8.encode(ls[2].category_name);
                ls.Add(new list_game());
                ls[3].category_name = "Королевская битва";
                ls[3].category_name_english = utf8.encode(ls[3].category_name);
                ls.Add(new list_game());
                ls[4].category_name = "Детектив";
                ls[4].category_name_english = utf8.encode(ls[4].category_name);
                ls.Add(new list_game());
                ls[5].category_name = "Джунгли";
                ls[5].category_name_english = utf8.encode(ls[5].category_name);

                return View(ls);

            }
            
        }
        [Authorize]
        public ActionResult Play()
        {
            Game games_1 = new Game();
            Game games = new Game();
            GameModel model = new GameModel();
            if (this.Request.QueryString != null && this.Request.QueryString["category"] != null && this.Request.QueryString["answer"] != null && this.Request.QueryString["id_game"] != null)
            {
                var str = this.Request.QueryString["id_game"];
                games_1.id = Convert.ToInt32(str);
                games_1.type = this.Request.QueryString["category"];


                using (UserContext db = new UserContext())
                {
                    games = db.Game.FirstOrDefault(u => u.type == games_1.type  && u.id == games_1.id);
                    
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