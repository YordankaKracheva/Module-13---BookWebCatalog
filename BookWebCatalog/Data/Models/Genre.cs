using System.ComponentModel.DataAnnotations;

namespace BookWebCatalog.Data.Models
{
	public class Genre
	{
		public Genre()
		{

		}
		public Genre(string name)
		{
			this.Name = name;
		}
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<Book> Books { get; set; }
	}
}
