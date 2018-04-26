using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class Change_Supplier_To_Division : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierName",
                table: "ExternalTransferOrders",
                newName: "DivisionName");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "ExternalTransferOrders",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "SupplierCode",
                table: "ExternalTransferOrders",
                newName: "DivisionCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DivisionName",
                table: "ExternalTransferOrders",
                newName: "SupplierName");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "ExternalTransferOrders",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "DivisionCode",
                table: "ExternalTransferOrders",
                newName: "SupplierCode");
        }
    }
}
