using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedpricechangesfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceChanges_Products_ProductGUID1",
                table: "PriceChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceChanges_UserProducts_UserProductGUID1",
                table: "PriceChanges");

            migrationBuilder.DropIndex(
                name: "IX_PriceChanges_ProductGUID1",
                table: "PriceChanges");

            migrationBuilder.DropIndex(
                name: "IX_PriceChanges_UserProductGUID1",
                table: "PriceChanges");

            migrationBuilder.DropColumn(
                name: "ProductGUID1",
                table: "PriceChanges");

            migrationBuilder.DropColumn(
                name: "UserProductGUID1",
                table: "PriceChanges");

            migrationBuilder.CreateIndex(
                name: "IX_PriceChanges_ProductGUID",
                table: "PriceChanges",
                column: "ProductGUID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceChanges_UserProductGUID",
                table: "PriceChanges",
                column: "UserProductGUID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceChanges_Products_ProductGUID",
                table: "PriceChanges",
                column: "ProductGUID",
                principalTable: "Products",
                principalColumn: "ProductGUID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceChanges_UserProducts_UserProductGUID",
                table: "PriceChanges",
                column: "UserProductGUID",
                principalTable: "UserProducts",
                principalColumn: "UserProductGUID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceChanges_Products_ProductGUID",
                table: "PriceChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceChanges_UserProducts_UserProductGUID",
                table: "PriceChanges");

            migrationBuilder.DropIndex(
                name: "IX_PriceChanges_ProductGUID",
                table: "PriceChanges");

            migrationBuilder.DropIndex(
                name: "IX_PriceChanges_UserProductGUID",
                table: "PriceChanges");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductGUID1",
                table: "PriceChanges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserProductGUID1",
                table: "PriceChanges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceChanges_ProductGUID1",
                table: "PriceChanges",
                column: "ProductGUID1");

            migrationBuilder.CreateIndex(
                name: "IX_PriceChanges_UserProductGUID1",
                table: "PriceChanges",
                column: "UserProductGUID1");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceChanges_Products_ProductGUID1",
                table: "PriceChanges",
                column: "ProductGUID1",
                principalTable: "Products",
                principalColumn: "ProductGUID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceChanges_UserProducts_UserProductGUID1",
                table: "PriceChanges",
                column: "UserProductGUID1",
                principalTable: "UserProducts",
                principalColumn: "UserProductGUID");
        }
    }
}
