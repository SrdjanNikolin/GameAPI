using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesApi.Migrations
{
    public partial class UpdateGameGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "Games",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
