using MySql.Data.MySqlClient;
using Booker.MVVM.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Wykorzystuję tu wzorzec Active Record
    Modele mają metody odpowiadające za zapisywanie, wyszukiwanie i usuwanie ich z bazy danych.

    W przeciwieństwie do podejścia Data Mapper, gdzie modele nie mają żadnych takich funkcji,
    a za operację CRUD [Create, Read, Update, Delete] odpowiadają oddzielne klasy Repozytoriów.
*/

namespace Booker.MVVM.Model
{
    internal class Book
    {
        // Propertki dla tabeli Książki
        public long IdBook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public long IdLibrary { get; set; }
        public string? LibraryAddress { get; set; }
        public string Publisher { get; set; }
        public bool IsAvaliable { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, long idLib, string publisher, bool avaliable, int year)
        {
            Title = title;
            Author = author;
            IdLibrary = idLib;
            Publisher = publisher;
            IsAvaliable = avaliable;
            Year = year;
        }

        public Book Insert() {
            var connection = Database.GetInstance().Connection;

            string sql = @"INSERT INTO Books (title, author, idLibrary, publisher, isAvailable, year)
            VALUES (@title, @author, @idLibrary, @publisher, @isAvailable, @year);";

            var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@title", this.Title);
            command.Parameters.AddWithValue("@author", this.Author);
            command.Parameters.AddWithValue("@idLibrary", this.IdLibrary);
            command.Parameters.AddWithValue("@publisher", this.Publisher);
            command.Parameters.AddWithValue("@isAvailable", this.IsAvaliable);
            command.Parameters.AddWithValue("@year", this.Year);

            command.ExecuteNonQuery();
            this.IdBook = command.LastInsertedId;
            return this;
        }

        public static List<Book> Find(BookFilterOptions? filter = null) {
            var connection = Database.GetInstance().Connection;

            string sql = @"SELECT Books.*, Libraries.address as address FROM Books
            INNER JOIN Libraries
            ON Books.idLibrary = Libraries.id
            "; // NIE USUWAĆ TEGO ENTERA KURWA
            if (filter != null) {
                sql += filter.ToSql();
            }
            sql += ';';
            
            var command = new MySqlCommand(sql, connection);

            // Using w tym kontekście działa tak, że
            // automatycznie nam zamknie readera jak tylko wyjdzie poza swój zakres
            // Gdyż jak nie zamknięmy readera to przy otwarciu drugiego wywali nam błąd
            // Możemy też zamknąć ręcznie za pomocą reader.Close, ale to jest lepsza praktyka
            using(var reader = command.ExecuteReader()) {
                var list = new List<Book>();
                
                while (reader.Read()) {
                    var book = new Book(
                        reader.GetString("title"),
                        reader.GetString("author"),
                        reader.GetInt64("idLibrary"),
                        reader.GetString("publisher"),
                        reader.GetBoolean("isAvailable"),
                        reader.GetInt32("year")
                    ) {
                        IdBook = reader.GetInt64("id"),
                        LibraryAddress = reader.GetString("address")
                    };

                    list.Add(book);
                }

                return list;
            }
        }

        // public Book(MySqlDataReader reader)
        // {
        //     IdBook = int.Parse(reader.GetString("IdBook"));
        //     Title = reader.GetString("Title");
        //     Author = reader.GetString("Author");
        //     IdLibrary = int.Parse(reader.GetString("IdLibrary"));
        // }

    }
}
