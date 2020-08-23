using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JML.DataAccess.Context.Migrations
{
    public partial class LiteratureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Literature",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Literature", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Literature");
        }
    }
}
