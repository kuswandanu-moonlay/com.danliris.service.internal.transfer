using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferDeliveryOrderConfig
{
    class TransferDeliveryOrderItemConfig : IEntityTypeConfiguration<TransferDeliveryOrderItem>
    {
        public void Configure(EntityTypeBuilder<TransferDeliveryOrderItem> builder)
        {
            builder.Property(p => p.DOId).HasMaxLength(100);
            builder.Property(p => p.ETOId).HasMaxLength(100);
            builder.Property(p => p.ETONo).HasMaxLength(255);
            builder.Property(p => p.TRId).HasMaxLength(100);
            builder.Property(p => p.TRNo).HasMaxLength(255);
            builder.Property(p => p.ITOId).HasMaxLength(100);
            builder.Property(p => p.ITONo).HasMaxLength(255);

            builder
                .HasMany(h => h.transferDeliveryOrderDetail)
                .WithOne(w => w.transferDeliveryOrderItem)
                .HasForeignKey(f => f.DOItemId)
                .IsRequired();
        }
    }
}
