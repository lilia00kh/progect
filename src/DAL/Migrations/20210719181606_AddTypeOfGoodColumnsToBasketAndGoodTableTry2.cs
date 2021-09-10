using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddTypeOfGoodColumnsToBasketAndGoodTableTry2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12f0aeb0-90d0-40d0-9700-04007b12cb42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c189445-9d9e-4319-b001-4f8b02f2d956");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f0410f08-e5a5-4b96-9712-bf5df41e676d", "33b74d09-f6cd-4fcb-84b2-1f75b58b0901", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b68fdf01-0b93-4aff-ae66-b8e368ff11ab", "977a95e0-9a14-4d63-96df-7dc2873e1a73", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b68fdf01-0b93-4aff-ae66-b8e368ff11ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0410f08-e5a5-4b96-9712-bf5df41e676d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12f0aeb0-90d0-40d0-9700-04007b12cb42", "80c24b14-9985-4308-9918-2cd27cef1ccb", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9c189445-9d9e-4319-b001-4f8b02f2d956", "c90ff23f-8dc9-4a63-a2d0-c98e914d9b6e", "Administrator", "ADMINISTRATOR" });
        }
    }
}
