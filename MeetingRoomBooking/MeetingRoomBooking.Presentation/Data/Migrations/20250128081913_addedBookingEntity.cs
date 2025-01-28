using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoomBooking.Presentation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedBookingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepeatBooking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_meetingRooms_MeetingRoomId",
                        column: x => x.MeetingRoomId,
                        principalTable: "meetingRooms",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"),
                column: "ConcurrencyStamp",
                value: "88b3c60c-a478-4885-b222-0396e4c056cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"),
                column: "ConcurrencyStamp",
                value: "0fc2e1d2-069e-4cbc-a147-eeb947dd3a99");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e98bb2e5-b339-4e6b-938c-2f9831868257", "AQAAAAIAAYagAAAAEJmyXb7eyaC+cRqs5byCiF50zAYbq7vNWkqt/SqHUGwWuM+/DNVrpdcE+Lg6rUe0sQ==", "7d24344a-b467-4f5e-8c67-e712ccc5d3b9" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MeetingRoomId",
                table: "Bookings",
                column: "MeetingRoomId");

            // Fix the foreign key issue: Using ApplicationUserRole
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<Guid>_AspNetUsers_UserId",
                table: "AspNetUserRoles", // Use the correct table name
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserRole<Guid>_AspNetUsers_UserId",
                table: "AspNetUserRoles"); // Corrected table name

            migrationBuilder.DropTable(
                name: "Bookings");

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
    }
}
