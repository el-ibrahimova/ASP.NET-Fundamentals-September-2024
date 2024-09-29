using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteCinemasMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("36e3334f-124a-4ef9-8c27-98f5eca974a3"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("8b40319c-dd70-4f31-85d7-c778bdabb13f"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("ea07dd92-f940-4c32-9f87-2ed955ba81ba"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("0c43616c-5614-4750-9fae-f034af886f5d"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("432e2cbf-4b49-4642-a8b2-3299fdc6190f"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CinemaMovies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("252930dd-ff25-4bed-9adf-c453756cd62f"), "Varna", "Cinemax" },
                    { new Guid("53368f06-46d6-4e8c-87b0-7225c276d1b6"), "Plovdiv", "Cinema city" },
                    { new Guid("d45ab027-51c6-46a3-81d7-023ae1e8b8f3"), "Sofia", "Cinema city" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("8732ff1a-58e3-4c86-b1ce-a5b91caeb35e"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" },
                    { new Guid("9609ffef-a97d-43f6-99b6-11d319ab134a"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("252930dd-ff25-4bed-9adf-c453756cd62f"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("53368f06-46d6-4e8c-87b0-7225c276d1b6"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("d45ab027-51c6-46a3-81d7-023ae1e8b8f3"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("8732ff1a-58e3-4c86-b1ce-a5b91caeb35e"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("9609ffef-a97d-43f6-99b6-11d319ab134a"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CinemaMovies");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("36e3334f-124a-4ef9-8c27-98f5eca974a3"), "Sofia", "Cinema city" },
                    { new Guid("8b40319c-dd70-4f31-85d7-c778bdabb13f"), "Varna", "Cinemax" },
                    { new Guid("ea07dd92-f940-4c32-9f87-2ed955ba81ba"), "Plovdiv", "Cinema city" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("0c43616c-5614-4750-9fae-f034af886f5d"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" },
                    { new Guid("432e2cbf-4b49-4642-a8b2-3299fdc6190f"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" }
                });
        }
    }
}
