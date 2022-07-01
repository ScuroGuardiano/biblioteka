using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booker.MVVM.Db;
using MySql.Data.MySqlClient;

namespace Booker.MVVM.Model
{
    internal class Library
    {
        // Propertki dla tabeli Biblioteki
        public long IdLibrary { get; set; }
        public string Address { get; set; }

        public Library(long id, string add)
        {
            IdLibrary = id;
            Address = add;
        }

        public static List<Library> List() {
            var connection = Database.GetInstance().Connection;

            var sql = "SELECT * FROM Libraries;";
            var command = new MySqlCommand(sql, connection);


            // Using w tym kontekście działa tak, że
            // automatycznie nam zamknie readera jak tylko wyjdzie poza swój zakres
            // Gdyż jak nie zamknięmy readera to przy otwarciu drugiego wywali nam błąd
            // Możemy też zamknąć ręcznie za pomocą reader.Close, ale to jest lepsza praktyka
            using(var reader = command.ExecuteReader()) 
            {
                var list = new List<Library>();
                
                while (reader.Read()) {
                    list.Add(new Library(
                        reader.GetInt64("id"),
                        reader.GetString("address")
                    ));
                }
                return list;
            }
        }
    }
}
