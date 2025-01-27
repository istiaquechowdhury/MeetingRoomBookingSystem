using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBooking.Presentation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedAdminTOaspnetUsertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"),
                column: "ConcurrencyStamp",
                value: "9365405c-4f7a-4e16-bd84-dd70360431c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"),
                column: "ConcurrencyStamp",
                value: "7a0d0f8c-5435-42bc-85ac-034999516a21");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"), 0, "9e977191-87a4-48ae-b93c-5be74c7ae585", "admin@gmail.com", true, null, null, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAECV+46FRhwh916Faatd41rPLBbnRXALFR6q9984H2r73BAZztW6HSx/fE6I2ByTLBw==", null, false, "d001839b-3a7d-436f-b9b2-ff178357e220", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"),
                column: "ConcurrencyStamp",
                value: "28257fbc-e923-4da3-8963-b7d08c9c310d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"),
                column: "ConcurrencyStamp",
                value: "79a69b53-414b-40a2-b452-49fa521054c5");
        }
    }
}
