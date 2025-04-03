using BookWebCatalog.Data;
using BookWebCatalog.Data.Models;
using BookWebCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookWebCatalog.Common.AdminUser;

namespace BookWebCatalog.Controllers
{
    public class GenreController : Controller
    {
        private readonly BooksWebCatalogAppDbContext context;
        public GenreController(BooksWebCatalogAppDbContext _context)
        {
            this.context = _context;
        }
		public IActionResult Index()
		{
			var genres = context.Genres.OrderBy(x => x.Id).ToList();
			return View(genres);
		}

		[HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Create()
        {

            return View();
        }
		[HttpPost]
		public IActionResult Create(GenreViewModel genre)
		{
			if (!ModelState.IsValid)
			{
				return View(genre);
			}
			
			Genre genreNew = new Genre() 
			{ 
				Name = genre.Name
			};

			context.Genres.Add(genreNew);
			context.SaveChanges();
			return RedirectToAction("Index", "Genre");
		}
		[HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Edit(int id)
		{
			var genre = context.Genres.Find(id);
			if (genre == null)
			{
				return RedirectToAction("Index", "Genre");
			}
			var genreViewModel = new GenreViewModel()
			{
				Name = genre.Name
			};
			ViewData["GenreId"] = genre.Id;
			return View(genreViewModel);
		}
		[HttpPost]
		public IActionResult Edit(int id, GenreViewModel genre)
		{
			var genres = context.Genres.Find(id);
			if (genres == null)
			{
				return RedirectToAction("Index", "Genre");
			}

			if (!ModelState.IsValid)
			{
				ViewData["GenreId"] = genres.Id;
				
				return View(genre);
			}
			genres.Name = genre.Name;

			context.SaveChanges();

			return RedirectToAction("Index", "Genre");
		}
		[HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Delete(int id)
		{
			var genre = context.Genres.Find(id);
			if (genre == null)
			{
				return RedirectToAction("Index", "Genre");
			}
			context.Genres.Remove(genre);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Genre");
		}
	}
}
