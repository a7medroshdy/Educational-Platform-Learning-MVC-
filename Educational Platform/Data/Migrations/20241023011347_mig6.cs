using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_Platform.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompletedLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LessonId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedLessons_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompletedLessons_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompletedLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompletedLessons_Lessons_LessonId1",
                        column: x => x.LessonId1,
                        principalTable: "Lessons",
                        principalColumn: "LessonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLessons_ApplicationUserId",
                table: "CompletedLessons",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLessons_LessonId",
                table: "CompletedLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLessons_LessonId1",
                table: "CompletedLessons",
                column: "LessonId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedLessons_StudentId",
                table: "CompletedLessons",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedLessons");
        }
    }
}
