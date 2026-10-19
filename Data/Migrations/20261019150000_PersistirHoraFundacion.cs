using HeroesFluent.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroesFluent.Data.Migrations;

[DbContext(typeof(HeroesDbContext))]
[Migration("20261019150000_PersistirHoraFundacion")]
public partial class PersistirHoraFundacion : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "FechaFundacion",
            table: "Equipos",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "date");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "FechaFundacion",
            table: "Equipos",
            type: "date",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");
    }
}
