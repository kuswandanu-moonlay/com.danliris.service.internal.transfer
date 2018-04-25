using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class Add_OrderDivision_And_DeliveryDivision_ETO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DivisionName",
                table: "ExternalTransferOrders",
                newName: "DeliveryDivisionName");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "ExternalTransferOrders",
                newName: "DeliveryDivisionId");

            migrationBuilder.RenameColumn(
                name: "DivisionCode",
                table: "ExternalTransferOrders",
                newName: "DeliveryDivisionCode");

            migrationBuilder.AddColumn<string>(
                name: "OrderDivisionCode",
                table: "ExternalTransferOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderDivisionId",
                table: "ExternalTransferOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderDivisionName",
                table: "ExternalTransferOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDivisionCode",
                table: "ExternalTransferOrders");

            migrationBuilder.DropColumn(
                name: "OrderDivisionId",
                table: "ExternalTransferOrders");

            migrationBuilder.DropColumn(
                name: "OrderDivisionName",
                table: "ExternalTransferOrders");

            migrationBuilder.RenameColumn(
                name: "DeliveryDivisionName",
                table: "ExternalTransferOrders",
                newName: "DivisionName");

            migrationBuilder.RenameColumn(
                name: "DeliveryDivisionId",
                table: "ExternalTransferOrders",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "DeliveryDivisionCode",
                table: "ExternalTransferOrders",
                newName: "DivisionCode");
        }
    }
}
