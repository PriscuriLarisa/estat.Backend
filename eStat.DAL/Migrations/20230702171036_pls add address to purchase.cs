using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eStat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class plsaddaddresstopurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Purchases");
        }
    }
}
