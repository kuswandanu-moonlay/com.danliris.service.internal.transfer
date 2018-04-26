using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.ExternalTransferOrderConfigs
{
    class ExternalTransferOrderItemConfig : IEntityTypeConfiguration<ExternalTransferOrderItem>
    {
        public void Configure(EntityTypeBuilder<ExternalTransferOrderItem> builder)
        {
            builder.Property(p => p.ITONo).HasMaxLength(255);
            builder.Property(p => p.TRNo).HasMaxLength(255);

            builder
                .HasMany(h => h.ExternalTransferOrderDetails)
                .WithOne(w => w.ExternalTransferOrderItem)
                .HasForeignKey(f => f.ETOItemId)
                .IsRequired();
        }
    }
}
