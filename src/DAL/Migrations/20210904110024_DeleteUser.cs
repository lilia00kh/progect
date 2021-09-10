using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DeleteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "64171f1b-fbbb-4ed8-b49a-5a30d183138a", "50ae071f-1c0b-423f-a6ca-d7c8caa3a1c5", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca4540dd-d848-4b73-9288-61b3d620342c", "a3ae2820-56fd-44af-9a29-6166a34187e9", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64171f1b-fbbb-4ed8-b49a-5a30d183138a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca4540dd-d848-4b73-9288-61b3d620342c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01a4b6c4-f289-4e17-a6c6-ee25f6d74c62", "5c4c9a3c-ffac-4236-9620-0296876400d2", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bbbe0ab1-8b1e-4324-a44c-6284772f37ae", "ed4da09b-84cb-4439-a608-8babf05e0811", "Administrator", "ADMINISTRATOR" });
        }
    }
}
