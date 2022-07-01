using System.Linq;
using Booker.MVVM.Model;

var jebanySmieszekOrzeszek = new Examples();
jebanySmieszekOrzeszek.WyszukajKsiazki();

internal class Examples {
    public void UtworzKsiazkeIDodajDoBazy() {
        // Te dane z interfejsu użytkownika bierzecie, jak użytkownik kliknie dodaj książkę
        // To zczytujecie wszystkie dane z kontrolek, tworzycie nową książkę i ją zapisujecie
        // Metodą insert
        Book rok1984 = new Book("Rok 1984", "George Orwell", 1, "MUZA", true, 2009);

        rok1984.Insert();

        // Wypisuje żeby było widać LOL. U Was Console.WriteLine raczej nie zadziała xD
        // Message.Boxa jebnijcie, a to zwrapować można w String.Format
        Console.WriteLine(
            "Dodano książkę {0}, autora {1} do bazy. Jej ID to {3}",
            rok1984.Title,
            rok1984.Author,
            rok1984.IdBook
        );
    }

    public void WpierdolDuzoKsiazek() {
        new Book("Dwór Cierni i Róż", "Sarah J. Mass", 1, "Uroboros", true, 1998).Insert();
        new Book("Rok 1984", "George Orwell", 2, "MUZA", false, 2010).Insert();
        new Book("Ojczyna", "R.A. Salvatore", 2, "Forgotten Realms", true, 2011).Insert();
        new Book("Wygnanie", "R.A. Salvatore", 1, "Forgotten Realms", false, 2012).Insert();
        new Book("Nowy Dom", "R.A. Salvatore", 2, "Forgotten Realms", true, 2013).Insert();
        new Book("Nowy Dom", "R.A. Salvatore", 1, "Forgotten Realms", true, 2013).Insert();
    }

    public void WyszukajKsiazki() {
        // Wszystkie
        var books = Book.Find();

        Console.WriteLine("--------- WSZYSTKIE KSIĄŻKI ---------");

        foreach (var book in books) {
            Console.WriteLine(
                "Książka {0} autora {1} znajduję się w bibliotece {2} i jest {3}",
                book.Title,
                book.Author,
                book.LibraryAddress,
                book.IsAvaliable ? "Dostępna" : "Niedostępna"
            );
        }

        Console.WriteLine("-------------------------------------");
        Console.WriteLine("------- Autor: R.A. Salvatore -------");

        var salvatoreBooks = Book.Find(new BookFilterOptions() {
            Author = "R.A. Salvatore"
        });

        foreach (var book in salvatoreBooks) {
            Console.WriteLine(
                "Książka {0} autora {1} znajduję się w bibliotece {2} i jest {3}",
                book.Title,
                book.Author,
                book.LibraryAddress,
                book.IsAvaliable ? "Dostępna" : "Niedostępna"
            );
        }

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("-- Książki wydane po 2000 w bibliotece 1 --");

        var library1 = Book.Find(new BookFilterOptions() {
            Year = "2",
            IdLibrary = 1
        });

        foreach (var book in library1) {
            Console.WriteLine(
                "Książka {0} autora {1} wydana w {4} znajduję się w bibliotece {2} i jest {3}",
                book.Title,
                book.Author,
                book.LibraryAddress,
                book.IsAvaliable ? "Dostępna" : "Niedostępna",
                book.Year
            );
        }
    }

    public void ListujBiblioteki() {
        // No to sobie podrzucicie do listy rozwijanej. Hai!
        var libraries = Library.List();

        // Sortowanie gotowej listy.
        var sorted = 
            from library in libraries
            orderby library.IdLibrary descending
            select library;

        Console.WriteLine("Nieposortowane:");
        foreach (var library in libraries) {
            Console.WriteLine("Biblioteka {0}: {1}", library.IdLibrary, library.Address);
        }
        Console.WriteLine();
        Console.WriteLine("Posortowane:");
        foreach (var library in sorted) {
            Console.WriteLine("Biblioteka {0}: {1}", library.IdLibrary, library.Address);
        }
    }
}
