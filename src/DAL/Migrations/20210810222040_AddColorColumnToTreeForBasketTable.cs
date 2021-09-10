using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddColorColumnToTreeForBasketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a4c092d-4bca-49fa-81d9-bf887e7ee454");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eefc65ba-cdf0-4ed7-99d2-da0aff5a1854");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "DetailsAboutGood",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a098b751-1bf1-411d-b189-76f27ec4c17d", "85c2ad71-6a93-44db-99db-fe13dc4d0edc", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19d2a653-5241-44cf-880b-ca1bd95a77b9", "207f904e-80a9-4d24-9ade-d188da0408fa", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19d2a653-5241-44cf-880b-ca1bd95a77b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a098b751-1bf1-411d-b189-76f27ec4c17d");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "DetailsAboutGood");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a4c092d-4bca-49fa-81d9-bf887e7ee454", "fccaa3a8-7f43-46d9-be53-1ee228d68d77", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eefc65ba-cdf0-4ed7-99d2-da0aff5a1854", "022c53c0-a0be-4dc9-960d-76fe4c422a57", "Administrator", "ADMINISTRATOR" });
        }
    }
}
