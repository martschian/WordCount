using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGoal.API.Migrations
{
    public partial class NullableProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Project_ProjectId",
                table: "Note");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Note",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Project_ProjectId",
                table: "Note",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Project_ProjectId",
                table: "Note");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Project_ProjectId",
                table: "Note",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
