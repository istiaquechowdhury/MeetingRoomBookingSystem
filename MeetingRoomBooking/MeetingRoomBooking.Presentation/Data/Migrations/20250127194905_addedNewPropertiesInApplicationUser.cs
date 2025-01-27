using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBooking.Presentation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedNewPropertiesInApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pin",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"),
                column: "ConcurrencyStamp",
                value: "f346c035-cd48-41b0-a801-c02a27da8152");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"),
                column: "ConcurrencyStamp",
                value: "74efb950-f857-460d-a759-2402df49cab1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"),
                columns: new[] { "ConcurrencyStamp", "Department", "Designation", "PasswordHash", "Pin", "SecurityStamp", "Status" },
                values: new object[] { "e445dffb-cfb5-432c-8dbd-493a03ad6587", null, null, "AQAAAAIAAYagAAAAEIcLtplgdZhK40snrViH91GYRBam7bsXKjXK2jMbvg7w3hz1yziW90YzhQnwaDRz7A==", null, "a40b0518-9217-4f90-9c0c-6b332304a460", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

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
        }
    }
}
