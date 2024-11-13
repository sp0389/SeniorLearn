using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearn.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                column: "ConcurrencyStamp",
                value: "424b7abd-4d5f-45c8-9a67-9c7bd9a777c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                column: "ConcurrencyStamp",
                value: "39a4759e-8194-4583-bfa4-c98fa0cdcc78");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2199dac7-bac1-49f0-8820-07b34f79533b",
                column: "ConcurrencyStamp",
                value: "ae363857-8e3b-40dd-920f-0bd59a3324bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1e5fe5-585b-4867-aae8-57776d64f330",
                column: "ConcurrencyStamp",
                value: "de2e4d15-d436-4270-afa2-54fe3a688010");

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1971), new DateTime(2024, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1971) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1966), new DateTime(2024, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1965) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2" },
                column: "StartDate",
                value: new DateTime(2024, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1963));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7610170e-d0e7-43b9-a289-02d13056d54e",
                column: "Registered",
                value: new DateTime(2024, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1943));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                column: "Registered",
                value: new DateTime(2024, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1936));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                column: "Registered",
                value: new DateTime(2024, 11, 11, 10, 11, 43, 443, DateTimeKind.Utc).AddTicks(1927));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Lessons");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                column: "ConcurrencyStamp",
                value: "6e7d649d-4e2e-49f5-bf17-382d8f92e6c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                column: "ConcurrencyStamp",
                value: "0d5be49a-5a46-4986-bf89-b5eaf5a55eee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2199dac7-bac1-49f0-8820-07b34f79533b",
                column: "ConcurrencyStamp",
                value: "4e911cd1-cfa2-425e-8182-f0a23b860472");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1e5fe5-585b-4867-aae8-57776d64f330",
                column: "ConcurrencyStamp",
                value: "a5dfe6f5-de6c-468a-a096-a9573481f04a");

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7972), new DateTime(2024, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7972) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7964), new DateTime(2024, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7964) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2" },
                column: "StartDate",
                value: new DateTime(2024, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7962));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7610170e-d0e7-43b9-a289-02d13056d54e",
                column: "Registered",
                value: new DateTime(2024, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                column: "Registered",
                value: new DateTime(2024, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                column: "Registered",
                value: new DateTime(2024, 10, 21, 14, 54, 14, 690, DateTimeKind.Utc).AddTicks(7914));
        }
    }
}
