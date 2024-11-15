using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketsAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         /*   migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("32369468-ee2f-4f6d-8cdd-1d79014b1584"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("781aef9e-e2f7-4736-81ad-3c51525de1ec"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("9a9fc8dc-bba3-47ab-820d-f29b1e57aacc"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("2e0b4a1d-62a4-4d74-bf5d-8e7ce1b0f42f"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ed1e24f5-a564-40a3-b327-bbec134eb4bb"));
         */

            migrationBuilder.AddColumn<int>(
                name: "AvailableTickets",
                table: "CinemaMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);

          /*  migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("50a0f596-e923-4aaf-81f5-27f6bb2dc876"), "Varna", "Cinemax" },
                    { new Guid("aa4c26b3-cf30-4fc5-93fc-c83f5c7e9d3f"), "Sofia", "Cinema city" },
                    { new Guid("d040e3ac-76ff-4e8c-9569-6c524740814e"), "Plovdiv", "Cinema city" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("9fe6ed99-335e-461e-9b42-21302b6271a4"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" },
                    { new Guid("a2ad40ec-904b-4581-b40c-012805b0aa4a"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" }
                });*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("50a0f596-e923-4aaf-81f5-27f6bb2dc876"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("aa4c26b3-cf30-4fc5-93fc-c83f5c7e9d3f"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("d040e3ac-76ff-4e8c-9569-6c524740814e"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("9fe6ed99-335e-461e-9b42-21302b6271a4"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a2ad40ec-904b-4581-b40c-012805b0aa4a"));
          */

            migrationBuilder.DropColumn(
                name: "AvailableTickets",
                table: "CinemaMovies");

          /*  migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("32369468-ee2f-4f6d-8cdd-1d79014b1584"), "Varna", "Cinemax" },
                    { new Guid("781aef9e-e2f7-4736-81ad-3c51525de1ec"), "Plovdiv", "Cinema city" },
                    { new Guid("9a9fc8dc-bba3-47ab-820d-f29b1e57aacc"), "Sofia", "Cinema city" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("2e0b4a1d-62a4-4d74-bf5d-8e7ce1b0f42f"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" },
                    { new Guid("ed1e24f5-a564-40a3-b327-bbec134eb4bb"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" }
                });
          */
        }
    }
}
