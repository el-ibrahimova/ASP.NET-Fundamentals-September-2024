using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("549e7d52-33b5-4623-8988-8daf7e3595e3"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("62467210-37bd-4238-b0a7-ecfacc0f1658"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("629aca8e-e7bb-4580-bb93-3ca987fb0f58"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("2288d562-0f41-48c4-983f-f958d388d6d5"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("89fb1f7c-4a54-451d-b6cd-f8bf20f018bd"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                defaultValue: "~/images/no-image.png");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("0e7edf73-f23e-431f-a151-f035a0855d54"), "Plovdiv", "Cinema city" },
                    { new Guid("a5e2d886-861e-406a-8fab-47ff95339a9c"), "Varna", "Cinemax" },
                    { new Guid("fab1936f-9f61-4f8f-b987-e3929c51933d"), "Sofia", "Cinema city" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("3e8888ef-9780-4a09-9a2f-f32171c47a65"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" },
                    { new Guid("cf4b0522-834c-419c-b030-109815a02d3d"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("0e7edf73-f23e-431f-a151-f035a0855d54"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("a5e2d886-861e-406a-8fab-47ff95339a9c"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("fab1936f-9f61-4f8f-b987-e3929c51933d"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("3e8888ef-9780-4a09-9a2f-f32171c47a65"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("cf4b0522-834c-419c-b030-109815a02d3d"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("549e7d52-33b5-4623-8988-8daf7e3595e3"), "Varna", "Cinemax" },
                    { new Guid("62467210-37bd-4238-b0a7-ecfacc0f1658"), "Plovdiv", "Cinema city" },
                    { new Guid("629aca8e-e7bb-4580-bb93-3ca987fb0f58"), "Sofia", "Cinema city" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("2288d562-0f41-48c4-983f-f958d388d6d5"), "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron.", "Peter Jackson", 178, "Fantasy", new DateTime(2001, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lord of the Rings" },
                    { new Guid("89fb1f7c-4a54-451d-b6cd-f8bf20f018bd"), "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete.", "Mike Newel", 157, "Fantasy", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" }
                });
        }
    }
}
