using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class addStatusTR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequestDetails_TransferRequests_TransferRequestId",
                table: "TransferRequestDetails");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TransferRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TransferRequestId",
                table: "TransferRequestDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequestDetails_TransferRequests_TransferRequestId",
                table: "TransferRequestDetails",
                column: "TransferRequestId",
                principalTable: "TransferRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequestDetails_TransferRequests_TransferRequestId",
                table: "TransferRequestDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TransferRequests");

            migrationBuilder.AlterColumn<int>(
                name: "TransferRequestId",
                table: "TransferRequestDetails",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequestDetails_TransferRequests_TransferRequestId",
                table: "TransferRequestDetails",
                column: "TransferRequestId",
                principalTable: "TransferRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
