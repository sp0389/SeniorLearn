using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearn.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                column: "ConcurrencyStamp",
                value: "c069a5d0-05dd-4e6c-a7e9-1c41c3177a84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                column: "ConcurrencyStamp",
                value: "28ef1af9-ecd1-400c-89ce-350fd70ce5a9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2199dac7-bac1-49f0-8820-07b34f79533b",
                column: "ConcurrencyStamp",
                value: "dcb75c45-0f9e-4c91-9109-5b696df871dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1e5fe5-585b-4867-aae8-57776d64f330",
                column: "ConcurrencyStamp",
                value: "79260364-afb7-470e-bf94-79d2e777ebd9");

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8977), new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8976) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8972), new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8971) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2" },
                column: "StartDate",
                value: new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7610170e-d0e7-43b9-a289-02d13056d54e",
                columns: new[] { "Registered", "Status" },
                values: new object[] { new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8948), 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                columns: new[] { "Registered", "Status" },
                values: new object[] { new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8940), 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                columns: new[] { "Registered", "Status" },
                values: new object[] { new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8933), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

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
        }
    }
}
