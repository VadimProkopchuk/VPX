using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JML.DataAccess.Context.Migrations
{
    public partial class user_and_isactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TestTemplates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "KnowledgeTests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeTests_UserId",
                table: "KnowledgeTests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeTests_Users_UserId",
                table: "KnowledgeTests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeTests_Users_UserId",
                table: "KnowledgeTests");

            migrationBuilder.DropIndex(
                name: "IX_KnowledgeTests_UserId",
                table: "KnowledgeTests");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TestTemplates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "KnowledgeTests");
        }
    }
}
