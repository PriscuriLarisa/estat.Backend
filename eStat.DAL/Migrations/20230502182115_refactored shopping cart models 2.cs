using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class refactoredshoppingcartmodels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartGUID);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Users_UserGUID",
                        column: x => x.UserGUID,
                        principalTable: "Users",
                        principalColumn: "UserGUID");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartProducts",
                columns: table => new
                {
                    ShoppingCartProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingCartGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserProductGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartProducts", x => x.ShoppingCartProductGUID);
                    table.ForeignKey(
                        name: "FK_ShoppingCartProducts_ShoppingCarts_ShoppingCartGUID",
                        column: x => x.ShoppingCartGUID,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartGUID");
                    table.ForeignKey(
                        name: "FK_ShoppingCartProducts_UserProducts_UserProductGUID",
                        column: x => x.UserProductGUID,
                        principalTable: "UserProducts",
                        principalColumn: "UserProductGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducts_ShoppingCartGUID",
                table: "ShoppingCartProducts",
                column: "ShoppingCartGUID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducts_UserProductGUID",
                table: "ShoppingCartProducts",
                column: "UserProductGUID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserGUID",
                table: "ShoppingCarts",
                column: "UserGUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartProducts");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
