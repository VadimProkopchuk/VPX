using Microsoft.EntityFrameworkCore.Migrations;

namespace JML.DataAccess.Context.Migrations
{
    public partial class Avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarBase64",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarBase64",
                table: "Users");
        }
    }
}
