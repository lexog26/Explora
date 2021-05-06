using Microsoft.EntityFrameworkCore.Migrations;

namespace Explora.DataLayer.Migrations
{
    public partial class ExploraFile_VersionColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "version",
                table: "files",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "version",
                table: "files");
        }
    }
}
