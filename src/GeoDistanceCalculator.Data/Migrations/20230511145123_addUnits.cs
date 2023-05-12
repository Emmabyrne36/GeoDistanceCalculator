using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoDistanceCalculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoordinateCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Coordinate1Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Coordinate2Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Distance = table.Column<double>(type: "REAL", nullable: false),
                    Units = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinateCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoordinateCalculations_Coordinates_Coordinate1Id",
                        column: x => x.Coordinate1Id,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoordinateCalculations_Coordinates_Coordinate2Id",
                        column: x => x.Coordinate2Id,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoordinateCalculations_Coordinate1Id",
                table: "CoordinateCalculations",
                column: "Coordinate1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CoordinateCalculations_Coordinate2Id",
                table: "CoordinateCalculations",
                column: "Coordinate2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoordinateCalculations");

            migrationBuilder.DropTable(
                name: "Coordinates");
        }
    }
}
