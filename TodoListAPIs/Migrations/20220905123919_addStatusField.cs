using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoListAPIs.Migrations
{
    public partial class addStatusField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "SubLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "MainLists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubLists");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MainLists");
        }
    }
}
