using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearn.Migrations
{
    /// <inheritdoc />
    public partial class MemberPaymentTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_UserId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Payments",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                newName: "IX_Payments_MemberId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                column: "ConcurrencyStamp",
                value: "146df566-15bb-422b-8ce6-b4894f4b3ce9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                column: "ConcurrencyStamp",
                value: "6e65c8df-32e2-4b8b-95d8-f1c4d69b2bf0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2199dac7-bac1-49f0-8820-07b34f79533b",
                column: "ConcurrencyStamp",
                value: "dd4475c2-1e98-47a7-93b5-abfe0ae16a04");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1e5fe5-585b-4867-aae8-57776d64f330",
                column: "ConcurrencyStamp",
                value: "3eb4e0d5-d717-4bb6-9652-84b2d53401db");

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9848), new DateTime(2024, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9848) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4" },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9838), new DateTime(2024, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9838) });

            migrationBuilder.UpdateData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2" },
                column: "StartDate",
                value: new DateTime(2024, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9836));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7610170e-d0e7-43b9-a289-02d13056d54e",
                column: "Registered",
                value: new DateTime(2024, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9811));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                column: "Registered",
                value: new DateTime(2024, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9766));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                column: "Registered",
                value: new DateTime(2024, 10, 17, 15, 14, 46, 115, DateTimeKind.Utc).AddTicks(9758));

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_MemberId",
                table: "Payments",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_MemberId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Payments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_MemberId",
                table: "Payments",
                newName: "IX_Payments_UserId");

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
                column: "Registered",
                value: new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8948));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                column: "Registered",
                value: new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8940));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                column: "Registered",
                value: new DateTime(2024, 10, 17, 4, 15, 22, 637, DateTimeKind.Utc).AddTicks(8933));

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
