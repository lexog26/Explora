using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Explora.DataLayer.Migrations
{
    public partial class ExploraTotem_Table_Created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "totems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    last_timestamp = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(maxLength: 100, nullable: true),
                    version = table.Column<int>(nullable: false, defaultValue: 1),
                    platform = table.Column<int>(nullable: false, defaultValue: 1),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    image_url = table.Column<string>(maxLength: 255, nullable: true),
                    file_url = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_totems", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "totems");
        }
    }
}
