using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Platform.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedLessons_Courses_CourseID",
                table: "CompletedLessons");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "CompletedLessons",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedLessons_CourseID",
                table: "CompletedLessons",
                newName: "IX_CompletedLessons_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedLessons_Courses_CourseId",
                table: "CompletedLessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedLessons_Courses_CourseId",
                table: "CompletedLessons");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CompletedLessons",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedLessons_CourseId",
                table: "CompletedLessons",
                newName: "IX_CompletedLessons_CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedLessons_Courses_CourseID",
                table: "CompletedLessons",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
