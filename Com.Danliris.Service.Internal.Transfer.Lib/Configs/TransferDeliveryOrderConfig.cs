using Com.Danliris.Service.Internal.Transfer.Lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs
{
    class TransferDeliveryOrderConfig : IEntityTypeConfiguration<TransferDeliveryOrder>
    {
        public void Configure(EntityTypeBuilder<TransferDeliveryOrder> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(100);
            builder.Property(c => c.SupplierName).HasMaxLength(500);
            builder.Property(c => c.Remark).HasMaxLength(500);
        }
    }
}
