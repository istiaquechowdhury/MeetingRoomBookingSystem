using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBooking.Presentation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedAdminUserTOAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"),
                column: "ConcurrencyStamp",
                value: "a00c3d0d-2b6c-4386-b549-4ddd678f4ec2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"),
                column: "ConcurrencyStamp",
                value: "af2e0222-735b-4dce-97ab-6eb282f725bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8e8ac11-ea41-4fd1-9df4-85835169654c", "AQAAAAIAAYagAAAAEGIrqDCHoRiGWOUYJoVSth4t0vqGBqDtTwCBhSGOZFDvT80VekOuLpxE4NgBXoO7jQ==", "7b363242-926a-4c11-a038-c1b73b7e5123" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles", // Corrected table name
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"), new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Only drop the data if you want to revert the changes
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"), new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567") });

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e977191-87a4-48ae-b93c-5be74c7ae585", "AQAAAAIAAYagAAAAECV+46FRhwh916Faatd41rPLBbnRXALFR6q9984H2r73BAZztW6HSx/fE6I2ByTLBw==", "d001839b-3a7d-436f-b9b2-ff178357e220" });
        }
    }
}
