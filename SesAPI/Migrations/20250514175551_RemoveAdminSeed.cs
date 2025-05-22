using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SesAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234", "B1C2D3E4-F5A6-2345-6789-01BCDEFA2345" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B1C2D3E4-F5A6-2345-6789-01BCDEFA2345");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "B1C2D3E4-F5A6-2345-6789-01BCDEFA2345", 0, "0c87d2cf-5f65-4124-a9ea-8a60e1f170a2", "admin@seslikitap.com", true, false, null, "ADMIN@SESLIKITAP.COM", "ADMIN@SESLIKITAP.COM", "AQAAAAIAAYagAAAAECK+phkWR/wOXQ1VeX7/qrqRFu6f1fpugRGxlp98ecp/La/osLpdOkgTrXdT/ynwaA==", null, false, "STATIC-ADMIN-SECURITY-STAMP", false, "admin@seslikitap.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234", "B1C2D3E4-F5A6-2345-6789-01BCDEFA2345" });
        }
    }
}
