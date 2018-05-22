using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class change_column_table_transferdeliveryorderitemstransferdeliveryorderdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TRId",
                table: "TransferDeliveryOrderItems");

            migrationBuilder.DropColumn(
                name: "TRNo",
                table: "TransferDeliveryOrderItems");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "TransferDeliveryOrderItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "TransferDeliveryOrderItems");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "TransferDeliveryOrderItems");

            migrationBuilder.AddColumn<int>(
                name: "TRId",
                table: "TransferDeliveryOrderDetails",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TRNo",
                table: "TransferDeliveryOrderDetails",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "TransferDeliveryOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "TransferDeliveryOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "TransferDeliveryOrderDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TRId",
                table: "TransferDeliveryOrderDetails");

            migrationBuilder.DropColumn(
                name: "TRNo",
                table: "TransferDeliveryOrderDetails");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "TransferDeliveryOrderDetails");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "TransferDeliveryOrderDetails");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "TransferDeliveryOrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "TRId",
                table: "TransferDeliveryOrderItems",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TRNo",
                table: "TransferDeliveryOrderItems",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "TransferDeliveryOrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "TransferDeliveryOrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "TransferDeliveryOrderItems",
                nullable: true);
        }
    }
}
