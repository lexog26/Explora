using Microsoft.EntityFrameworkCore.Migrations;

namespace Explora.DataLayer.Migrations
{
    public partial class ExploraFile_Image_Url_Column_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "files",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "files");
        }
    }
}
