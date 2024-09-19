using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearn.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_MemberId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MemberId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Availability",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MemberId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MemberId",
                table: "Courses",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_MemberId",
                table: "Courses",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
