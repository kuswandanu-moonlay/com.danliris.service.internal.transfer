﻿// <auto-generated />
using Com.Danliris.Service.Internal.Transfer.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Migrations
{
    [DbContext(typeof(InternalTransferDbContext))]
    partial class InternalTransferDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel.TransferDeliveryOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("ArrivalDate");

                    b.Property<string>("DONo")
                        .HasMaxLength(255);

                    b.Property<DateTime>("DOdate");

                    b.Property<string>("Remark")
                        .HasMaxLength(500);

                    b.Property<string>("SupplierCode");

                    b.Property<string>("SupplierId");

                    b.Property<string>("SupplierName")
                        .HasMaxLength(500);

                    b.Property<string>("_CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_CreatedUtc");

                    b.Property<string>("_DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_DeletedUtc");

                    b.Property<bool>("_IsDeleted");

                    b.Property<string>("_LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_LastModifiedUtc");

                    b.HasKey("Id");

                    b.ToTable("TransferDeliveryOrders");
                });

            modelBuilder.Entity("Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel.TransferDeliveryOrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int>("DOItemId");

                    b.Property<string>("ETODetailId");

                    b.Property<string>("Grade")
                        .HasMaxLength(100);

                    b.Property<string>("ITODetailId");

                    b.Property<string>("Note")
                        .HasMaxLength(500);

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductId");

                    b.Property<string>("ProductName");

                    b.Property<string>("ProductRemark");

                    b.Property<int>("ReceivedQuantity");

                    b.Property<int>("RemainingQuantity");

                    b.Property<int>("RequestedQuantity");

                    b.Property<string>("TRDetailId");

                    b.Property<int>("UnitReceivedQuantity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("UomId");

                    b.Property<string>("UomUnit");

                    b.Property<string>("_CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_CreatedUtc");

                    b.Property<string>("_DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_DeletedUtc");

                    b.Property<bool>("_IsDeleted");

                    b.Property<string>("_LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_LastModifiedUtc");

                    b.HasKey("Id");

                    b.HasIndex("DOItemId");

                    b.ToTable("TransferDeliveryOrderDetails");
                });

            modelBuilder.Entity("Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel.TransferDeliveryOrderItem", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("Active");

                    b.Property<int>("DOId")
                        .HasMaxLength(100);

                    b.Property<string>("ETOId")
                        .HasMaxLength(100);

                    b.Property<string>("ETONo")
                        .HasMaxLength(255);

                    b.Property<string>("ITOId")
                        .HasMaxLength(100);

                    b.Property<string>("ITONo")
                        .HasMaxLength(255);

                    b.Property<string>("TRId")
                        .HasMaxLength(100);

                    b.Property<string>("TRNo")
                        .HasMaxLength(255);

                    b.Property<string>("_CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_CreatedUtc");

                    b.Property<string>("_DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_DeletedUtc");

                    b.Property<bool>("_IsDeleted");

                    b.Property<string>("_LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("_LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("_LastModifiedUtc");

                    b.HasKey("Id");

                    b.ToTable("TransferDeliveryOrderItems");
                });

            modelBuilder.Entity("Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel.TransferDeliveryOrderDetail", b =>
                {
                    b.HasOne("Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel.TransferDeliveryOrderItem", "transferDeliveryOrderItem")
                        .WithMany("transferDeliveryOrderDetail")
                        .HasForeignKey("DOItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel.TransferDeliveryOrderItem", b =>
                {
                    b.HasOne("Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel.TransferDeliveryOrder", "transferDeliveryOrder")
                        .WithMany("TransferDeliveryOrderItem")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
