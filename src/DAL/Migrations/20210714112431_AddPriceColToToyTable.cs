using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddPriceColToToyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51aa0bcd-57f4-4c5c-8399-b34b04e5967d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "852dd6b2-46ce-4a38-be95-33e6647bda44");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "07864782-9a86-4dc8-b4c9-59c5879aa8f8", "d9c8e29e-2497-4122-bfa2-f51598067647", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "420d95c4-f921-4a1b-bf55-680a49832576", "87a31705-6ded-470f-b6f1-aadcaffe9e2f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07864782-9a86-4dc8-b4c9-59c5879aa8f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "420d95c4-f921-4a1b-bf55-680a49832576");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "852dd6b2-46ce-4a38-be95-33e6647bda44", "9d02de18-1224-4944-b614-4bfd9fb56c8f", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51aa0bcd-57f4-4c5c-8399-b34b04e5967d", "3f77f43f-7a47-47a7-b165-32c9712e96af", "Administrator", "ADMINISTRATOR" });
        }
    }
}
