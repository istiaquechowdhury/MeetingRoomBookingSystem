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
            migrationBuilder.CreateTable(
                name: "IdentityUserRole<Guid>",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<Guid>", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"),
                column: "ConcurrencyStamp",
                value: "44afa394-e56b-4108-af6c-c9a1773a0ddb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3c852ab-7db6-4f1a-89c9-8d1abc234567"),
                column: "ConcurrencyStamp",
                value: "4fd2338c-8276-4927-bebc-d88ac9bb58fe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "921724a0-e5fa-4632-a987-e97da20e2b7c", "AQAAAAIAAYagAAAAEGSm8i5FdMQCKGkrlO/gbTEeCZKjlw5doF1/dAweSD3LPfpf/zochXBOCpHY83p48Q==", "cd097a25-df76-470b-959e-b90e6c6df9e4" });

            migrationBuilder.InsertData(
                table: "IdentityUserRole<Guid>",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d0b85c3e-4f68-4a8c-9c92-7aabc1234567"), new Guid("a0f85c3e-4f68-4a8c-9c92-7aabc1234567") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUserRole<Guid>");

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
