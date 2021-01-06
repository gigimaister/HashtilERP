using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HashtilERP.Server.Data.Migrations
{
    public partial class no2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KobiPassportModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KobiPassportModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avarage = table.Column<int>(type: "int", nullable: true),
                    Gamlon = table.Column<int>(type: "int", nullable: true),
                    Gidul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrowingDaysToMarket = table.Column<int>(type: "int", nullable: false),
                    GrowingRoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hamama = table.Column<int>(type: "int", nullable: true),
                    IsNeedToBeAudit = table.Column<bool>(type: "bit", nullable: false),
                    IsNeedToBeChecked = table.Column<bool>(type: "bit", nullable: false),
                    MagashAmount = table.Column<int>(type: "int", nullable: false),
                    MarketDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantsAmount = table.Column<int>(type: "int", nullable: false),
                    SowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KobiPassportModel", x => x.Id);
                });
        }
    }
}
