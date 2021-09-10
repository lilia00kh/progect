using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateTreesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trees_TreeSize_TreeSizeId1",
                table: "Trees");

            migrationBuilder.DropTable(
                name: "TreeSize");

            migrationBuilder.DropIndex(
                name: "IX_Trees_TreeSizeId1",
                table: "Trees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f33b09e-67be-45b8-b938-7a78e74829b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea32c2e9-8465-4478-9644-f30a6713a930");

            migrationBuilder.DropColumn(
                name: "TreeSizeId",
                table: "Trees");

            migrationBuilder.DropColumn(
                name: "TreeSizeId1",
                table: "Trees");

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<Guid>(nullable: false),
                    NameOfSize = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "TreeSizesAndPrices",
                columns: table => new
                {
                    TreeSizeAndPriceId = table.Column<Guid>(nullable: false),
                    TreeId = table.Column<Guid>(nullable: false),
                    SizeId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeSizesAndPrices", x => x.TreeSizeAndPriceId);
                    table.ForeignKey(
                        name: "FK_TreeSizesAndPrices_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreeSizesAndPrices_Trees_TreeId",
                        column: x => x.TreeId,
                        principalTable: "Trees",
                        principalColumn: "TreeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6f40070-a6a0-42bb-bcad-2154913337dc", "02a6d594-0fdd-4355-9d45-f8e3a3154383", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bad2d379-a3a2-4783-99e4-ce4746c5cbe8", "9f918497-5b16-4f53-8e03-6ab3ca2929b2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_TreeSizesAndPrices_SizeId",
                table: "TreeSizesAndPrices",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TreeSizesAndPrices_TreeId",
                table: "TreeSizesAndPrices",
                column: "TreeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreeSizesAndPrices");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6f40070-a6a0-42bb-bcad-2154913337dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bad2d379-a3a2-4783-99e4-ce4746c5cbe8");

            migrationBuilder.AddColumn<Guid>(
                name: "TreeSizeId",
                table: "Trees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TreeSizeId1",
                table: "Trees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TreeSize",
                columns: table => new
                {
                    TreeSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeSize", x => x.TreeSizeId);
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

            migrationBuilder.AddForeignKey(
                name: "FK_Trees_TreeSize_TreeSizeId1",
                table: "Trees",
                column: "TreeSizeId1",
                principalTable: "TreeSize",
                principalColumn: "TreeSizeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
