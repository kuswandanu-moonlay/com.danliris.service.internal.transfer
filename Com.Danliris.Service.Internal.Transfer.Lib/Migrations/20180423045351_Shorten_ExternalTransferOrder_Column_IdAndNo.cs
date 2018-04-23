using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class Shorten_ExternalTransferOrder_Column_IdAndNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalTransferOrderDetails_ExternalTransferOrderItems_ExternalTransferOrderItemId",
                table: "ExternalTransferOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalTransferOrderItems_ExternalTransferOrders_ExternalTransferOrderId",
                table: "ExternalTransferOrderItems");

            migrationBuilder.RenameColumn(
                name: "ExternalTransferOrderNo",
                table: "ExternalTransferOrders",
                newName: "ETONo");

            migrationBuilder.RenameColumn(
                name: "TransferRequestNo",
                table: "ExternalTransferOrderItems",
                newName: "TRNo");

            migrationBuilder.RenameColumn(
                name: "TransferRequestId",
                table: "ExternalTransferOrderItems",
                newName: "TRId");

            migrationBuilder.RenameColumn(
                name: "InternalTransferOrderNo",
                table: "ExternalTransferOrderItems",
                newName: "ITONo");

            migrationBuilder.RenameColumn(
                name: "InternalTransferOrderId",
                table: "ExternalTransferOrderItems",
                newName: "ITOId");

            migrationBuilder.RenameColumn(
                name: "ExternalTransferOrderId",
                table: "ExternalTransferOrderItems",
                newName: "ETOId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalTransferOrderItems_ExternalTransferOrderId",
                table: "ExternalTransferOrderItems",
                newName: "IX_ExternalTransferOrderItems_ETOId");

            migrationBuilder.RenameColumn(
                name: "TransferRequestDetailId",
                table: "ExternalTransferOrderDetails",
                newName: "TRDetailId");

            migrationBuilder.RenameColumn(
                name: "InternalTransferOrderDetailId",
                table: "ExternalTransferOrderDetails",
                newName: "ITODetailId");

            migrationBuilder.RenameColumn(
                name: "ExternalTransferOrderItemId",
                table: "ExternalTransferOrderDetails",
                newName: "ETOItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalTransferOrderDetails_ExternalTransferOrderItemId",
                table: "ExternalTransferOrderDetails",
                newName: "IX_ExternalTransferOrderDetails_ETOItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalTransferOrderDetails_ExternalTransferOrderItems_ETOItemId",
                table: "ExternalTransferOrderDetails",
                column: "ETOItemId",
                principalTable: "ExternalTransferOrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalTransferOrderItems_ExternalTransferOrders_ETOId",
                table: "ExternalTransferOrderItems",
                column: "ETOId",
                principalTable: "ExternalTransferOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalTransferOrderDetails_ExternalTransferOrderItems_ETOItemId",
                table: "ExternalTransferOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalTransferOrderItems_ExternalTransferOrders_ETOId",
                table: "ExternalTransferOrderItems");

            migrationBuilder.RenameColumn(
                name: "ETONo",
                table: "ExternalTransferOrders",
                newName: "ExternalTransferOrderNo");

            migrationBuilder.RenameColumn(
                name: "TRNo",
                table: "ExternalTransferOrderItems",
                newName: "TransferRequestNo");

            migrationBuilder.RenameColumn(
                name: "TRId",
                table: "ExternalTransferOrderItems",
                newName: "TransferRequestId");

            migrationBuilder.RenameColumn(
                name: "ITONo",
                table: "ExternalTransferOrderItems",
                newName: "InternalTransferOrderNo");

            migrationBuilder.RenameColumn(
                name: "ITOId",
                table: "ExternalTransferOrderItems",
                newName: "InternalTransferOrderId");

            migrationBuilder.RenameColumn(
                name: "ETOId",
                table: "ExternalTransferOrderItems",
                newName: "ExternalTransferOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalTransferOrderItems_ETOId",
                table: "ExternalTransferOrderItems",
                newName: "IX_ExternalTransferOrderItems_ExternalTransferOrderId");

            migrationBuilder.RenameColumn(
                name: "TRDetailId",
                table: "ExternalTransferOrderDetails",
                newName: "TransferRequestDetailId");

            migrationBuilder.RenameColumn(
                name: "ITODetailId",
                table: "ExternalTransferOrderDetails",
                newName: "InternalTransferOrderDetailId");

            migrationBuilder.RenameColumn(
                name: "ETOItemId",
                table: "ExternalTransferOrderDetails",
                newName: "ExternalTransferOrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalTransferOrderDetails_ETOItemId",
                table: "ExternalTransferOrderDetails",
                newName: "IX_ExternalTransferOrderDetails_ExternalTransferOrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalTransferOrderDetails_ExternalTransferOrderItems_ExternalTransferOrderItemId",
                table: "ExternalTransferOrderDetails",
                column: "ExternalTransferOrderItemId",
                principalTable: "ExternalTransferOrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalTransferOrderItems_ExternalTransferOrders_ExternalTransferOrderId",
                table: "ExternalTransferOrderItems",
                column: "ExternalTransferOrderId",
                principalTable: "ExternalTransferOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
