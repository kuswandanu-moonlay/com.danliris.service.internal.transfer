using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferRequestConfig
{
    public class TransferRequestConfig : IEntityTypeConfiguration<TransferRequest>
    {
        public void Configure(EntityTypeBuilder<TransferRequest> builder)
        {
            builder.Property(p => p.TRNo).HasMaxLength(255);
            builder.Property(p => p.TRDate).HasMaxLength(255);
            builder.Property(p => p.RequestedArrivalDate).HasMaxLength(255);
            builder.Property(p => p.UnitId).HasMaxLength(255);
            builder.Property(p => p.UnitCode).HasMaxLength(255);
            builder.Property(p => p.UnitName).HasMaxLength(255);
            builder.Property(p => p.CategoryId).HasMaxLength(255);
            builder.Property(p => p.CategoryCode).HasMaxLength(255);
            builder.Property(p => p.CategoryName).HasMaxLength(255);
            builder.Property(p => p.Remark).HasMaxLength(255);

            //builder
            //    .HasMany(h => h.TransferRequestDetail)
            //    .WithOne(w => w.TransferRequest)
            //    .HasForeignKey(f => f.TransferRequestId)
            //    .IsRequired();
        }
    }
}
