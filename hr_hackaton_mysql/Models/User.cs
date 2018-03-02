using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr_hackaton_mysql.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }           //Почта
        public string password { get; set; }        //Пароль
        public string first_name { get; set; }      // Имя
        public string last_name { get; set; }       //Фамилия
        public string middle_name { get; set; }     //Отчество
        public Gender UserGender { get; set; }             //Пол
        public string about_me { get; set; }        //Описания себя
        public string tags { get; set; }            //Теги
        public int rating { get; set; }             //Рейтинг
        public int psycho_type { get; set; }        //Психо тип
        public string place_of_work { get; set; }    //Места_работы
        public string birthday_date { get; set; }

        
        public int resume_id { get; set; }          //
        
        public int role_id { get; set; }
        


    }
    public enum Gender
    {
        Мужчина,
        Женщина
    }


    public class Resume
    {
        
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int minimal_rating { get; set; }
        public string tags { get; set; }
        public int wanted_salary { get; set; }
        public string psycho_type { get; set; }
        public int user_id { get; set; }
        
    }

    public class Role
    {
        
        public int id { get; set; }
        public string name { get; set; }            //Название роли
        
    }
    public class Places_of_work
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string description { get; set; }
        public string date_begin { get; set; }
        public string date_end { get; set; }
    }
    public class Events
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string users { get; set; }
        public int admin_id { get; set; }
        

    }
    public class Game
    {
        public int id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string chose_1 { get; set; }
        public string chose_2 { get; set; }
        public string item_1 { get; set; }
        public string item_2 { get; set; }
        public string right_answer { get; set; }
        public int score { get; set; }
    }

    public class list_game
    {
        public string category_name { get; set; }
        public string category_name_english { get; set; }

    }
}
