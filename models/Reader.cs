using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.MVVM.Model
{
    internal class Reader
    {
        // Propertki dla tabeli czytelnicy
        public int IdReader { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int NrBorrowed { get; set; }
        public bool IsLibrarian { get; set; }


        public Reader(int id, string firstName, string lastName, string email, string phone, int borrowed, bool librarian)
        {
            IdReader = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phone;
            NrBorrowed = borrowed;
            IsLibrarian = librarian;
        }

    }
}
