using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalTransferOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AutoIncrementNumber = table.Column<int>(nullable: false),
                    CategoryCode = table.Column<string>(nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    DivisionCode = table.Column<string>(nullable: true),
                    DivisionId = table.Column<string>(nullable: true),
                    DivisionName = table.Column<string>(nullable: true),
                    ITONo = table.Column<string>(maxLength: 50, nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false),
                    IsPost = table.Column<bool>(nullable: false),
                    TRDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    RequestedArrivalDate = table.Column<DateTime>(nullable: false),
                    TRId = table.Column<int>(nullable: false),
                    TRNo = table.Column<string>(maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_InternalTransferOrders", x => x.Id);
                });



            migrationBuilder.CreateTable(
                name: "InternalTransferOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    ITOId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductRemark = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
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
                    table.PrimaryKey("PK_InternalTransferOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalTransferOrderDetails_InternalTransferOrders_ITOId",
                        column: x => x.ITOId,
                        principalTable: "InternalTransferOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_InternalTransferOrderDetails_ITOId",
                table: "InternalTransferOrderDetails",
                column: "ITOId");
        }

      

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalTransferOrderDetails");

           

            migrationBuilder.DropTable(
                name: "InternalTransferOrders");

            
        }
    }
}
