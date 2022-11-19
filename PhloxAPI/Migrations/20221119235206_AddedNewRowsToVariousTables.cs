using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhloxAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewRowsToVariousTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Building_Amenities_AmenityId",
                table: "Building");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Building",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "Building",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Reports");

            migrationBuilder.RenameTable(
                name: "Building",
                newName: "Buildings");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "Buildings",
                newName: "BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Building_AmenityId",
                table: "Buildings",
                newName: "IX_Buildings_BuildingId");

            migrationBuilder.AddColumn<Guid>(
                name: "AmenityId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ConnectedBuildingId",
                table: "Amenities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Amenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AmenityId",
                table: "Reports",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_ConnectedBuildingId",
                table: "Amenities",
                column: "ConnectedBuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Buildings_ConnectedBuildingId",
                table: "Amenities",
                column: "ConnectedBuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Buildings_BuildingId",
                table: "Buildings",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Amenities_AmenityId",
                table: "Reports",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Buildings_ConnectedBuildingId",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Buildings_BuildingId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Amenities_AmenityId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_AmenityId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_ConnectedBuildingId",
                table: "Amenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "AmenityId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ConnectedBuildingId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Amenities");

            migrationBuilder.RenameTable(
                name: "Buildings",
                newName: "Building");

            migrationBuilder.RenameColumn(
                name: "BuildingId",
                table: "Building",
                newName: "AmenityId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_BuildingId",
                table: "Building",
                newName: "IX_Building_AmenityId");

            migrationBuilder.AddColumn<string>(
                name: "Building",
                table: "Reports",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Building",
                table: "Building",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Amenities_AmenityId",
                table: "Building",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id");
        }
    }
}
