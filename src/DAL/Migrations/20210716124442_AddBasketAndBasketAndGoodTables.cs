using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddBasketAndBasketAndGoodTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1857aae-d1b0-479e-8257-a236ece4c607");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb42aa8b-83e8-48d7-8e67-9cf57191fcb1");

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketId);
                });

            migrationBuilder.CreateTable(
                name: "BasketAndGood",
                columns: table => new
                {
                    BasketAndGoodId = table.Column<Guid>(nullable: false),
                    BasketId = table.Column<Guid>(nullable: false),
                    GoodId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketAndGood", x => x.BasketAndGoodId);
                    table.ForeignKey(
                        name: "FK_BasketAndGood_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64bd27ac-e4ef-4655-8a92-6f798d1b8241", "df909808-08d3-48e2-b47b-b407f14ba2e5", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6892d91-f1cf-42fe-9da1-17b8b4a946d1", "646e97fe-b2ed-4671-83b9-a2e2134b6a94", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketAndGood_BasketId",
                table: "BasketAndGood",
                column: "BasketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketAndGood");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64bd27ac-e4ef-4655-8a92-6f798d1b8241");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6892d91-f1cf-42fe-9da1-17b8b4a946d1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1857aae-d1b0-479e-8257-a236ece4c607", "970bdc11-8e5f-4872-ab58-b9374f9093c6", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb42aa8b-83e8-48d7-8e67-9cf57191fcb1", "bb77b54b-e6a5-4b1d-bd76-7dec1e55f86f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
