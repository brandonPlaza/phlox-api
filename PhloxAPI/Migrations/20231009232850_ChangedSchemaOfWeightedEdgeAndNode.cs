using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSchemaOfWeightedEdgeAndNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Nodes_NodeId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_NodeId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "Nodes");

            migrationBuilder.AddColumn<int>(
                name: "FirstNodeToSecondCardinal",
                table: "WeightedEdges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondNodeToFirstCardinal",
                table: "WeightedEdges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNodeToSecondCardinal",
                table: "WeightedEdges");

            migrationBuilder.DropColumn(
                name: "SecondNodeToFirstCardinal",
                table: "WeightedEdges");

            migrationBuilder.AddColumn<Guid>(
                name: "NodeId",
                table: "Nodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_NodeId",
                table: "Nodes",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Nodes_NodeId",
                table: "Nodes",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id");
        }
    }
}
