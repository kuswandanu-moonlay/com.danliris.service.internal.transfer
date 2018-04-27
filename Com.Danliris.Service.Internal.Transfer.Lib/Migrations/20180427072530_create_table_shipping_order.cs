using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class create_table_shipping_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferShippingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    IsPosted = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    SODate = table.Column<DateTime>(nullable: false),
                    SONo = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_TransferShippingOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferShippingOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DOId = table.Column<int>(nullable: false),
                    DONo = table.Column<string>(nullable: true),
                    ETOId = table.Column<int>(nullable: false),
                    ETONo = table.Column<string>(maxLength: 255, nullable: true),
                    ITOId = table.Column<int>(nullable: false),
                    ITONo = table.Column<string>(maxLength: 255, nullable: true),
                    SOId = table.Column<int>(nullable: false),
                    TRId = table.Column<int>(nullable: false),
                    TRNo = table.Column<string>(maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_TransferShippingOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferShippingOrderItems_TransferShippingOrders_SOId",
                        column: x => x.SOId,
                        principalTable: "TransferShippingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferShippingOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DODetailId = table.Column<int>(nullable: false),
                    DOQuantity = table.Column<double>(nullable: false),
                    DeliveryQuantity = table.Column<double>(nullable: false),
                    ETODetailId = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(maxLength: 255, nullable: true),
                    ITODetailId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductId = table.Column<string>(maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(maxLength: 255, nullable: true),
                    ProductRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    ReceiptQuantity = table.Column<double>(nullable: false),
                    RemainingQuantity = table.Column<double>(nullable: false),
                    SOItemId = table.Column<int>(nullable: false),
                    TRDetailId = table.Column<int>(nullable: false),
                    UomId = table.Column<string>(maxLength: 255, nullable: true),
                    UomUnit = table.Column<string>(maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_TransferShippingOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferShippingOrderDetails_TransferShippingOrderItems_SOItemId",
                        column: x => x.SOItemId,
                        principalTable: "TransferShippingOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferShippingOrderDetails_SOItemId",
                table: "TransferShippingOrderDetails",
                column: "SOItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferShippingOrderItems_SOId",
                table: "TransferShippingOrderItems",
                column: "SOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferShippingOrderDetails");

            migrationBuilder.DropTable(
                name: "TransferShippingOrderItems");

            migrationBuilder.DropTable(
                name: "TransferShippingOrders");
        }
    }
}
