using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("1c60895f-e483-4735-96aa-32abe393fe44"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("820c7248-1b41-455f-b1b8-ca77aa9bca18"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("8d95078d-d92f-4162-9164-54e3a240c980"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("01d9f074-a099-4ce4-ac1b-9fa82f0495b7"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("2dbddf3a-4a0b-433d-8fbd-97e7eea1b400"));

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                defaultValue: "/images/1.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldNullable: true,
                oldDefaultValue: "~/images/no-image.png");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CinemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("632baff1-1585-4cb7-b26c-e9baf148ad2a"), "Sofia", "Cinema city" },
                    { new Guid("7db2a48e-aeb0-4aed-90d6-0e7b5d5055f5"), "Plovdiv", "Cinema city" },
                    { new Guid("fe0708ab-9284-4046-8eab-52e3c888b674"), "Varna", "Cinemax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("c920c669-8ff4-4528-8b35-156a27001b95"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" },
                    { new Guid("f0266258-853f-4f17-81d8-c8591771759d"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaId",
                table: "Tickets",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MovieId",
                table: "Tickets",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("632baff1-1585-4cb7-b26c-e9baf148ad2a"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("7db2a48e-aeb0-4aed-90d6-0e7b5d5055f5"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("fe0708ab-9284-4046-8eab-52e3c888b674"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("c920c669-8ff4-4528-8b35-156a27001b95"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("f0266258-853f-4f17-81d8-c8591771759d"));

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                defaultValue: "~/images/no-image.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldNullable: true,
                oldDefaultValue: "/images/1.jpg");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("1c60895f-e483-4735-96aa-32abe393fe44"), "Plovdiv", "Cinema city" },
                    { new Guid("820c7248-1b41-455f-b1b8-ca77aa9bca18"), "Sofia", "Cinema city" },
                    { new Guid("8d95078d-d92f-4162-9164-54e3a240c980"), "Varna", "Cinemax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("01d9f074-a099-4ce4-ac1b-9fa82f0495b7"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" },
                    { new Guid("2dbddf3a-4a0b-433d-8fbd-97e7eea1b400"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" }
                });
        }
    }
}
