using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddTreeFunctionalityDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af227497-3043-44a4-8c7f-34462b4677fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c43805fe-28a9-4dcd-a6c0-4d5558edd4d0");

            migrationBuilder.CreateTable(
                name: "TreeSize",
                columns: table => new
                {
                    TreeSizeId = table.Column<Guid>(nullable: false),
                    TreeId = table.Column<Guid>(nullable: false),
                    SizeId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeSize", x => x.TreeSizeId);
                });

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    TreeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TreeSizeId = table.Column<Guid>(nullable: false),
                    TreeSizeId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trees", x => x.TreeId);
                    table.ForeignKey(
                        name: "FK_Trees_TreeSize_TreeSizeId1",
                        column: x => x.TreeSizeId1,
                        principalTable: "TreeSize",
                        principalColumn: "TreeSizeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea32c2e9-8465-4478-9644-f30a6713a930", "ca9c676a-a33d-4a7e-a0dd-a3eaa50db9be", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f33b09e-67be-45b8-b938-7a78e74829b2", "5a0d5513-7d7d-4246-8dc0-f2e35473e6ea", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Trees_TreeSizeId1",
                table: "Trees",
                column: "TreeSizeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropTable(
                name: "TreeSize");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f33b09e-67be-45b8-b938-7a78e74829b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea32c2e9-8465-4478-9644-f30a6713a930");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CountOfVotes = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 60, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c43805fe-28a9-4dcd-a6c0-4d5558edd4d0", "ab302a25-0e8a-4510-8bd5-c3f54698c023", "Viewer", "VIEWER" },
                    { "af227497-3043-44a4-8c7f-34462b4677fd", "e62da7c6-2add-42f5-a3c7-98c4260cd3b1", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CountOfVotes", "Description", "Rate", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("e1019ea6-c93e-4fc0-b593-8f5605a1a1ef"), "583 Wall Dr. Gwynn Oak, MD 21207", 0, "USA", 0.0, "The Lord of rings", 2012 },
                    { new Guid("756e13fe-df2d-400a-abc0-2c50fee92940"), "Jon Snow", 0, "Very interesting book", 0.0, "A song of ice and fire", 1996 },
                    { new Guid("577ffecf-0a09-4e12-a05a-d030072c3e3c"), "The little prince", 0, "", 0.0, "Saint-Exupery", 2012 },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Orwell", 0, "", 0.0, "1984", 2016 },
                    { new Guid("c36fedfa-357f-4f29-ad58-2af8e5464cd7"), "1963", 0, "USA", 0.0, "Stephan King", 2012 },
                    { new Guid("ce420acf-2252-47ef-885c-fe24bdd49bdb"), "1963", 0, "USA", 0.0, "Stephan King", 2012 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "BookId" },
                values: new object[,]
                {
                    { new Guid("f7670bde-5375-46fc-8566-9777c96c65ba"), new Guid("e1019ea6-c93e-4fc0-b593-8f5605a1a1ef") },
                    { new Guid("59211e0b-e2ab-4333-995f-1eca6017e836"), new Guid("756e13fe-df2d-400a-abc0-2c50fee92940") },
                    { new Guid("5f1bcba6-d218-4d4b-b4b7-4e6f9e58c6cb"), new Guid("577ffecf-0a09-4e12-a05a-d030072c3e3c") },
                    { new Guid("b8a34fb0-24f1-4e20-aeba-eaac33ff5ba5"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("810902a3-671c-4c23-9463-f7f19bf834a1"), new Guid("c36fedfa-357f-4f29-ad58-2af8e5464cd7") }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Address", "BookId", "Phone", "Status", "User", "UserEmail", "UserName" },
                values: new object[] { new Guid("8d7c2c0a-9628-4700-9cef-472b5691f845"), "Horodok", new Guid("e1019ea6-c93e-4fc0-b593-8f5605a1a1ef"), "+1111-111-1111", "new", "lilia00kh@gmail.com", "lilia00kh@gmail.com", "Liliia" });
        }
    }
}
