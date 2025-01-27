using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeetingRoomBooking.Presentation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedAdminandUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"), "28257fbc-e923-4da3-8963-b7d08c9c310d", "Admin", "ADMIN" },
                    { new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"), "79a69b53-414b-40a2-b452-49fa521054c5", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"));
        }
    }
}
