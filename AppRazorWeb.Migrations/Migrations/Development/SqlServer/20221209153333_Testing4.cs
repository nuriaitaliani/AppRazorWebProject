using Microsoft.EntityFrameworkCore.Migrations;

namespace AppRazorWeb.Migrations.Migrations.Development.SqlServer
{
    public partial class Testing4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "user",
                newName: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "user",
                newName: "phone_number");
        }
    }
}
