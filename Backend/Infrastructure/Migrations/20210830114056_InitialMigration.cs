using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "label",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "person_label",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    label_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_label", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_label_label_label_id",
                        column: x => x.label_id,
                        principalTable: "label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_label_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "weekly_log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    day_of_week = table.Column<string>(type: "text", nullable: false),
                    beneficiary_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weekly_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_weekly_log_person_beneficiary_id",
                        column: x => x.beneficiary_id,
                        principalTable: "person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_role_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: true),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    weekly_log_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedule_person_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedule_weekly_log_weekly_log_id",
                        column: x => x.weekly_log_id,
                        principalTable: "weekly_log",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_person_label_label_id",
                table: "person_label",
                column: "label_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_label_person_id",
                table: "person_label",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_role_person_id",
                table: "person_role",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_role_role_id",
                table: "person_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_volunteer_id",
                table: "schedule",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_weekly_log_id",
                table: "schedule",
                column: "weekly_log_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_person_id",
                table: "users",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_weekly_log_beneficiary_id",
                table: "weekly_log",
                column: "beneficiary_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_label");

            migrationBuilder.DropTable(
                name: "person_role");

            migrationBuilder.DropTable(
                name: "schedule");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "label");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "weekly_log");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
