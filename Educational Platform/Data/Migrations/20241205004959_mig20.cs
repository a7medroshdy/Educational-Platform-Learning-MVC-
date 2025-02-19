using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Platform.Migrations
{
    /// <inheritdoc />
    public partial class mig20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CompletedLessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLessons_CourseId",
                table: "CompletedLessons",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedLessons_Courses_CourseId",
                table: "CompletedLessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedLessons_Courses_CourseId",
                table: "CompletedLessons");

            migrationBuilder.DropIndex(
                name: "IX_CompletedLessons_CourseId",
                table: "CompletedLessons");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CompletedLessons");
        }
    }
}
