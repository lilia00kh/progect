using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddToyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6f40070-a6a0-42bb-bcad-2154913337dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bad2d379-a3a2-4783-99e4-ce4746c5cbe8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9b3e0fbd-9942-4c39-9ecc-ee7e8cd85d62", "d3f0f208-03a0-4079-9106-4d73457c179a", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebec3652-829f-4a8c-8dae-83db97370a48", "dcab8394-896d-44c6-900a-5bf09ec8b6e8", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b3e0fbd-9942-4c39-9ecc-ee7e8cd85d62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebec3652-829f-4a8c-8dae-83db97370a48");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6f40070-a6a0-42bb-bcad-2154913337dc", "02a6d594-0fdd-4355-9d45-f8e3a3154383", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bad2d379-a3a2-4783-99e4-ce4746c5cbe8", "9f918497-5b16-4f53-8e03-6ab3ca2929b2", "Administrator", "ADMINISTRATOR" });
        }
    }
}
