using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.ExternalTransferOrderConfigs
{
    class ExternalTransferOrderDetailConfig : IEntityTypeConfiguration<ExternalTransferOrderDetail>
    {
        public void Configure(EntityTypeBuilder<ExternalTransferOrderDetail> builder)
        {
            builder.Property(p => p.ProductId).HasMaxLength(255);
            builder.Property(p => p.ProductCode).HasMaxLength(255);
            builder.Property(p => p.ProductName).HasMaxLength(255);
            builder.Property(p => p.DefaultUomId).HasMaxLength(255);
            builder.Property(p => p.DefaultUomUnit).HasMaxLength(255);
            builder.Property(p => p.DealUomId).HasMaxLength(255);
            builder.Property(p => p.DealUomUnit).HasMaxLength(255);
            builder.Property(p => p.Grade).HasMaxLength(255);
            builder.Property(p => p.ProductRemark).HasMaxLength(1000);
        }
    }
}
