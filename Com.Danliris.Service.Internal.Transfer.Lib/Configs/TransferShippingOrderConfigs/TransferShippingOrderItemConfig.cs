using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferShippingOrderConfigs
{
    public class TransferShippingOrderItemConfig : IEntityTypeConfiguration<TransferShippingOrderItem>
    {
        public void Configure(EntityTypeBuilder<TransferShippingOrderItem> builder)
        {
            builder.Property(p => p.TRNo).HasMaxLength(255);
            builder.Property(p => p.ETONo).HasMaxLength(255);
            builder.Property(p => p.ITONo).HasMaxLength(255);

            builder
                .HasMany(h => h.TransferShippingOrderDetails)
                .WithOne(w => w.TransferShippingOrderItem)
                .HasForeignKey(f => f.SOItemId)
                .IsRequired();
        }
    }
}
