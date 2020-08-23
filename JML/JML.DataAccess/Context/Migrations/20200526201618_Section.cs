using Microsoft.EntityFrameworkCore.Migrations;

namespace JML.DataAccess.Context.Migrations
{
    public partial class Section : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Lectures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Lectures");
        }
    }
}
