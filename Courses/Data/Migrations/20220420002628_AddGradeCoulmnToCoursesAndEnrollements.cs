using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses.Data.Migrations
{
    public partial class AddGradeCoulmnToCoursesAndEnrollements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Grade",
                table: "Enrollements",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Grade",
                table: "Courses",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Enrollements",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
