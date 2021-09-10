using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddColorColumnToTreeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64af8723-2feb-4c80-9ae6-dd1f8a4357c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa78efd4-7295-43b6-aa2b-132f96af3005");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Trees",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a4c092d-4bca-49fa-81d9-bf887e7ee454", "fccaa3a8-7f43-46d9-be53-1ee228d68d77", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eefc65ba-cdf0-4ed7-99d2-da0aff5a1854", "022c53c0-a0be-4dc9-960d-76fe4c422a57", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a4c092d-4bca-49fa-81d9-bf887e7ee454");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eefc65ba-cdf0-4ed7-99d2-da0aff5a1854");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Trees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa78efd4-7295-43b6-aa2b-132f96af3005", "9d197a29-1c5a-4157-b361-fcf508c33716", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64af8723-2feb-4c80-9ae6-dd1f8a4357c0", "bae3ad6b-7075-49ee-bb0c-7720e52c1939", "Administrator", "ADMINISTRATOR" });
        }
    }
}
