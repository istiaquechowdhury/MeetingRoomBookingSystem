using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBooking.Presentation.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedNullablefromBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"),
                column: "ConcurrencyStamp",
                value: "c6e9e9e0-a82f-496f-9e26-b8206d3d502f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"),
                column: "ConcurrencyStamp",
                value: "a4b9f1cd-8457-49e8-ac3e-2c751cbf64ab");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d354b327-6f0f-48b1-a55b-e034a87fc161", "AQAAAAIAAYagAAAAEMH7ie4dRiscb4aTm00JcMfHqHRDzws0uNBxaZq0umjQCAcFIGvvwbGCDusxGkPQ+w==", "fcc50453-9333-495b-9443-3a94a663eb15" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e445dffb-cfb5-432c-8dbd-493a03ad6587", "AQAAAAIAAYagAAAAEIcLtplgdZhK40snrViH91GYRBam7bsXKjXK2jMbvg7w3hz1yziW90YzhQnwaDRz7A==", "a40b0518-9217-4f90-9c0c-6b332304a460" });
        }
    }
}
