using Microsoft.EntityFrameworkCore.Migrations;

namespace HashtilERP.Server.Data.Migrations
{
    public partial class ExtendIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hamama",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreenName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Hamama",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ScreenName",
                table: "AspNetUsers");
        }
    }
}
