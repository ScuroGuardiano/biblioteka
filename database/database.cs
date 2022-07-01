using MySql.Data.MySqlClient;

namespace Booker.MVVM.Db {
    // Singleton, jedna instacja klasy dla całego programu
    // https://pl.wikipedia.org/wiki/Singleton_(wzorzec_projektowy)
    internal class Database {
        private Database() {
            // IP bazy, u Was to będzie 127.0.0.1
            string host = "192.168.66.64";
            // Port, u Was to będzie 3306
            string port = "8888";
            string username = "root";
            string password = "dupa1234";
            string database = "shaise";

            string connectionString = String.Format(
                "server={0};port={1};uid={2};pwd={3};database={4};SslMode=Disabled",
                host,
                port,
                username,
                password,
                database
            );

            this.Connection = new MySqlConnection();
            this.Connection.ConnectionString = connectionString;
            
            try {
                this.Connection.Open();
            }
            catch (MySqlException ex) {
                // Tutaj zróbcie sobie z tym exception co chcecie
                // Możecie np. u siebie MessageBoxa wyjebać, że wywaliło się
                // Ja to po prostu throwuję ponownie, bo robię w konsolowej appce
                // wyjebcie tego throwa i dajcie swoją obsługę jeśli chcecie.
                throw ex;
            }
        }

        public MySqlConnection Connection { get; private set; }

        private static Database? _instance;

        public static Database GetInstance() {
            if (Database._instance == null) {
                Database._instance = new Database();
            }
            return Database._instance;
        }
    }
}