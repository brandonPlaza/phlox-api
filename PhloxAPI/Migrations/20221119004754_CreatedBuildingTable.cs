using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBuildingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Building",
                newName: "Letter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Letter",
                table: "Building",
                newName: "Name");
        }
    }
}
