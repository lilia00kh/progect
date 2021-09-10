using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddRecomendationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e43f388b-8ef1-4892-a925-472091db0282");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee38e2fb-5c48-4587-9ce1-1d0adaa3baee");

            migrationBuilder.CreateTable(
                name: "Recomendations",
                columns: table => new
                {
                    RecomendationId = table.Column<Guid>(nullable: false),
                    GoodId = table.Column<Guid>(nullable: false),
                    GoodType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendations", x => x.RecomendationId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa78efd4-7295-43b6-aa2b-132f96af3005", "9d197a29-1c5a-4157-b361-fcf508c33716", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64af8723-2feb-4c80-9ae6-dd1f8a4357c0", "bae3ad6b-7075-49ee-bb0c-7720e52c1939", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recomendations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64af8723-2feb-4c80-9ae6-dd1f8a4357c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa78efd4-7295-43b6-aa2b-132f96af3005");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e43f388b-8ef1-4892-a925-472091db0282", "8540a76f-eab1-4939-80b4-8a2da320793f", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee38e2fb-5c48-4587-9ce1-1d0adaa3baee", "a28de397-c286-4985-b781-46c612d1169c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
