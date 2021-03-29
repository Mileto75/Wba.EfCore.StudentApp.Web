using Microsoft.EntityFrameworkCore.Migrations;

namespace Wba.EfCore.StudentApp.Web.Migrations
{
    public partial class teacherAddAddressMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Teachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Teachers");
        }
    }
}
