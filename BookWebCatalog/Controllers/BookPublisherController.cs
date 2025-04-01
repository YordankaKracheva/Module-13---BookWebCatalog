using BookWebCatalog.Data;
using BookWebCatalog.Data.Models;
using BookWebCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static BookWebCatalog.Common.AdminUser;

namespace BookWebCatalog.Controllers
{
	public class BookPublisherController : Controller
	{
		private readonly BooksWebCatalogAppDbContext context;
		public BookPublisherController(BooksWebCatalogAppDbContext _context)
		{
			this.context = _context;
		}
		public IActionResult Index()
		{
            List<BookPublisher> list = FillBookPublisher().OrderBy(x => x.BookId).ToList();
            return View(list);
		}
        public List<BookPublisher> FillBookPublisher()
        {
            List<BookPublisher> list = context.BookPublishers.ToList();
            foreach (var item in list)
            {
                Book book = OneBook(item.BookId);
                item.Book = book;
                Publisher publisher = OnePublisher(item.PublisherId);
                item.Publisher = publisher;
            }
            return list;
        }
        public Book OneBook(int id)
        {
            List<Book> books = context.Books.ToList();
            foreach (var item in books)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null!;
        }
        public Publisher OnePublisher(int id)
        {
            List<Publisher> publishers = context.Publishers.ToList();
            foreach (var item in publishers)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null!;
        }
        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> Create()
        {
            List<Book> books = await context.Books.ToListAsync();
            List<Publisher> publishers = await context.Publishers.ToListAsync();

            //FIlmCreateViewModel viewModel = new FIlmCreateViewModel();
            BookPublisherViewModel viewModel = new BookPublisherViewModel();
            viewModel.Books = books;
            viewModel.Publishers = publishers;
            return View(viewModel);
        }
		[HttpPost]
		public async Task<IActionResult> Create(BookPublisherViewModel item)
		{
			if (!ModelState.IsValid)
			{
				return View(item);
			}

			BookPublisher bookNew = new BookPublisher(item.BookId, item.PublisherId);

			await context.BookPublishers.AddAsync(bookNew);
			await context.SaveChangesAsync();
			return RedirectToAction("Index", "BookPublisher");
		}
	}
}
