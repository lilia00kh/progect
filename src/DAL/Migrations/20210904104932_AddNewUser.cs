using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddNewUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "506ba671-259a-4984-aa7c-37a5a3feea3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e761f8a5-1965-4ed7-b2ba-1c141ac75cb8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01a4b6c4-f289-4e17-a6c6-ee25f6d74c62", "5c4c9a3c-ffac-4236-9620-0296876400d2", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bbbe0ab1-8b1e-4324-a44c-6284772f37ae", "ed4da09b-84cb-4439-a608-8babf05e0811", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a4b6c4-f289-4e17-a6c6-ee25f6d74c62");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbbe0ab1-8b1e-4324-a44c-6284772f37ae");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e761f8a5-1965-4ed7-b2ba-1c141ac75cb8", "6d21baed-88f0-40c8-b851-e11e77386315", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "506ba671-259a-4984-aa7c-37a5a3feea3c", "afc1a8ff-63e6-42b0-99ce-0e867c2a9d82", "Administrator", "ADMINISTRATOR" });
        }
    }
}
