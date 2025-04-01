using BookWebCatalog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static BookWebCatalog.Common.AdminUser;

namespace BookWebCatalog.Data
{
    public class BooksWebCatalogAppDbContext : IdentityDbContext
    {
        public BooksWebCatalogAppDbContext(DbContextOptions<BooksWebCatalogAppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookPublisher> BookPublishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public IdentityUser AdminUser { get; set; }
        private IdentityUser TestUser { get; set; }
        private List<Genre> GenresList { get; set; }
        private List<Author> AuthorsList { get; set; }
        private List<Publisher> PublishersList { get; set; }
        private List<BookPublisher> BookPublishersList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //да няма повтарящи се редове в свързващата таблица
            modelBuilder.Entity<BookPublisher>().HasKey(fa => new
            { fa.BookId, fa.PublisherId });

            SeedUsers(modelBuilder);


            GenresList = SeedGenres();
            modelBuilder.Entity<Genre>()
                .HasData(GenresList);

            AuthorsList = SeedAuthors();
            modelBuilder.Entity<Author>()
                .HasData(AuthorsList);

            PublishersList = SeedPublishers();
            modelBuilder.Entity<Publisher>()
                .HasData(PublishersList);


            modelBuilder.Entity<Book>()
                .HasData(new Book()
                {
                    Id = 1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    DateOfReleasing = "1997-06-26",
                    Rating = 4.8,
                    GenreID = GenresList[4].Id,
                    AuthorID = AuthorsList[0].Id,
                    UserID = TestUser.Id
                },
                new Book()
                {
                    Id = 2,
                    Title = "1984",
                    DateOfReleasing = "1949-06-08",
                    Rating = 4.7,
                    GenreID = GenresList[3].Id,
                    AuthorID = AuthorsList[1].Id,
                    UserID = TestUser.Id
                },
                new Book()
                {
                    Id = 3,
                    Title = "The Handmaid's Tale",
                    DateOfReleasing = "1985-04-17",
                    Rating = 4.6,
                    GenreID = GenresList[4].Id,
                    AuthorID = AuthorsList[2].Id,
                    UserID = TestUser.Id
                },
                new Book()
                {
                    Id = 4,
                    Title = "Norwegian Wood",
                    DateOfReleasing = "1987-09-04",
                    Rating = 4.5,
                    GenreID = GenresList[5].Id,
                    AuthorID = AuthorsList[3].Id,
                    UserID = TestUser.Id
                },
                new Book()
                {
                    Id = 5,
                    Title = "Murder on the Orient Express",
                    DateOfReleasing = "1934-01-01",
                    Rating = 4.4,
                    GenreID = GenresList[2].Id,
                    AuthorID = AuthorsList[4].Id,
                    UserID = TestUser.Id
                }
            );

            BookPublishersList = SeedBookPublishers();
            modelBuilder.Entity<BookPublisher>()
                .HasData(BookPublishersList);

            base.OnModelCreating(modelBuilder);
        }
        private void SeedUsers(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            TestUser = new IdentityUser()
            {
                UserName = "test@softuni.bg",
                NormalizedUserName = "TEST@SOFTUNI.BG",
            };

            TestUser.PasswordHash = hasher.HashPassword(TestUser, "softuni");

            AdminUser = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = AdminEmail,
                NormalizedEmail = AdminEmail,
                UserName = AdminEmail,
                NormalizedUserName = AdminEmail,
            };

            AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "admin");

            modelBuilder.Entity<IdentityUser>()
                .HasData(TestUser, AdminUser);
        }

        private List<Genre> SeedGenres()
        {
            GenresList = new List<Genre>
        {
            new Genre() { Id = 1, Name = "action" },
            new Genre() { Id = 2, Name = "comedy" },
            new Genre() { Id = 3, Name = "mystery" },
            new Genre() { Id = 4, Name = "thriller" },
            new Genre() { Id = 5, Name = "drama" },
            new Genre() { Id = 6, Name = "romance" }
        };

            return GenresList;
        }
        private List<Publisher> SeedPublishers()
        {
            PublishersList = new List<Publisher>
            {
                new Publisher() { Id = 1, Name = "Scholastic Corporation" },
                new Publisher() { Id = 2, Name = "Houghton Mifflin Harcourt" },
                new Publisher() { Id = 3, Name = "Penguin Random House" },
                new Publisher() { Id = 4, Name = "HarperCollins" },
                new Publisher() { Id = 5, Name = "Simon & Schuster" }
            };

            return PublishersList;
        }
        private List<Author> SeedAuthors()
        {
            AuthorsList = new List<Author>
            {
                new Author() { Id = 1, FirstName = "J.K.", LastName = "Rowling", Years = 26, DateOfBirth = "1965-07-31" },
                new Author() { Id = 2, FirstName = "George", LastName = "Orwell", Years = 17, DateOfBirth = "1903-06-25" },
                new Author() { Id = 3, FirstName = "Margaret", LastName = "Atwood", Years = 54, DateOfBirth = "1939-11-18" },
                new Author() { Id = 4, FirstName = "Haruki", LastName = "Murakami", Years = 40, DateOfBirth = "1949-01-12" },
                new Author() { Id = 5, FirstName = "Agatha", LastName = "Christie", Years = 66, DateOfBirth = "1890-09-15" },
            };

            return AuthorsList;
        }
        private List<BookPublisher> SeedBookPublishers()
        {
            BookPublishersList = new List<BookPublisher>
            {
                new BookPublisher() { BookId = 1, PublisherId = 1 },
                new BookPublisher() { BookId = 2, PublisherId = 3 },
                new BookPublisher() { BookId = 3, PublisherId = 4 },
                new BookPublisher() { BookId = 4, PublisherId = 2 },
                new BookPublisher() { BookId = 5, PublisherId = 5 }
            };
            return BookPublishersList;
        }
    }
}
