using Microsoft.EntityFrameworkCore.Migrations;

namespace MyExam.Data.Migrations
{
    public partial class FixedTaskSectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSector_Tasks_TaskId",
                table: "TaskSector");

            migrationBuilder.DropColumn(
                name: "TaksId",
                table: "TaskSector");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TaskSector",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSector_Tasks_TaskId",
                table: "TaskSector",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSector_Tasks_TaskId",
                table: "TaskSector");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TaskSector",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TaksId",
                table: "TaskSector",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSector_Tasks_TaskId",
                table: "TaskSector",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
