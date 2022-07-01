using MySql.Data.MySqlClient;
using Booker.MVVM.Db;
using System.Reflection;

namespace Booker.MVVM.Model {
    internal class BookFilterOptions {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public long? IdLibrary { get; set; }
        public string? Publisher { get; set; }
        public string? Year { get; set; }

        // Konwertuje filterek na SQL-owego WHERE'a
        // Filtrowanie jest trudne ok, nie oceniać.
        public string ToSql() {
            if (!isAnyPropertyNotNull()) {
                return "";
            }

            string sql = "WHERE";
            bool firstAdded = false;

            // Tutaj w drugim argumencie musicie dać nazwę swojej kolumny
            AddPropertyToSql(ref sql, "title", Title, ref firstAdded);
            AddPropertyToSql(ref sql, "author", Author, ref firstAdded);
            AddPropertyToSql(ref sql, "idLibrary", IdLibrary, ref firstAdded);
            AddPropertyToSql(ref sql, "publisher", Publisher, ref firstAdded);
            AddPropertyToSql(ref sql, "year", Year, ref firstAdded);
            
            return sql;
        }

        // Dodaje propertkę do filterka. Nie oceniać.
        public void AddPropertyToSql(ref string sql, string column, object? value, ref bool firstAdded) {
            if (value != null) {
                if (firstAdded) {
                    sql += " AND";
                }
                // MySqlHelper.EscapeString zapobiega atakom SQL Injection
                // W skrócie usuwa niebezpieczne znaki dla SQL. (A dokładniej robi z ' -> \')
                sql += String.Format(" {0} LIKE '%{1}%'", column, MySqlHelper.EscapeString(value.ToString()));
                firstAdded = true;
            }
        }

        // Używając siły magii potężnego wszechmocnego .NET-a sprawdzam czy chociaż jedna
        // propertka nie jest nullem.
        public bool isAnyPropertyNotNull() {
            // Przepraszam.
            PropertyInfo[] properties = typeof(BookFilterOptions).GetProperties();
            foreach (var property in properties) {
                if (property.GetValue(this) != null) {
                    return true;
                }
            }
            return false;
        }
    }
}
