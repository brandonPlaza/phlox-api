using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNodeToHelpRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Buildings_BuildingId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_BuildingId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "Longitute",
                table: "HelpRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "NodeId",
                table: "HelpRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_NodeId",
                table: "HelpRequests",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Nodes_NodeId",
                table: "HelpRequests",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Nodes_NodeId",
                table: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_NodeId",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "HelpRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                table: "Nodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "HelpRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitute",
                table: "HelpRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
    }
}
