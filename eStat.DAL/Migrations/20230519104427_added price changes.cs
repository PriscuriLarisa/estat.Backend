using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedpricechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceChanges",
                columns: table => new
                {
                    PriceChangeGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProductGUID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductGUID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceChanges", x => x.PriceChangeGUID);
                    table.ForeignKey(
                        name: "FK_PriceChanges_Products_ProductGUID1",
                        column: x => x.ProductGUID1,
                        principalTable: "Products",
                        principalColumn: "ProductGUID");
                    table.ForeignKey(
                        name: "FK_PriceChanges_UserProducts_UserProductGUID1",
                        column: x => x.UserProductGUID1,
                        principalTable: "UserProducts",
                        principalColumn: "UserProductGUID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceChanges_ProductGUID1",
                table: "PriceChanges",
                column: "ProductGUID1");

            migrationBuilder.CreateIndex(
                name: "IX_PriceChanges_UserProductGUID1",
                table: "PriceChanges",
                column: "UserProductGUID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceChanges");
        }
    }
}
