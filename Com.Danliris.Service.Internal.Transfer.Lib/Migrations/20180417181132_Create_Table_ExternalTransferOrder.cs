using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class Create_Table_ExternalTransferOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalTransferOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencyDescription = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencyRate = table.Column<string>(maxLength: 255, nullable: true),
                    CurrencySymbol = table.Column<string>(maxLength: 255, nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    ExternalTransferOrderNo = table.Column<string>(nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    IsPosted = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    SupplierCode = table.Column<string>(maxLength: 255, nullable: true),
                    SupplierId = table.Column<string>(maxLength: 255, nullable: true),
                    SupplierName = table.Column<string>(maxLength: 255, nullable: true),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTransferOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalTransferOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    ExternalTransferOrderId = table.Column<int>(nullable: false),
                    InternalTransferOrderId = table.Column<int>(nullable: false),
                    InternalTransferOrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    TransferRequestId = table.Column<int>(nullable: false),
                    TransferRequestNo = table.Column<string>(maxLength: 255, nullable: true),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTransferOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalTransferOrderItems_ExternalTransferOrders_ExternalTransferOrderId",
                        column: x => x.ExternalTransferOrderId,
                        principalTable: "ExternalTransferOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalTransferOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Convertion = table.Column<double>(nullable: false),
                    DealQuantity = table.Column<double>(nullable: false),
                    DealUomId = table.Column<string>(maxLength: 255, nullable: true),
                    DealUomUnit = table.Column<string>(maxLength: 255, nullable: true),
                    DefaultQuantity = table.Column<double>(nullable: false),
                    DefaultUomId = table.Column<string>(maxLength: 255, nullable: true),
                    DefaultUomUnit = table.Column<string>(maxLength: 255, nullable: true),
                    ExternalTransferOrderItemId = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(maxLength: 255, nullable: true),
                    InternalTransferOrderDetailId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductId = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 255, nullable: true),
                    ProductRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    ReceivedQuantity = table.Column<double>(nullable: false),
                    RemainingQuantity = table.Column<double>(nullable: false),
                    TransferRequestDetailId = table.Column<int>(nullable: false),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTransferOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalTransferOrderDetails_ExternalTransferOrderItems_ExternalTransferOrderItemId",
                        column: x => x.ExternalTransferOrderItemId,
                        principalTable: "ExternalTransferOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalTransferOrderDetails_ExternalTransferOrderItemId",
                table: "ExternalTransferOrderDetails",
                column: "ExternalTransferOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalTransferOrderItems_ExternalTransferOrderId",
                table: "ExternalTransferOrderItems",
                column: "ExternalTransferOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalTransferOrderDetails");

            migrationBuilder.DropTable(
                name: "ExternalTransferOrderItems");

            migrationBuilder.DropTable(
                name: "ExternalTransferOrders");
        }
    }
}
