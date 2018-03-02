using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace hr_hackaton_mysql.Logic
{
    public class UTF8
    {

        public string encode(string myString)
        {
            var utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(myString);
            myString = utf8.GetString(utfBytes, 0, utfBytes.Length);

            return myString;
        }
    }
}