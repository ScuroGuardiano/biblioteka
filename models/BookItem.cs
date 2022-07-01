namespace Booker.MVVM.Model
{
    internal class BookItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public string Library { get; set; }
        public bool isAvailable { get; set; }
        public string Publisher { get; set; }

        public BookItem(string title, string author, string year,
            string library, bool IsAvailable, string publisher)
        {
            Title = title;
            Author = author;
            Year = year;
            Library = library;
            isAvailable = IsAvailable;
            Publisher = publisher;
        }


        public override string ToString()
        {
            return $"Title: {Title}\nAuthor: {Author}\nYear: {Year}\n" +
                $"Library: {Library}\nisAvailable: {isAvailable}\nPublisher: {Publisher}";
        }
    }
}