using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hr_hackaton_mysql.Models
{
    public class LoginModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string middle_name { get; set; }

        [Required]
        public Gender UserGender { get; set; }

        [Required]
        public string about_me { get; set; }

        [Required]
        public string birthday_date { get; set; }
    }

    public class ResumeModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int wanted_salary { get; set; }
    }

    public class EventsModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string date { get; set; }
    }

    public class GameModel
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string text { get; set; }
        [Required]
        public string chose_1 { get; set; }
        [Required]
        public string chose_2 { get; set; }
        [Required]
        public string item_1 { get; set; }
        [Required]
        public string item_2 { get; set; }
        [Required]
        public string right_answer { get; set; }
        [Required]
        public int score { get; set; }
        
    }
   

}