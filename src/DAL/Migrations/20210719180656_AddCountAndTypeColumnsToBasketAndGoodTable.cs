using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddCountAndTypeColumnsToBasketAndGoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0209734b-7788-4c14-bf53-6bfdf3aa935a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79032169-f520-4f78-905f-13b861d4422e");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfGood",
                table: "BasketAndGood",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e03ea6ca-26c6-4a4f-8ade-979530d6f45a", "89a5389f-8d58-45ce-89f5-58b063b5bf15", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81354fb4-24b9-4b00-8708-51ada078b9f4", "57b06dd9-052a-4d4d-b149-389f309fe14d", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81354fb4-24b9-4b00-8708-51ada078b9f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e03ea6ca-26c6-4a4f-8ade-979530d6f45a");

            migrationBuilder.DropColumn(
                name: "TypeOfGood",
                table: "BasketAndGood");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0209734b-7788-4c14-bf53-6bfdf3aa935a", "a48cc2f3-1189-41ce-bfa0-2755c73278f1", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79032169-f520-4f78-905f-13b861d4422e", "3e9a9d89-8df7-436c-b0f7-ae65fe81dd18", "Administrator", "ADMINISTRATOR" });
        }
    }
}
