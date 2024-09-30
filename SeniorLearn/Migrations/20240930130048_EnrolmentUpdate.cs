using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearn.Migrations
{
    /// <inheritdoc />
    public partial class EnrolmentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonEnrolment");

            migrationBuilder.RenameColumn(
                name: "IsStandalone",
                table: "Lessons",
                newName: "IsInCourse");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Enrolments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    EnrolmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrolments_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrolments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Enrolments_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                column: "ConcurrencyStamp",
                value: "3ce9c3e2-e3c4-4dbe-8c9e-ae5aeabbd654");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                column: "ConcurrencyStamp",
                value: "293a1bc1-53d0-4bd7-b450-1b34b4fb6588");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2199dac7-bac1-49f0-8820-07b34f79533b",
                column: "ConcurrencyStamp",
                value: "eb006cb6-b22c-4fa8-8c04-e8602d294099");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1e5fe5-585b-4867-aae8-57776d64f330",
                column: "ConcurrencyStamp",
                value: "4b13a951-d802-4135-9564-d125401848da");

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8532), new DateTime(2024, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8531) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8526), new DateTime(2024, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8526) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2" },
                column: "StartDate",
                value: new DateTime(2024, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8524));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7610170e-d0e7-43b9-a289-02d13056d54e",
                column: "Registered",
                value: new DateTime(2024, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8502));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                column: "Registered",
                value: new DateTime(2024, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8497));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                column: "Registered",
                value: new DateTime(2024, 9, 30, 13, 0, 48, 91, DateTimeKind.Utc).AddTicks(8490));

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_CourseId",
                table: "Enrolments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_LessonId",
                table: "Enrolments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_MemberId",
                table: "Enrolments",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrolments");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "IsInCourse",
                table: "Lessons",
                newName: "IsStandalone");

            migrationBuilder.CreateTable(
                name: "LessonEnrolment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnrolmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonEnrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonEnrolment_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonEnrolment_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                column: "ConcurrencyStamp",
                value: "d148dd6b-a294-4d28-9b23-a329023f40d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                column: "ConcurrencyStamp",
                value: "d463b7e0-7e21-44ac-a692-a213884989d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2199dac7-bac1-49f0-8820-07b34f79533b",
                column: "ConcurrencyStamp",
                value: "ea710c86-af2f-4fe9-acd0-dc22af8ac9a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1e5fe5-585b-4867-aae8-57776d64f330",
                column: "ConcurrencyStamp",
                value: "51ef888a-7964-4bd0-9436-9ec974d7c250");

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1269), new DateTime(2024, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1269) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1222), new DateTime(2024, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1222) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2" },
                column: "StartDate",
                value: new DateTime(2024, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1221));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7610170e-d0e7-43b9-a289-02d13056d54e",
                column: "Registered",
                value: new DateTime(2024, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1200));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                column: "Registered",
                value: new DateTime(2024, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1187));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                column: "Registered",
                value: new DateTime(2024, 9, 24, 9, 50, 58, 64, DateTimeKind.Utc).AddTicks(1173));

            migrationBuilder.CreateIndex(
                name: "IX_LessonEnrolment_LessonId",
                table: "LessonEnrolment",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonEnrolment_MemberId",
                table: "LessonEnrolment",
                column: "MemberId");
        }
    }
}
