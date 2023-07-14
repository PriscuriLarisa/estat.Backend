using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedpricepredictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PricePredictions",
                columns: table => new
                {
                    PricePredictionGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NbOfSearchesLastMonth = table.Column<int>(type: "int", nullable: false),
                    NbOfPurchasesLastMonth = table.Column<int>(type: "int", nullable: false),
                    CurrentAveragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AveragePriceLastMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentAverageStockPerRetailer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AverageStockPerRetailerLastMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MyStock = table.Column<int>(type: "int", nullable: false),
                    MyCurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MyAveragePriceLastMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MySellsLastMonth = table.Column<int>(type: "int", nullable: false),
                    PredictedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePredictions", x => x.PricePredictionGUID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricePredictions");
        }
    }
}
