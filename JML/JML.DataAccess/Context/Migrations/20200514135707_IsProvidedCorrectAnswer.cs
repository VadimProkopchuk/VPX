using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JML.DataAccess.Context.Migrations
{
    public partial class IsProvidedCorrectAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeTestQuestions_AnswerTemplates_SelectedAnswerId",
                table: "KnowledgeTestQuestions");

            migrationBuilder.DropIndex(
                name: "IX_KnowledgeTestQuestions_SelectedAnswerId",
                table: "KnowledgeTestQuestions");

            migrationBuilder.DropColumn(
                name: "SelectedAnswerId",
                table: "KnowledgeTestQuestions");

            migrationBuilder.AlterColumn<Guid>(
                name: "KnowledgeTestId",
                table: "KnowledgeTestQuestions",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsProvidedCorrectAnswer",
                table: "KnowledgeTestQuestions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProvidedCorrectAnswer",
                table: "KnowledgeTestQuestions");

            migrationBuilder.AlterColumn<Guid>(
                name: "KnowledgeTestId",
                table: "KnowledgeTestQuestions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SelectedAnswerId",
                table: "KnowledgeTestQuestions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeTestQuestions_SelectedAnswerId",
                table: "KnowledgeTestQuestions",
                column: "SelectedAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeTestQuestions_AnswerTemplates_SelectedAnswerId",
                table: "KnowledgeTestQuestions",
                column: "SelectedAnswerId",
                principalTable: "AnswerTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
