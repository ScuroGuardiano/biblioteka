using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.MVVM.Model
{
    internal class Booking
    {
        // Propertki dla tabeli Rezerwacje
        public int IdBooking { get; set; }
        public int IdBrrowing { get; set; }
        public bool IsReceived { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime ExpiryTerm { get; set; }


        public Booking(int id, int idBorrow, bool receive, DateTime booking, DateTime expiry)
        {
            IdBooking = id;
            IdBrrowing = idBorrow;
            IsReceived = receive;
            BookingTime = booking;
            ExpiryTerm = expiry;
        }

    }
}
