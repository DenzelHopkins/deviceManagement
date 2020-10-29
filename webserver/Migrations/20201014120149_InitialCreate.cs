using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    GeräteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modell = table.Column<string>(nullable: false),
                    Baujahr = table.Column<int>(nullable: false),
                    Seriennummer = table.Column<int>(nullable: false),
                    Verfügbar = table.Column<bool>(nullable: false),
                    Historie = table.Column<string>(nullable: false),
                    DatumAusleihe = table.Column<DateTime>(nullable: false),
                    AusgeliehenAn = table.Column<string>(nullable: false),
                    Bemerkung = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.GeräteID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
