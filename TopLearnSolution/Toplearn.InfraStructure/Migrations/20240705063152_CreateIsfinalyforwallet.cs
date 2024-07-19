using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toplearn.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateIsfinalyforwallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Orders_OrderId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_OrderId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Courses");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinaly",
                table: "Wallets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "OrderSum",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourseId",
                table: "Orders",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Courses_CourseId",
                table: "Orders",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Courses_CourseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CourseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsFinaly",
                table: "Wallets");

            migrationBuilder.AlterColumn<float>(
                name: "OrderSum",
                table: "OrderDetails",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Courses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OrderId",
                table: "Courses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Orders_OrderId",
                table: "Courses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
