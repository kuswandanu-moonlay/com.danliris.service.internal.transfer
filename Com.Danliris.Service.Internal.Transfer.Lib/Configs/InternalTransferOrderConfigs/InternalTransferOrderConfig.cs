using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.InternalTransferOrderConfigs
{
    public class InternalTransferOrderConfig : IEntityTypeConfiguration<InternalTransferOrder>
    {
        public void Configure(EntityTypeBuilder<InternalTransferOrder> builder)
        {
            builder.Property(p => p.ITONo).HasMaxLength(50);
            builder.Property(p => p.TRNo).HasMaxLength(50);

            builder
                .HasMany(h => h.InternalTransferOrderDetails)
                .WithOne(w => w.InternalTransferOrder)
                .HasForeignKey(f => f.ITOId)
                .IsRequired();
        }
    }
}
