using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ScheduleDurationChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "schedule");

            migrationBuilder.AddColumn<int>(
                name: "duration",
                table: "weekly_log",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "weekly_log");

            migrationBuilder.AddColumn<int>(
                name: "duration",
                table: "schedule",
                type: "integer",
                nullable: true);
        }
    }
}
