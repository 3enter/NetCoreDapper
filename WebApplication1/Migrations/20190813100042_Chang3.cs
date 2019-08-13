using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Chang3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Class_ClassID",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "Student",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Class_ClassID",
                table: "Student",
                column: "ClassID",
                principalTable: "Class",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Class_ClassID",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "Student",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Class_ClassID",
                table: "Student",
                column: "ClassID",
                principalTable: "Class",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
