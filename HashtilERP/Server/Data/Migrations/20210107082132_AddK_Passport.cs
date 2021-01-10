using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HashtilERP.Server.Data.Migrations
{
    public partial class AddK_Passport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "K_Passport",
                columns: table => new
                {
                    K_PassportId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrowingRoom = table.Column<int>(type: "int", nullable: false),
                    SowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hamama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gamlon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MagashAmount = table.Column<int>(type: "int", nullable: false),
                    PlantsAmount = table.Column<int>(type: "int", nullable: false),
                    IsNeedToBeAudit = table.Column<bool>(type: "bit", nullable: false),
                    IsNeedToBeChecked = table.Column<bool>(type: "bit", nullable: false),
                    PassportStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportAVG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_K_Passport", x => x.K_PassportId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "K_Passport");
        }
    }
}
