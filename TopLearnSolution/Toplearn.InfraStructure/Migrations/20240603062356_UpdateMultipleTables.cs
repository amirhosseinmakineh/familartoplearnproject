using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toplearn.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMultipleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Permissions");
        }
    }
}
