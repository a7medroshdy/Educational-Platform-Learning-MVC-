using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Platform.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedLessons_Lessons_LessonId",
                table: "CompletedLessons");

            migrationBuilder.DropColumn(
                name: "CompletedLessons",
                table: "Enrollment");

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "CompletedLessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedLessons_Lessons_LessonId",
                table: "CompletedLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedLessons_Lessons_LessonId",
                table: "CompletedLessons");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "CompletedLessons");

            migrationBuilder.AddColumn<int>(
                name: "CompletedLessons",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedLessons_Lessons_LessonId",
                table: "CompletedLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
