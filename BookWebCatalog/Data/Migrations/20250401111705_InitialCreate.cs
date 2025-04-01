using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWebCatalog.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Years = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfReleasing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookPublishers",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPublishers", x => new { x.BookId, x.PublisherId });
                    table.ForeignKey(
                        name: "FK_BookPublishers_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPublishers_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2d7ef80e-27b9-406c-98d4-63a32a6ad7ef", 0, "ada69a6d-3ed6-45cb-8b89-d5da1c0e72c8", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAELJbTqG7SEIHFys0yIlHDCczzKr96ZZKvWwBHMqVny0V36Dh03pTwBZ2H2kUXkVluQ==", null, false, "ead874ff-6219-4dc1-be93-68f11fcf0121", false, "test@softuni.bg" },
                    { "836eefd4-9aa7-4104-98fb-425ff738918e", 0, "bc7b41b3-552e-4612-9e4a-c60bf24b9c47", "admin@mail.com", false, false, null, "admin@mail.com", "admin@mail.com", "AQAAAAEAACcQAAAAEGgQkCDbohSIOqn3zwf7ICQrD9e3gxW6atpyGWk9+7QopJRq4R3UX2WbY+eWIKwoTA==", null, false, "e80acfbd-2b3e-4d0f-af4c-1121254896f8", false, "admin@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Years" },
                values: new object[,]
                {
                    { 1, "1965-07-31", "J.K.", "Rowling", 26 },
                    { 2, "1903-06-25", "George", "Orwell", 17 },
                    { 3, "1939-11-18", "Margaret", "Atwood", 54 },
                    { 4, "1949-01-12", "Haruki", "Murakami", 40 },
                    { 5, "1890-09-15", "Agatha", "Christie", 66 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "action" },
                    { 2, "comedy" },
                    { 3, "mystery" },
                    { 4, "thriller" },
                    { 5, "drama" },
                    { 6, "romance" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Scholastic Corporation" },
                    { 2, "Houghton Mifflin Harcourt" },
                    { 3, "Penguin Random House" },
                    { 4, "HarperCollins" },
                    { 5, "Simon & Schuster" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorID", "DateOfReleasing", "GenreID", "Rating", "Title", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "1997-06-26", 5, 4.7999999999999998, "Harry Potter and the Philosopher's Stone", "2d7ef80e-27b9-406c-98d4-63a32a6ad7ef" },
                    { 2, 2, "1949-06-08", 4, 4.7000000000000002, "1984", "2d7ef80e-27b9-406c-98d4-63a32a6ad7ef" },
                    { 3, 3, "1985-04-17", 5, 4.5999999999999996, "The Handmaid's Tale", "2d7ef80e-27b9-406c-98d4-63a32a6ad7ef" },
                    { 4, 4, "1987-09-04", 6, 4.5, "Norwegian Wood", "2d7ef80e-27b9-406c-98d4-63a32a6ad7ef" },
                    { 5, 5, "1934-01-01", 3, 4.4000000000000004, "Murder on the Orient Express", "2d7ef80e-27b9-406c-98d4-63a32a6ad7ef" }
                });

            migrationBuilder.InsertData(
                table: "BookPublishers",
                columns: new[] { "BookId", "PublisherId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 2 },
                    { 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPublishers_PublisherId",
                table: "BookPublishers",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreID",
                table: "Books",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserID",
                table: "Books",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPublishers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "836eefd4-9aa7-4104-98fb-425ff738918e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d7ef80e-27b9-406c-98d4-63a32a6ad7ef");
        }
    }
}
