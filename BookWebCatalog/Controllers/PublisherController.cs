using BookWebCatalog.Data;
using BookWebCatalog.Data.Models;
using BookWebCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookWebCatalog.Common.AdminUser;

namespace BooksWebCatalog.Controllers
{
    public class PublisherController : Controller
    {
        private readonly BooksWebCatalogAppDbContext context;
        public PublisherController(BooksWebCatalogAppDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {
            var publishers = context.Publishers.OrderBy(x => x.Id).ToList();
            return View(publishers);
        }
        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Create()
        {

            return View();
        }
		[HttpPost]
		public IActionResult Create(PublisherViewModel publisher)
		{
			if (!ModelState.IsValid)
			{
				return View(publisher);
			}

            Publisher publisherNew = new Publisher()
			{
				Name = publisher.Name
			};

			context.Publishers.Add(publisherNew);
			context.SaveChanges();
			return RedirectToAction("Index", "Publisher");
		}
		[HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Edit(int id)
		{

			var publisher = context.Publishers.Find(id);
			if (publisher == null)
			{
				return RedirectToAction("Index", "Publisher");
			}
            var publisherViewModel = new PublisherViewModel()
			{
				Name = publisher.Name
			};
			ViewData["PublisherId"] = publisher.Id;
			return View(publisherViewModel);
		}
		[HttpPost]
		public IActionResult Edit(int id, PublisherViewModel publisher)
		{
			var publishers = context.Publishers.Find(id);
			if (publisher == null)
			{
				return RedirectToAction("Index", "Publisher");
			}

			if (!ModelState.IsValid)
			{
				ViewData["PublisherId"] = publishers.Id;

				return View(publisher);
			}
            publishers.Name = publisher.Name;
			context.SaveChanges();

			return RedirectToAction("Index", "Publisher");
		}
		[HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Delete(int id)
		{
			var publisher = context.Publishers.Find(id);
			if (publisher == null)
			{
				return RedirectToAction("Index", "Publisher");
			}
			context.Publishers.Remove(publisher);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Publisher");
		}
	}
}
