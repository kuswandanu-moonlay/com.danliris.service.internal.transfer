using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class Change_ReceivedQuantity_To_DOQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivedQuantity",
                table: "ExternalTransferOrderDetails",
                newName: "DOQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOQuantity",
                table: "ExternalTransferOrderDetails",
                newName: "ReceivedQuantity");
        }
    }
}
