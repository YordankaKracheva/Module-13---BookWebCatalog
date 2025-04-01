using System.ComponentModel.DataAnnotations;

namespace BookWebCatalog.Data.Models
{
	public class Author
	{
		public Author()
		{

		}
		public Author(string firstName, string lastName, int years, string DOB)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Years = years;
			this.DateOfBirth = DOB;
		}
		[Key]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public int Years { get; set; }
		public string DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; }

        public override string ToString()
		{
			return $"{this.FirstName} {this.LastName} {this.Years} {this.DateOfBirth}";
		}
	}
}
