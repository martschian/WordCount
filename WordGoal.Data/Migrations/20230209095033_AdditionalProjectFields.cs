using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGoal.API.Migrations
{
    public partial class AdditionalProjectFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DueDate",
                table: "Project",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "WordGoalDaily",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WordGoalTotal",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WordGoalDaily",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WordGoalTotal",
                table: "Project");
        }
    }
}
