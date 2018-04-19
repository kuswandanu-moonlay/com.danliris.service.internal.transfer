using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class addTransferDeliveryOrder : Migration
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
                    ArrivalDate = table.Column<DateTime>(nullable: false),
                    DONo = table.Column<string>(maxLength: 255, nullable: true),
                    DOdate = table.Column<DateTime>(nullable: false),
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
                    Id = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    DOId = table.Column<int>(maxLength: 100, nullable: false),
                    ETOId = table.Column<string>(maxLength: 100, nullable: true),
                    ETONo = table.Column<string>(maxLength: 255, nullable: true),
                    ITOId = table.Column<string>(maxLength: 100, nullable: true),
                    ITONo = table.Column<string>(maxLength: 255, nullable: true),
                    TRId = table.Column<string>(maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_TransferDeliveryOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferDeliveryOrderItems_TransferDeliveryOrders_Id",
                        column: x => x.Id,
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
                    ETODetailId = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    ITODetailId = table.Column<string>(nullable: true),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductRemark = table.Column<string>(nullable: true),
                    ReceivedQuantity = table.Column<int>(nullable: false),
                    RemainingQuantity = table.Column<int>(nullable: false),
                    RequestedQuantity = table.Column<int>(nullable: false),
                    TRDetailId = table.Column<string>(nullable: true),
                    UnitReceivedQuantity = table.Column<int>(nullable: false, defaultValue: 0),
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
