using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferShippingOrderConfigs
{
    public class TransferShippingOrderDetailConfig : IEntityTypeConfiguration<TransferShippingOrderDetail>
    {
        public void Configure(EntityTypeBuilder<TransferShippingOrderDetail> builder)
        {
            builder.Property(p => p.ProductId).HasMaxLength(255);
            builder.Property(p => p.ProductCode).HasMaxLength(255);
            builder.Property(p => p.ProductName).HasMaxLength(255);
            builder.Property(p => p.UomId).HasMaxLength(255);
            builder.Property(p => p.UomUnit).HasMaxLength(255);
            builder.Property(p => p.Grade).HasMaxLength(255);
            builder.Property(p => p.ProductRemark).HasMaxLength(1000);
        }
    }
}
