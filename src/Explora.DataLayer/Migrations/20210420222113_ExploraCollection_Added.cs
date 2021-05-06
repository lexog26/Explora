using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Explora.DataLayer.Migrations
{
    public partial class ExploraCollection_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "collection_id",
                table: "files",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    last_timestamp = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(maxLength: 100, nullable: true),
                    description = table.Column<string>(maxLength: 250, nullable: true),
                    image_url = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collections", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_files_collection_id",
                table: "files",
                column: "collection_id");

            migrationBuilder.AddForeignKey(
                name: "FK_files_collections_collection_id",
                table: "files",
                column: "collection_id",
                principalTable: "collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_files_collections_collection_id",
                table: "files");

            migrationBuilder.DropTable(
                name: "collections");

            migrationBuilder.DropIndex(
                name: "IX_files_collection_id",
                table: "files");

            migrationBuilder.DropColumn(
                name: "collection_id",
                table: "files");
        }
    }
}
