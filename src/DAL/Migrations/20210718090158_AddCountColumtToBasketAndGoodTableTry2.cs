using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddCountColumtToBasketAndGoodTableTry2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ce81bc1-970a-4c94-b4ff-39c5b7846909");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ec609ea-3a25-42a1-b2a4-8a7a9d51ff32");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BasketAndGood",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0209734b-7788-4c14-bf53-6bfdf3aa935a", "a48cc2f3-1189-41ce-bfa0-2755c73278f1", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79032169-f520-4f78-905f-13b861d4422e", "3e9a9d89-8df7-436c-b0f7-ae65fe81dd18", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0209734b-7788-4c14-bf53-6bfdf3aa935a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79032169-f520-4f78-905f-13b861d4422e");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "BasketAndGood");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ec609ea-3a25-42a1-b2a4-8a7a9d51ff32", "5a3f2161-6a83-4aa6-bc66-a17fdbb163bf", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ce81bc1-970a-4c94-b4ff-39c5b7846909", "38eeabd5-a94e-46d8-8ced-b54d73ed56da", "Administrator", "ADMINISTRATOR" });
        }
    }
}
