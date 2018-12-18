using Microsoft.EntityFrameworkCore.Migrations;

namespace MyExam.Data.Migrations
{
    public partial class FixedReportEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_ReporterId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportedId",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_ReporterId",
                table: "Reports",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_ReporterId",
                table: "Reports");

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "Reports",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ReportedId",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_ReporterId",
                table: "Reports",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
