using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JML.DataAccess.Context.Migrations
{
    public partial class ActiveAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginAt",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveAt",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveAt",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "LoginAt",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
