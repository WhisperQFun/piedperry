using hr_hackaton_mysql.Models;
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
        [Authorize]
        public ActionResult Index()
        {
            using (UserContext db = new UserContext())
            {
                List<list_game> ls = new List<list_game>();
                ls.Add(new list_game());
                ls[0].category_name = "Xоррор";
                ls.Add(new list_game());
                ls[1].category_name = "Космос";
                ls.Add(new list_game());
                ls[2].category_name = "Выживание";
                ls.Add(new list_game());
                ls[3].category_name = "Королевская битва";
                ls.Add(new list_game());
                ls[4].category_name = "Детектив";
                ls.Add(new list_game());
                ls[5].category_name = "Джунгли";
                ls.Add(new list_game());
                ls[6].category_name = "";
                return View(ls);

            }
            
        }
        [Authorize]
        public ActionResult Play()
        {
            Game games_1 = new Game();
            Game games = new Game();
            GameModel model = new GameModel();
            if (this.Request.QueryString != null && this.Request.QueryString["category"] != null && this.Request.QueryString["answer"] != null && this.Request.QueryString["id"] != null)
            {
                var str = this.Request.QueryString["id"];
                games_1.id = Convert.ToInt32(str);
                games_1.type = this.Request.QueryString["category"];


                using (UserContext db = new UserContext())
                {
                    games = db.Game.FirstOrDefault(u => u.type == games_1.type  && u.id == games_1.id);
                    
                }
                
                


                if (games.text != null)
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
                    return RedirectToAction("Index", "Play");
                }

                
            }
            else
            {

                return RedirectToAction("Index", "Play");

            }


            
            


        }
    }
}