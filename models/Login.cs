using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.MVVM.Model
{
    internal class Login
    {
        // Propertki dla tabeli Logowania
        public string IdAccount { get; set; }
        public string Password { get; set; }

        public Login(string id, string pass) 
        {
            IdAccount = id;
            Password = pass;
        }
        public Login (MySqlDataReader reader)
        {
            IdAccount=reader.GetString("Id");
            Password=reader.GetString("Password");
        }
    }
}
