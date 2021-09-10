using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddTypeOfGoodColumnsToBasketAndGoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81354fb4-24b9-4b00-8708-51ada078b9f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e03ea6ca-26c6-4a4f-8ade-979530d6f45a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12f0aeb0-90d0-40d0-9700-04007b12cb42", "80c24b14-9985-4308-9918-2cd27cef1ccb", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9c189445-9d9e-4319-b001-4f8b02f2d956", "c90ff23f-8dc9-4a63-a2d0-c98e914d9b6e", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "e03ea6ca-26c6-4a4f-8ade-979530d6f45a", "89a5389f-8d58-45ce-89f5-58b063b5bf15", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81354fb4-24b9-4b00-8708-51ada078b9f4", "57b06dd9-052a-4d4d-b149-389f309fe14d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
