using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.MVVM.Model
{
    internal class Borrowing
    {
        // Propertki dla tebeli Wypożyczenia
        public int IdBorrowing { get; set; }
        public int IdReader { get; set; }
        public int IdBook { get; set; }
        public DateTime BorrowingTime { get; set; }
        public DateTime Deadline { get; set; }


        public Borrowing(int id, int idRead, int idBook, DateTime Borrow, DateTime deadline)
        {
            IdBorrowing = id;
            IdReader = idRead;
            IdBook = idBook;
            BorrowingTime = deadline;
            Deadline = deadline;
        }
    }
}
