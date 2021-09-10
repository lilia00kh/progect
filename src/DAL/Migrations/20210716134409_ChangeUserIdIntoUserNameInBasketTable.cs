using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangeUserIdIntoUserNameInBasketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64bd27ac-e4ef-4655-8a92-6f798d1b8241");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6892d91-f1cf-42fe-9da1-17b8b4a946d1");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Baskets");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Baskets",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbf58bbb-dabd-4409-817f-bf06b73ddde8", "779fc733-bf75-46e2-802d-001957446095", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d825a12c-6a8e-44db-b610-3806bc863685", "5e69a947-334a-4feb-8664-b0d96724385d", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d825a12c-6a8e-44db-b610-3806bc863685");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbf58bbb-dabd-4409-817f-bf06b73ddde8");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Baskets");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Baskets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64bd27ac-e4ef-4655-8a92-6f798d1b8241", "df909808-08d3-48e2-b47b-b407f14ba2e5", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6892d91-f1cf-42fe-9da1-17b8b4a946d1", "646e97fe-b2ed-4671-83b9-a2e2134b6a94", "Administrator", "ADMINISTRATOR" });
        }
    }
}
