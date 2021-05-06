using Microsoft.EntityFrameworkCore.Migrations;

namespace Explora.DataLayer.Migrations
{
    public partial class ExploraFile_DeletedColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "files",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "files");
        }
    }
}
