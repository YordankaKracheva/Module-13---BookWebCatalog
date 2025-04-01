using BookWebCatalog.Data.Models;

namespace BookWebCatalog.Models
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string DateOfReleasing { get; set; }
        public double Rating { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public int AuthorID { get; set; }
        public int PublisherID { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Publisher> Publishers { get; set; } = new List<Publisher>();
    }
}
