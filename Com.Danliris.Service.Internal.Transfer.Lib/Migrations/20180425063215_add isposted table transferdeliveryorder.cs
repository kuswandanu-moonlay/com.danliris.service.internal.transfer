using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class addispostedtabletransferdeliveryorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "TransferDeliveryOrders");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "TransferDeliveryOrderDetails");

            migrationBuilder.RenameColumn(
                name: "UnitReceivedQuantity",
                table: "TransferDeliveryOrderDetails",
                newName: "ShippingOrderQuantity");

            migrationBuilder.RenameColumn(
                name: "ReceivedQuantity",
                table: "TransferDeliveryOrderDetails",
                newName: "DOQuantity");

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "TransferDeliveryOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrderDivisionCode",
                table: "TransferDeliveryOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderDivisionId",
                table: "TransferDeliveryOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderDivisionName",
                table: "TransferDeliveryOrders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "ExternalTransferOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "ExternalTransferOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "ExternalTransferOrders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "TransferDeliveryOrders");

            migrationBuilder.DropColumn(
                name: "OrderDivisionCode",
                table: "TransferDeliveryOrders");

            migrationBuilder.DropColumn(
                name: "OrderDivisionId",
                table: "TransferDeliveryOrders");

            migrationBuilder.DropColumn(
                name: "OrderDivisionName",
                table: "TransferDeliveryOrders");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "ExternalTransferOrders");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "ExternalTransferOrders");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "ExternalTransferOrders");

            migrationBuilder.RenameColumn(
                name: "ShippingOrderQuantity",
                table: "TransferDeliveryOrderDetails",
                newName: "UnitReceivedQuantity");

            migrationBuilder.RenameColumn(
                name: "DOQuantity",
                table: "TransferDeliveryOrderDetails",
                newName: "ReceivedQuantity");

            migrationBuilder.RenameColumn(
                name: "isPosted",
                table: "ExternalTransferOrders",
                newName: "IsPosted");

            migrationBuilder.RenameColumn(
                name: "isClosed",
                table: "ExternalTransferOrders",
                newName: "IsClosed");

            migrationBuilder.RenameColumn(
                name: "isCanceled",
                table: "ExternalTransferOrders",
                newName: "IsCanceled");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "TransferDeliveryOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TransferDeliveryOrderDetails",
                maxLength: 500,
                nullable: true);
        }
    }
}
