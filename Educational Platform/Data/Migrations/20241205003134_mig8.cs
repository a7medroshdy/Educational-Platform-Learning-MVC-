using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Platform.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "CompletedLessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLessons_CourseID",
                table: "CompletedLessons",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedLessons_Courses_CourseID",
                table: "CompletedLessons",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedLessons_Courses_CourseID",
                table: "CompletedLessons");

            migrationBuilder.DropIndex(
                name: "IX_CompletedLessons_CourseID",
                table: "CompletedLessons");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "CompletedLessons");
        }
    }
}
