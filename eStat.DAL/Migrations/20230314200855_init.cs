using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Characteristics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InUse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductGUID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Membership = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserGUID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderGUID);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserGUID",
                        column: x => x.UserGUID,
                        principalTable: "Users",
                        principalColumn: "UserGUID");
                });

            migrationBuilder.CreateTable(
                name: "ProductRequests",
                columns: table => new
                {
                    ProductRequestGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRequests", x => x.ProductRequestGUID);
                    table.ForeignKey(
                        name: "FK_ProductRequests_Products_ProductGUID",
                        column: x => x.ProductGUID,
                        principalTable: "Products",
                        principalColumn: "ProductGUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRequests_Users_UserGUID",
                        column: x => x.UserGUID,
                        principalTable: "Users",
                        principalColumn: "UserGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseGUID);
                    table.ForeignKey(
                        name: "FK_Purchases_Users_UserGUID",
                        column: x => x.UserGUID,
                        principalTable: "Users",
                        principalColumn: "UserGUID");
                });

            migrationBuilder.CreateTable(
                name: "Searches",
                columns: table => new
                {
                    SearchGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Searches", x => x.SearchGUID);
                    table.ForeignKey(
                        name: "FK_Searches_Products_ProductGUID",
                        column: x => x.ProductGUID,
                        principalTable: "Products",
                        principalColumn: "ProductGUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Searches_Users_UserGUID",
                        column: x => x.UserGUID,
                        principalTable: "Users",
                        principalColumn: "UserGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProducts",
                columns: table => new
                {
                    UserProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProducts", x => x.UserProductGUID);
                    table.ForeignKey(
                        name: "FK_UserProducts_Products_ProductGUID",
                        column: x => x.ProductGUID,
                        principalTable: "Products",
                        principalColumn: "ProductGUID");
                    table.ForeignKey(
                        name: "FK_UserProducts_Users_UserGUID",
                        column: x => x.UserGUID,
                        principalTable: "Users",
                        principalColumn: "UserGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.OrderProductGUID);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderGUID",
                        column: x => x.OrderGUID,
                        principalTable: "Orders",
                        principalColumn: "OrderGUID");
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductGUID",
                        column: x => x.ProductGUID,
                        principalTable: "Products",
                        principalColumn: "ProductGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseProducts",
                columns: table => new
                {
                    PurchaseProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProducts", x => x.PurchaseProductGUID);
                    table.ForeignKey(
                        name: "FK_PurchaseProducts_Purchases_PurchaseGUID",
                        column: x => x.PurchaseGUID,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseGUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseProducts_UserProducts_UserProductGUID",
                        column: x => x.UserProductGUID,
                        principalTable: "UserProducts",
                        principalColumn: "UserProductGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderGUID",
                table: "OrderProducts",
                column: "OrderGUID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductGUID",
                table: "OrderProducts",
                column: "ProductGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserGUID",
                table: "Orders",
                column: "UserGUID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequests_ProductGUID",
                table: "ProductRequests",
                column: "ProductGUID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequests_UserGUID",
                table: "ProductRequests",
                column: "UserGUID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProducts_PurchaseGUID",
                table: "PurchaseProducts",
                column: "PurchaseGUID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProducts_UserProductGUID",
                table: "PurchaseProducts",
                column: "UserProductGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserGUID",
                table: "Purchases",
                column: "UserGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Searches_ProductGUID",
                table: "Searches",
                column: "ProductGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Searches_UserGUID",
                table: "Searches",
                column: "UserGUID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProducts_ProductGUID",
                table: "UserProducts",
                column: "ProductGUID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProducts_UserGUID",
                table: "UserProducts",
                column: "UserGUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductRequests");

            migrationBuilder.DropTable(
                name: "PurchaseProducts");

            migrationBuilder.DropTable(
                name: "Searches");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "UserProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
