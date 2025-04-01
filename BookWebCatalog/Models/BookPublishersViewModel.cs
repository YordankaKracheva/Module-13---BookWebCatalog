using BookWebCatalog.Data.Models;

namespace BookWebCatalog.Models
{
	public class BookPublisherViewModel
	{
		public int BookId { get; set; }
		public int PublisherId { get; set; }

		public List<Book> Books { get; set; } = new List<Book>();
		public List<Publisher> Publishers { get; set; } = new List<Publisher>();
	}
}
