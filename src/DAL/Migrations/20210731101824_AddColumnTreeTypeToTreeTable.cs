using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddColumnTreeTypeToTreeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d70b2755-aed1-4afe-8759-e0de7312bf61");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8cabd69-6d63-464c-ac81-4a08e6d38f81");

            migrationBuilder.AddColumn<string>(
                name: "TreeType",
                table: "Trees",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e43f388b-8ef1-4892-a925-472091db0282", "8540a76f-eab1-4939-80b4-8a2da320793f", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee38e2fb-5c48-4587-9ce1-1d0adaa3baee", "a28de397-c286-4985-b781-46c612d1169c", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e43f388b-8ef1-4892-a925-472091db0282");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee38e2fb-5c48-4587-9ce1-1d0adaa3baee");

            migrationBuilder.DropColumn(
                name: "TreeType",
                table: "Trees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8cabd69-6d63-464c-ac81-4a08e6d38f81", "31681cbb-4154-41c3-801d-38020eb6b1df", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d70b2755-aed1-4afe-8759-e0de7312bf61", "fc5fb547-54cc-4a0b-8eb3-c2a2f467807f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
