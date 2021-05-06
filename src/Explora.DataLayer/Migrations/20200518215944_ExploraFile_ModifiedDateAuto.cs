using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Explora.DataLayer.Migrations
{
    public partial class ExploraFile_ModifiedDateAuto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "files",
                newName: "modified_date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_date",
                table: "files",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "files",
                newName: "ModifiedDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "files",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);
        }
    }
}
