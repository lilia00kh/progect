using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CreatedDetailsAboutGoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b68fdf01-0b93-4aff-ae66-b8e368ff11ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0410f08-e5a5-4b96-9712-bf5df41e676d");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "BasketAndGood");

            migrationBuilder.DropColumn(
                name: "TypeOfGood",
                table: "BasketAndGood");

            migrationBuilder.AddColumn<Guid>(
                name: "DetailsAboutGoodId",
                table: "BasketAndGood",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DetailsAboutGood",
                columns: table => new
                {
                    DetailsAboutGoodId = table.Column<Guid>(nullable: false),
                    TypeOfGood = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Size = table.Column<double>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsAboutGood", x => x.DetailsAboutGoodId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "193fe9c0-b58b-44f7-9acc-fb85a45aa575", "9978b793-57ae-46d4-9c1b-c6f87698fb26", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9cd98315-abf1-46eb-9515-2640e4e59b29", "d866a685-6dfe-4701-bc3f-4312590c213f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketAndGood_DetailsAboutGoodId",
                table: "BasketAndGood",
                column: "DetailsAboutGoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketAndGood_DetailsAboutGood_DetailsAboutGoodId",
                table: "BasketAndGood",
                column: "DetailsAboutGoodId",
                principalTable: "DetailsAboutGood",
                principalColumn: "DetailsAboutGoodId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketAndGood_DetailsAboutGood_DetailsAboutGoodId",
                table: "BasketAndGood");

            migrationBuilder.DropTable(
                name: "DetailsAboutGood");

            migrationBuilder.DropIndex(
                name: "IX_BasketAndGood_DetailsAboutGoodId",
                table: "BasketAndGood");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "193fe9c0-b58b-44f7-9acc-fb85a45aa575");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cd98315-abf1-46eb-9515-2640e4e59b29");

            migrationBuilder.DropColumn(
                name: "DetailsAboutGoodId",
                table: "BasketAndGood");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BasketAndGood",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfGood",
                table: "BasketAndGood",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f0410f08-e5a5-4b96-9712-bf5df41e676d", "33b74d09-f6cd-4fcb-84b2-1f75b58b0901", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b68fdf01-0b93-4aff-ae66-b8e368ff11ab", "977a95e0-9a14-4d63-96df-7dc2873e1a73", "Administrator", "ADMINISTRATOR" });
        }
    }
}
