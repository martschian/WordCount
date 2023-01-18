using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGoal.API.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1, "adam@handoq.com", "Adam Handoq" });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Description", "Title", "UserId" },
                values: new object[] { 1, "What is says on the tin, baybeeee!", "The Great American Novel", 1 });

            migrationBuilder.InsertData(
                table: "LogEntry",
                columns: new[] { "Id", "NumberOfMinutes", "ProjectId", "Timestamp", "WordCount" },
                values: new object[] { 1, 10, 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 100 });

            migrationBuilder.InsertData(
                table: "Note",
                columns: new[] { "Id", "Description", "NoteText", "ProjectId", "Title" },
                values: new object[] { 1, "Short LGBTQIA+ meet-cute ideas", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. At consectetur lorem donec massa.", 1, "Rainbow Connection" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogEntry",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
