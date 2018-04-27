using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class create_table_transferdeliveryorder_and_shipping_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferDeliveryOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DONo = table.Column<string>(maxLength: 255, nullable: true),
                    DOdate = table.Column<DateTime>(nullable: false),
                    IsPosted = table.Column<bool>(nullable: false),
                    OrderDivisionCode = table.Column<string>(nullable: true),
                    OrderDivisionId = table.Column<string>(nullable: true),
                    OrderDivisionName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    SupplierCode = table.Column<string>(nullable: true),
                    SupplierId = table.Column<string>(nullable: true),
                    SupplierName = table.Column<string>(maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_TransferDeliveryOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferDeliveryOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DOId = table.Column<int>(maxLength: 100, nullable: false),
                    ETOId = table.Column<int>(maxLength: 100, nullable: false),
                    ETONo = table.Column<string>(maxLength: 255, nullable: true),
                    ITOId = table.Column<int>(maxLength: 100, nullable: false),
                    ITONo = table.Column<string>(maxLength: 255, nullable: true),
                    TRId = table.Column<int>(maxLength: 100, nullable: false),
                    TRNo = table.Column<string>(maxLength: 255, nullable: true),
                    UnitCode = table.Column<string>(nullable: true),
                    UnitId = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_TransferDeliveryOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferDeliveryOrderItems_TransferDeliveryOrders_DOId",
                        column: x => x.DOId,
                        principalTable: "TransferDeliveryOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferDeliveryOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DOItemId = table.Column<int>(nullable: false),
                    DOQuantity = table.Column<int>(nullable: false),
                    ETODetailId = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    ITODetailId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductRemark = table.Column<string>(nullable: true),
                    RemainingQuantity = table.Column<int>(nullable: false),
                    RequestedQuantity = table.Column<int>(nullable: false),
                    ShippingOrderQuantity = table.Column<int>(nullable: false, defaultValue: 0),
                    TRDetailId = table.Column<int>(nullable: false),
                    UomId = table.Column<string>(nullable: true),
                    UomUnit = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_TransferDeliveryOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferDeliveryOrderDetails_TransferDeliveryOrderItems_DOItemId",
                        column: x => x.DOItemId,
                        principalTable: "TransferDeliveryOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferDeliveryOrderDetails_DOItemId",
                table: "TransferDeliveryOrderDetails",
                column: "DOItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDeliveryOrderItems_DOId",
                table: "TransferDeliveryOrderItems",
                column: "DOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferDeliveryOrderDetails");

            migrationBuilder.DropTable(
                name: "TransferDeliveryOrderItems");

            migrationBuilder.DropTable(
                name: "TransferDeliveryOrders");
        }
    }
}
