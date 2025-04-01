namespace BookWebCatalog.Data.Models
{
	public class BookPublisher
	{
		public BookPublisher()
		{

		}
		public BookPublisher(int bookId, int publisherId)
		{
			this.BookId = bookId;
			this.PublisherId = publisherId;
		}
		public int BookId { get; set; }
		public Book Book { get; set; }
		public int PublisherId { get; set; }
		public Publisher Publisher { get; set; }
	}
}
