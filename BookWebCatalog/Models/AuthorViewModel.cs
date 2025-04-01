using System.ComponentModel.DataAnnotations;

namespace BookWebCatalog.Models
{
	public class AuthorViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Years { get; set; }
		public string DateOfBirth { get; set; }
	}
}
