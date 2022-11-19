using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAmenityRowConnectedAmenitiesToConnectedBuildings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Amenities_AmenityId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_AmenityId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "AmenityId",
                table: "Amenities");

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    AmenityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Building_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Building_AmenityId",
                table: "Building",
                column: "AmenityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.AddColumn<Guid>(
                name: "AmenityId",
                table: "Amenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_AmenityId",
                table: "Amenities",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Amenities_AmenityId",
                table: "Amenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id");
        }
    }
}
