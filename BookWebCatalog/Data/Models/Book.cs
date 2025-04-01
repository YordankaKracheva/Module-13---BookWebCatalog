using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookWebCatalog.Data.Models
{
	public class Book
	{
		public Book()
		{

		}
		public Book(string title, string dateOfReleasing,
			double rating, int authorId, int genreId)
		{
			this.Title = title;
			this.DateOfReleasing = dateOfReleasing;
			this.Rating = rating;
			this.GenreID = genreId;
			this.AuthorID = authorId;	
		}
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string DateOfReleasing { get; set; }
		public double Rating { get; set; }
		public int GenreID { get; set; }
		public Genre Genre { get; set; }
		public int AuthorID { get; set; }
		public Author Author { get; set; }
		public ICollection<BookPublisher> BookPublishers { get; set; }

		[Required]
		public string UserID { get; set; } = null!;
		public IdentityUser User { get; set; } = null!;

		public override string ToString()
		{
			return $"{this.Title} - {this.DateOfReleasing}, {this.Rating}/10 {this.GenreID} {this.AuthorID}.";
		}
	}
}
