using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.ExternalTransferOrderConfig
{
    class ExternalTransferOrderItemConfig : IEntityTypeConfiguration<ExternalTransferOrderItem>
    {
        public void Configure(EntityTypeBuilder<ExternalTransferOrderItem> builder)
        {
            builder.Property(p => p.InternalTransferOrderNo).HasMaxLength(255);
            builder.Property(p => p.TransferRequestNo).HasMaxLength(255);

            builder
                .HasMany(h => h.ExternalTransferOrderDetails)
                .WithOne(w => w.ExternalTransferOrderItem)
                .HasForeignKey(f => f.ExternalTransferOrderItemId)
                .IsRequired();
        }
    }
}
