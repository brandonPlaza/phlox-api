using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedHelpRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Amenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HelpRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitute = table.Column<double>(type: "float", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAccepted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeCancelled = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_UserId",
                table: "Amenities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Users_UserId",
                table: "Amenities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Users_UserId",
                table: "Amenities");

            migrationBuilder.DropTable(
                name: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_UserId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Amenities");
        }
    }
}
