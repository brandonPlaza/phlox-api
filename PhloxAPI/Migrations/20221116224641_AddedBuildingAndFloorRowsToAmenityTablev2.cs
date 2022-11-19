using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedBuildingAndFloorRowsToAmenityTablev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Building",
                table: "Amenities",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Amenities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Building",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Amenities");
        }
    }
}
