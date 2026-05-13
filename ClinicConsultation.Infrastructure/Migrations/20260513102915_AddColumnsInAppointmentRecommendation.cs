using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicConsultation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsInAppointmentRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AiInsight",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Confidence",
                table: "Recommendations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstimateDuration",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetArea",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TreatmentOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecommendationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    EstimatedCostMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedCostMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentOption_Recommendations_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "Recommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOption_RecommendationId",
                table: "TreatmentOption",
                column: "RecommendationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreatmentOption");

            migrationBuilder.DropColumn(
                name: "AiInsight",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Confidence",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "EstimateDuration",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TargetArea",
                table: "Appointments");
        }
    }
}
