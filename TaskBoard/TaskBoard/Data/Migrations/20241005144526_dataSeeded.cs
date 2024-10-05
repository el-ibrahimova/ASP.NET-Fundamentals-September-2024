using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class dataSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Board identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Board name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Task identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Task Title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Task Description"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of creation"),
                    BoardId = table.Column<int>(type: "int", nullable: true, comment: "Board identifier"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Application user identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Board Tasks");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "122c2d73-9399-4f10-b9cd-ba65fde063d6", 0, "5cacf670-4320-437c-badb-7406ef72e3ad", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAIAAYagAAAAEEKcanUH0oK5T8fLXd6idLWpvJJyhhnXyVZfR8LRpk5vsMsxCyFh5Eryf1C98Y3qOA==", null, false, "d5391d86-891c-4206-9ff4-fb060a1ca44c", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 3, 19, 17, 45, 24, 737, DateTimeKind.Local).AddTicks(7743), "Implement better styling for all public pages", "122c2d73-9399-4f10-b9cd-ba65fde063d6", "Improve CSS styles" },
                    { 2, 1, new DateTime(2024, 5, 5, 17, 45, 24, 737, DateTimeKind.Local).AddTicks(7813), "Create Android client app for the TaskBoard RESTful API", "122c2d73-9399-4f10-b9cd-ba65fde063d6", "Android Client App" },
                    { 3, 2, new DateTime(2024, 9, 5, 17, 45, 24, 737, DateTimeKind.Local).AddTicks(7823), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "122c2d73-9399-4f10-b9cd-ba65fde063d6", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 10, 5, 17, 45, 24, 737, DateTimeKind.Local).AddTicks(7830), "Implement [Create Task] page for adding new tasks", "122c2d73-9399-4f10-b9cd-ba65fde063d6", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "122c2d73-9399-4f10-b9cd-ba65fde063d6");
        }
    }
}
