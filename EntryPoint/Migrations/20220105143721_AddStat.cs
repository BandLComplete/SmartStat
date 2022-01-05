using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntryPoint.Migrations
{
    public partial class AddStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthInMinutes",
                table: "Practices");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Length",
                table: "Practices",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    UserDateName = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.UserDateName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Practices");

            migrationBuilder.AddColumn<int>(
                name: "LengthInMinutes",
                table: "Practices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
