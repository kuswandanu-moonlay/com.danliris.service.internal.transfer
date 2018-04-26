using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferDeliveryOrderConfig
{
    class TransferDeliveryOrderConfig : IEntityTypeConfiguration<TransferDeliveryOrder>
    {
        public void Configure(EntityTypeBuilder<TransferDeliveryOrder> builder)
        {
            builder.Property(c => c.DONo).HasMaxLength(255);
            builder.Property(c => c.SupplierName).HasMaxLength(500);
            builder.Property(c => c.Remark).HasMaxLength(500);

            builder
                .HasMany(h => h.TransferDeliveryOrderItem)
                .WithOne(w => w.transferDeliveryOrder)
                .HasForeignKey(f => f.Id)
                .IsRequired();
        }
    }
}
