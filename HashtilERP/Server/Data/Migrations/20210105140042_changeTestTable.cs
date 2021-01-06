using Microsoft.EntityFrameworkCore.Migrations;

namespace HashtilERP.Server.Data.Migrations
{
    public partial class changeTestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KobiTestTable",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PassportNum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KobiTestTable", x => x.Id);
                });
        }
    }
}
