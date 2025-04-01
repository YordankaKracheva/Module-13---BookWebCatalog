using BookWebCatalog.Data;
using BookWebCatalog.Data.Models;
using BookWebCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookWebCatalog.Common.AdminUser;

namespace BookWebCatalog.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BooksWebCatalogAppDbContext context;
        public AuthorController(BooksWebCatalogAppDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {
            var authors = context.Authors.OrderBy(x => x.Id).ToList();
            return View(authors);
        }
        [HttpGet]
		[Authorize(Roles = AdminRoleName)]
        public IActionResult Create()
        {

            return View();
        }
		[HttpPost]
		public IActionResult Create(AuthorViewModel author)
		{
			if (!ModelState.IsValid)
			{
				return View(author);
			}

			Author authorNew = new Author()
			{
				FirstName = author.FirstName,
				LastName = author.LastName,
				Years = author.Years,
				DateOfBirth = author.DateOfBirth
			};

			context.Authors.Add(authorNew);
			context.SaveChanges();
			return RedirectToAction("Index", "Author");
		}
		[HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Edit(int id)
		{

			var author = context.Authors.Find(id);
			if (author == null)
			{
				return RedirectToAction("Index", "Author");
			}
			var authorViewModel = new AuthorViewModel()
			{
				FirstName = author.FirstName,
				LastName = author.LastName,
				Years = author.Years,
				DateOfBirth = author.DateOfBirth
			};
			ViewData["AuthorId"] = author.Id;
			return View(authorViewModel);
		}
		[HttpPost]
		public IActionResult Edit(int id, AuthorViewModel author)
		{
			var authors = context.Authors.Find(id);
			if (authors == null)
			{
				return RedirectToAction("Index", "Author");
			}

			if (!ModelState.IsValid)
			{
				ViewData["AuthorId"] = authors.Id;

				return View(author);
			}
            authors.FirstName = author.FirstName;
            authors.LastName = author.LastName;
            authors.Years = author.Years;
            authors.DateOfBirth = author.DateOfBirth;
			context.SaveChanges();

			return RedirectToAction("Index", "Author");
		}
		[HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Delete(int id)
		{
			var author = context.Authors.Find(id);
			if (author == null)
			{
				return RedirectToAction("Index", "Author");
			}
			context.Authors.Remove(author);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Author");
		}
	}
}
