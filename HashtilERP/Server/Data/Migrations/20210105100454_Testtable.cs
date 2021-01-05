using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HashtilERP.Server.Data.Migrations
{
    public partial class Testtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "KobiPassportModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassportNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrowingRoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hamama = table.Column<int>(type: "int", nullable: true),
                    Gamlon = table.Column<int>(type: "int", nullable: true),
                    SowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarketDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MagashAmount = table.Column<int>(type: "int", nullable: false),
                    PlantsAmount = table.Column<int>(type: "int", nullable: false),
                    Avarage = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNeedToBeAudit = table.Column<bool>(type: "bit", nullable: false),
                    IsNeedToBeChecked = table.Column<bool>(type: "bit", nullable: false),
                    Gidul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrowingDaysToMarket = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KobiPassportModel", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KobiPassportModel");

            migrationBuilder.DropTable(
                name: "KobiTestTable");

            
        }
    }
}
