using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferShippingOrderConfigs
{
    public class TransferShippingOrderConfig : IEntityTypeConfiguration<TransferShippingOrder>
    {
        public void Configure(EntityTypeBuilder<TransferShippingOrder> builder)
        {
            builder.Property(p => p.SupplierId).HasMaxLength(255);
            builder.Property(p => p.SupplierCode).HasMaxLength(255);
            builder.Property(p => p.SupplierName).HasMaxLength(255);
            builder.Property(p => p.Remark).HasMaxLength(1000);

            builder
                .HasMany(h => h.TransferShippingOrderItems)
                .WithOne(w => w.TransferShippingOrder)
                .HasForeignKey(f => f.SOId)
                .IsRequired();
        }
    }
}

