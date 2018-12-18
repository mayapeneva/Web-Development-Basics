using Microsoft.EntityFrameworkCore.Migrations;

namespace MyExam.Data.Migrations
{
    public partial class QuantityInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
