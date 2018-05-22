using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferDeliveryOrderConfig
{
    class TransferDeliveryOrderDetailConfig : IEntityTypeConfiguration<TransferDeliveryOrderDetail>
    {
        public void Configure(EntityTypeBuilder<TransferDeliveryOrderDetail> builder)
        {
            builder.Property(p => p.Grade).HasMaxLength(100);
            builder.Property(p => p.ShippingOrderQuantity).HasDefaultValue(0);
            builder.Property(p => p.TRId).HasMaxLength(100);
            builder.Property(p => p.TRNo).HasMaxLength(255);
        }
    }
}
