using BookWebCatalog.Data;
using BookWebCatalog.Data.Models;
using BookWebCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using SQLitePCL;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using static BookWebCatalog.Data.Models.Genre;
using static BookWebCatalog.Common.AdminUser;
using Microsoft.AspNetCore.Authorization;
using BookWebCatalog.Data.Models;
using BookWebCatalog.Models;
using static System.Reflection.Metadata.BlobBuilder;
using System.Threading.Tasks;

namespace BookWebCatalog.Controllers
{
	public class BookController : Controller
	{
		private readonly BooksWebCatalogAppDbContext context;
		public BookController(BooksWebCatalogAppDbContext _context)
		{
			this.context = _context;
		}

		public IActionResult Index()
		{
			var books = context.Books
				.Include(b => b.BookPublishers)
				.ThenInclude(bp => bp.Publisher)
				.Include(b => b.Author)
				.Include(b => b.Genre)
				.OrderBy(x => x.Id)
				.ToList();
            return View(books);
		}

		public IActionResult Search(string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
			{
				ViewData["SearchTerm"] = "All Genres"; // You can customize this as needed.
				var allBooks = context.Books
				.Include(b => b.BookPublishers)
				.ThenInclude(bp => bp.Publisher)
				.Include(b => b.Author)
				.Include(b => b.Genre)
				.OrderBy(x => x.Id)
				.ToList();
				return View(allBooks); // Show all books if no genre is provided.
			}

			// Otherwise, filter by genre.
			var booksByGenre = context.Books
				.Include(b => b.BookPublishers)
				.ThenInclude(bp => bp.Publisher)
				.Include(b => b.Author)
				.Include(b => b.Genre)
				.Where(b => b.Genre.Name == searchTerm)
				.OrderBy(b => b.Title)
				.ToList();

			// Pass the search term to the view for display purposes (e.g., to show the current search term).
			ViewData["SearchTerm"] = searchTerm;

			// Return the filtered list to the view.
			return View(booksByGenre);
		}
        //public List<BookPublisher> FillBookPublisher()
        //{
        //    List<BookPublisher> list = context.BookPublishers.ToList();
        //    foreach (var item in list)
        //    {
        //        Book book = OneBook(item.BookId);
        //        item.Book = book;
        //        Publisher publisher = OnePublisher(item.PublisherId);
        //        item.Publisher = publisher;
        //    }
        //    return list;
        //}
        //public Book OneBook(int id)
        //{
        //    List<Book> books = context.Books.ToList();
        //    foreach (var item in books)
        //    {
        //        if (item.Id == id)
        //        {
        //            return item;
        //        }
        //    }
        //    return null!;
        //}
        //public Publisher OnePublisher(int id)
        //{
        //    List<Publisher> publishers = context.Publishers.ToList();
        //    foreach (var item in publishers)
        //    {
        //        if (item.Id == id)
        //        {
        //            return item;
        //        }
        //    }
        //    return null!;
        //}

        [HttpGet]
		[Authorize(Roles = AdminRoleName)]
		public async Task<IActionResult> Create()
		{
			var publishers = await context.Publishers.ToListAsync();
			var genres = await context.Genres.ToListAsync();
			var authors = await context.Authors.ToListAsync();

			var viewModel = new BookCreateViewModel
			{
				Publishers = publishers,
				Genres = genres,
				Authors = authors
			};


			
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(BookCreateViewModel book)
		{
			if (!ModelState.IsValid)
			{
				return View(book);
			}
			
			var bookNew = new Book(book.Title, book.DateOfReleasing, book.Rating, book.AuthorID, book.GenreID)
			{
				UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)
			};

            var bookPublisher = new BookPublisher
            {
                BookId = bookNew.Id,
                PublisherId = book.PublisherID
            };
            
            context.BookPublishers.Add(bookPublisher);
            await context.Books.AddAsync(bookNew);
			await context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = AdminRoleName)]
		public async Task<IActionResult> Edit(int id)
		{
			var book = await context.Books
				.FirstOrDefaultAsync(b => b.Id == id);

			if (book == null)
			{
				return RedirectToAction("Index");
			}

			var viewModel = new BookCreateViewModel
			{
				Title = book.Title,
				DateOfReleasing = book.DateOfReleasing,
				Rating = book.Rating,
				GenreID = book.GenreID,
				AuthorID = book.AuthorID,
				Publishers = await context.Publishers.ToListAsync(),
				Genres = await context.Genres.ToListAsync(),
				Authors = await context.Authors.ToListAsync()
			};

			ViewData["BookId"] = book.Id;

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, BookCreateViewModel book)
		{
			var existingBook = await context.Books.FindAsync(id);

			if (existingBook == null)
			{
				return RedirectToAction("Index");
			}

			if (!ModelState.IsValid)
			{
				ViewData["BookId"] = existingBook.Id;
				return View(book);
			}

			existingBook.Title = book.Title;
			existingBook.DateOfReleasing = book.DateOfReleasing;
			existingBook.Rating = book.Rating;
			existingBook.GenreID = book.GenreID;
			existingBook.AuthorID = book.AuthorID;

			await context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = AdminRoleName)]
		public async Task<IActionResult> Delete(int id)
		{
			var book = await context.Books.FindAsync(id);

			if (book == null)
			{
				return RedirectToAction("Index");
			}
			context.Books.Remove(book);
			await context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}