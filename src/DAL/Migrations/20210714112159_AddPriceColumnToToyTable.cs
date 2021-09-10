using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddPriceColumnToToyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e00cced-754e-4090-b3c5-e6c4f049c13e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac55eba6-68c7-4e0e-9c8d-01e7e3e93321");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Toys",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "852dd6b2-46ce-4a38-be95-33e6647bda44", "9d02de18-1224-4944-b614-4bfd9fb56c8f", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51aa0bcd-57f4-4c5c-8399-b34b04e5967d", "3f77f43f-7a47-47a7-b165-32c9712e96af", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51aa0bcd-57f4-4c5c-8399-b34b04e5967d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "852dd6b2-46ce-4a38-be95-33e6647bda44");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Toys");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e00cced-754e-4090-b3c5-e6c4f049c13e", "642b97d3-2e94-4a72-ba9f-6c4db580f783", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac55eba6-68c7-4e0e-9c8d-01e7e3e93321", "bb24803f-3acf-40aa-a4df-c78790593e5b", "Administrator", "ADMINISTRATOR" });
        }
    }
}
