using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.InternalTransferOrderConfigs
{
    class InternalTransferOrderDetailConfig : IEntityTypeConfiguration<InternalTransferOrderDetail>
    {
        public void Configure(EntityTypeBuilder<InternalTransferOrderDetail> builder)
        {
            builder.Property(p => p.ProductId).HasMaxLength(100);
            builder.Property(p => p.ProductRemark).HasMaxLength(1000);
        }
    }
}
