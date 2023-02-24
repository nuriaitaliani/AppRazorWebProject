using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppRazorWeb.Migrations.Migrations.Development.SqlServer
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    description = table.Column<string>(type: "nvarchar(144)", maxLength: 144, nullable: true),
                    place = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    student_name = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    daily_lesson = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: true),
                    student_course = table.Column<int>(type: "int", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    dni = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    email = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.UniqueConstraint("AK_user_dni", x => x.dni)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "schedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start = table.Column<TimeSpan>(type: "time", nullable: false),
                    end = table.Column<TimeSpan>(type: "time", nullable: false),
                    activity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    day_of_week = table.Column<byte>(type: "tinyint", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "fk_sch_act",
                        column: x => x.activity_id,
                        principalTable: "activity",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_schedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    schedule_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userschedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_Schedules_UserSchedule",
                        column: x => x.schedule_id,
                        principalTable: "schedule",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Users_UserSchedule",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_schedule_activity_id",
                table: "schedule",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "IX_userschedule_schedule_id",
                table: "user_schedule",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_userschedule_user_id",
                table: "user_schedule",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_schedule");

            migrationBuilder.DropTable(
                name: "schedule");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "activity");
        }
    }
}
