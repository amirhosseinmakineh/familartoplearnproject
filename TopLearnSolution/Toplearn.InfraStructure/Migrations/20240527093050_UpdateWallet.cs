using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toplearn.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Wallets");
        }
    }
}
