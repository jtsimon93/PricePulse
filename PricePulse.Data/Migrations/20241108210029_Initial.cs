using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricePulse.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumerPriceIndexSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SeriesTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Item = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsSeasonallyAdjusted = table.Column<bool>(type: "bit", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnergyItem = table.Column<bool>(type: "bit", nullable: false),
                    IsFoodItem = table.Column<bool>(type: "bit", nullable: false),
                    IsHousingItem = table.Column<bool>(type: "bit", nullable: false),
                    IsMedicalItem = table.Column<bool>(type: "bit", nullable: false),
                    IsTransportationItem = table.Column<bool>(type: "bit", nullable: false),
                    IsApparelItem = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerPriceIndexSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerPriceIndexEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerPriceIndexSeriesId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateRetrieved = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerPriceIndexEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerPriceIndexEntries_ConsumerPriceIndexSeries_ConsumerPriceIndexSeriesId",
                        column: x => x.ConsumerPriceIndexSeriesId,
                        principalTable: "ConsumerPriceIndexSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerPriceIndexEntries_ConsumerPriceIndexSeriesId_Year_Month",
                table: "ConsumerPriceIndexEntries",
                columns: new[] { "ConsumerPriceIndexSeriesId", "Year", "Month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerPriceIndexSeries_SeriesId",
                table: "ConsumerPriceIndexSeries",
                column: "SeriesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumerPriceIndexEntries");

            migrationBuilder.DropTable(
                name: "ConsumerPriceIndexSeries");
        }
    }
}
