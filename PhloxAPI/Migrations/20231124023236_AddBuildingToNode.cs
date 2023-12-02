using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBuildingToNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Building",
                table: "Nodes",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                table: "Nodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_BuildingId",
                table: "Nodes",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Buildings_BuildingId",
                table: "Nodes",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Buildings_BuildingId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_BuildingId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Building",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Nodes");
        }
    }
}
