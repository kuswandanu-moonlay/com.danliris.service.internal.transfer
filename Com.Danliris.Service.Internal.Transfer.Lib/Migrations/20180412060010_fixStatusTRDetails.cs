using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class fixStatusTRDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TransferRequests");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TransferRequestDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TransferRequestDetails");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TransferRequests",
                nullable: true);
        }
    }
}
