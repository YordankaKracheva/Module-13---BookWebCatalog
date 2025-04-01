using System.ComponentModel.DataAnnotations;

namespace BookWebCatalog.Data.Models
{
	public class Publisher
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		
        public ICollection<BookPublisher> BookPublishers { get; set; }
    }
}
