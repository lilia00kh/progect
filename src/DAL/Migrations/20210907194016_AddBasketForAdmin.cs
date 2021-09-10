using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddBasketForAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Baskets",
                columns: new[] { "BasketId", "UserName" },
                values: new object[] { new Guid("928fa7e1-97ec-4c3f-8d28-7c26ee2231c9"), "grinch.in.ua@ukr.net" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Baskets",
                keyColumn: "BasketId",
                keyValue: new Guid("928fa7e1-97ec-4c3f-8d28-7c26ee2231c9"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64171f1b-fbbb-4ed8-b49a-5a30d183138a", "50ae071f-1c0b-423f-a6ca-d7c8caa3a1c5", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca4540dd-d848-4b73-9288-61b3d620342c", "a3ae2820-56fd-44af-9a29-6166a34187e9", "Administrator", "ADMINISTRATOR" });
        }
    }
}
