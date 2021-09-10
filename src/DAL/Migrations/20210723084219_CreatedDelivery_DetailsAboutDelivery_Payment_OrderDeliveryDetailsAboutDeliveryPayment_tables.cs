using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CreatedDelivery_DetailsAboutDelivery_Payment_OrderDeliveryDetailsAboutDeliveryPayment_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "193fe9c0-b58b-44f7-9acc-fb85a45aa575");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cd98315-abf1-46eb-9515-2640e4e59b29");

            migrationBuilder.CreateTable(
                name: "DetailsAboutDelivery",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsAboutDelivery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    User = table.Column<string>(maxLength: 60, nullable: false),
                    UserEmail = table.Column<string>(maxLength: 60, nullable: false),
                    UserName = table.Column<string>(maxLength: 60, nullable: false),
                    Address = table.Column<string>(maxLength: 60, nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    GoodId = table.Column<Guid>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DetailsAboutDeliveryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_DetailsAboutDelivery_DetailsAboutDeliveryId",
                        column: x => x.DetailsAboutDeliveryId,
                        principalTable: "DetailsAboutDelivery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDeliveryDetailsAboutGoodPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeliveryId = table.Column<Guid>(nullable: false),
                    DetailsAboutGoodId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveryDetailsAboutGoodPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryDetailsAboutGoodPayment_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryDetailsAboutGoodPayment_DetailsAboutGood_DetailsAboutGoodId",
                        column: x => x.DetailsAboutGoodId,
                        principalTable: "DetailsAboutGood",
                        principalColumn: "DetailsAboutGoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDeliveryDetailsAboutGoodPayment_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eca7351d-3362-4390-b577-9be429113777", "dda58996-9e2b-4813-8ce4-0a03e362be2c", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f5f94fca-d17d-4137-93a0-77810290c87c", "90251032-0a7f-4499-8567-172ffa1c729d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DetailsAboutDeliveryId",
                table: "Deliveries",
                column: "DetailsAboutDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryDetailsAboutGoodPayment_DeliveryId",
                table: "OrderDeliveryDetailsAboutGoodPayment",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryDetailsAboutGoodPayment_DetailsAboutGoodId",
                table: "OrderDeliveryDetailsAboutGoodPayment",
                column: "DetailsAboutGoodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryDetailsAboutGoodPayment_PaymentId",
                table: "OrderDeliveryDetailsAboutGoodPayment",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDeliveryDetailsAboutGoodPayment");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "DetailsAboutDelivery");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eca7351d-3362-4390-b577-9be429113777");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5f94fca-d17d-4137-93a0-77810290c87c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "193fe9c0-b58b-44f7-9acc-fb85a45aa575", "9978b793-57ae-46d4-9c1b-c6f87698fb26", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9cd98315-abf1-46eb-9515-2640e4e59b29", "d866a685-6dfe-4701-bc3f-4312590c213f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
