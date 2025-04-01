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
           List<Book> books = FillBooks().OrderBy(x => x.Id).ToList();
            return View(books);
        }
        public List<Book> FillBooks()
        {
            List<Book> books = context.Books.Include(b => b.BookPublishers).ThenInclude(bp => bp.Publisher).ToList();
            foreach (var item in books)
            {
                item.Author = context.Authors.Find(item.AuthorID);
                item.Genre = context.Genres.Find(item.GenreID);
            }
            return books;
        }
        public List<BookPublisher> FillBookPublisher()
        {
            List<BookPublisher> bp = context.BookPublishers.ToList();
            foreach (var item in bp)
            {
                Book book = OneBook(item.BookId);
                item.Book = book;
                Publisher publisher = OnePublisher(item.PublisherId);
                item.Publisher = publisher;
            }
            return bp;
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
            List<Publisher> publishers = await context.Publishers.ToListAsync();
            List<Genre> genres = await context.Genres.ToListAsync();
            List<Author> authors = await context.Authors.ToListAsync();
            BookCreateViewModel viewModel = new BookCreateViewModel();
            viewModel.Publishers = publishers;
            viewModel.Genres = genres;
            viewModel.Authors = authors;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            Book bookNew = new Book(book.Title, book.DateOfReleasing, book.Rating, book.AuthorID, book.GenreID);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bookNew.UserID = userId;
            await context.Books.AddAsync(bookNew);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Book");
        }
        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public IActionResult Delete(int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index", "Book");
            }
            context.Books.Remove(book);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Book");
        }
        [HttpGet]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index", "Book");
            }
            var bookCreateViewModel = new BookCreateViewModel()
            {
                Title = book.Title,
                DateOfReleasing = book.DateOfReleasing,
                Rating = book.Rating,
                GenreID = book.GenreID,
                AuthorID = book.AuthorID
            };
            ViewData["BookId"] = book.Id;
            List<Publisher> publishers = await context.Publishers.ToListAsync();
            List<Genre> genres = await context.Genres.ToListAsync();
            List<Author> authors = await context.Authors.ToListAsync();
            bookCreateViewModel.Publishers = publishers;
            bookCreateViewModel.Genres = genres;
            bookCreateViewModel.Authors = authors;
            return View(bookCreateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookCreateViewModel book)
        {
            var books = context.Books.Find(id);
            if (books == null)
            {
                return RedirectToAction("Index", "Book");
            }
            if (!ModelState.IsValid)
            {
                ViewData["BookId"] = books.Id;
                return View(books);
            }
            books.Title = book.Title;
            books.DateOfReleasing = book.DateOfReleasing;
            books.Rating = book.Rating;
            books.GenreID = book.GenreID;
            books.AuthorID = book.AuthorID;
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Book");
        }
    }
}