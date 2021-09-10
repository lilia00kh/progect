using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddCountColumtToBasketAndGoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d825a12c-6a8e-44db-b610-3806bc863685");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbf58bbb-dabd-4409-817f-bf06b73ddde8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ec609ea-3a25-42a1-b2a4-8a7a9d51ff32", "5a3f2161-6a83-4aa6-bc66-a17fdbb163bf", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ce81bc1-970a-4c94-b4ff-39c5b7846909", "38eeabd5-a94e-46d8-8ced-b54d73ed56da", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ce81bc1-970a-4c94-b4ff-39c5b7846909");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ec609ea-3a25-42a1-b2a4-8a7a9d51ff32");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbf58bbb-dabd-4409-817f-bf06b73ddde8", "779fc733-bf75-46e2-802d-001957446095", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d825a12c-6a8e-44db-b610-3806bc863685", "5e69a947-334a-4feb-8664-b0d96724385d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
