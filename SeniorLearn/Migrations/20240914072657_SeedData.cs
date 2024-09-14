using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeniorLearn.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "00a9ce5f-d3f4-40ec-8eaf-4b716e66bb7f", "Administrator", "ADMINISTRATOR" },
                    { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "9d4d2a2a-b9d1-4745-888f-89f11fdb7e41", "Standard", "STANDARD" },
                    { "2199dac7-bac1-49f0-8820-07b34f79533b", "fc3c43a3-41a5-483f-acae-6068092cade6", "Honorary", "HONORARY" },
                    { "de1e5fe5-585b-4867-aae8-57776d64f330", "827c54f3-d19c-4575-a424-3ae9f3910130", "Professional", "PROFESSIONAL" }
                });

            migrationBuilder.InsertData(
                table: "Organisations",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Senior Learn" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrganisationId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Registered", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7610170e-d0e7-43b9-a289-02d13056d54e", 0, "b2686cbb-099f-4c58-91a4-8fcb9c048d35", "Member", "j.bloggs@seniorlearn.com.au", true, "Joe", "Bloggs", false, null, null, "J.BLOGGS@SENIORLEARN.COM.AU", 1, "AQAAAAIAAYagAAAAENaAF8X3fgawsa7CT8EKV1Bm+PGcrq9PhRBL+ee6Rb8lCZVRf/6it+zEesnSHS6q1w==", null, false, new DateTime(2024, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4384), "LZOWMFVS2SAJIT7PFI3CPG4WQDCHQS5R", false, "j.bloggs@seniorlearn.com.au" },
                    { "c6e5a515-b561-458a-85e6-ab9e7eed58f4", 0, "36bea754-e167-42af-83ed-bd78392859f3", "Member", "m.member@seniorlearn.com.au", true, "Mary", "Member", false, null, null, "M.MEMBER@SENIORLEARN.COM.AU", 1, "AQAAAAIAAYagAAAAEGuoaNhuyNZDd/SdkB7dMyKO61l9hBzj4h26Bm6gmQpnrpwe+vNFNyBLSPj0JGM13Q==", null, false, new DateTime(2024, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4379), "ISWZYSPA6TIRY35DE4KKKESEPQZKL6VG", false, "m.member@seniorlearn.com.au" },
                    { "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2", 0, "3e098325-ba04-4578-8bd8-231bbf8dde66", "Member", "a.admin@seniorlearn.com.au", true, "Adam", "Admin", false, null, null, "A.ADMIN@SENIORLEARN.COM.AU", 1, "AQAAAAIAAYagAAAAEHsSevUsbVfCvzTrAPeOAJGAdLJXoClxNuG4OJyPozgYXexeGOqLXgnIxAZgTQTbfA==", null, false, new DateTime(2024, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4368), "M67EBX32EPBJDLSU75U3EA5SFKIR7MDP", false, "a.admin@seniorlearn.com.au" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator", "EndDate", "RoleType", "StartDate" },
                values: new object[,]
                {
                    { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e", "OrganisationUserRole", new DateTime(2025, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4480), 2, new DateTime(2024, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4480) },
                    { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4", "OrganisationUserRole", new DateTime(2025, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4475), 1, new DateTime(2024, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4474) },
                    { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2", "OrganisationUserRole", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), 0, new DateTime(2024, 9, 14, 7, 26, 56, 560, DateTimeKind.Utc).AddTicks(4472) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2199dac7-bac1-49f0-8820-07b34f79533b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "de1e5fe5-585b-4867-aae8-57776d64f330", "7610170e-d0e7-43b9-a289-02d13056d54e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1455a748-82ad-4e31-bb41-7c72cfc0fbfa", "c6e5a515-b561-458a-85e6-ab9e7eed58f4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "09adf476-7af7-4bd7-89e5-d173778b3ec9", "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09adf476-7af7-4bd7-89e5-d173778b3ec9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1455a748-82ad-4e31-bb41-7c72cfc0fbfa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de1e5fe5-585b-4867-aae8-57776d64f330");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7610170e-d0e7-43b9-a289-02d13056d54e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e5a515-b561-458a-85e6-ab9e7eed58f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2");

            migrationBuilder.DeleteData(
                table: "Organisations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
