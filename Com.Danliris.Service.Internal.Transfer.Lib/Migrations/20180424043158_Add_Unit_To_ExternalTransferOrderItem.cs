using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class Add_Unit_To_ExternalTransferOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "ExternalTransferOrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "ExternalTransferOrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "ExternalTransferOrderItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "ExternalTransferOrderItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ExternalTransferOrderItems");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "ExternalTransferOrderItems");
        }
    }
}
