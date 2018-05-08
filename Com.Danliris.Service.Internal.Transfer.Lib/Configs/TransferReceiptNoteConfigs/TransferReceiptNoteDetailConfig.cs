using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferReceiptNoteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferReceiptNoteConfigs
{
    public class TransferReceiptNoteDetailConfig : IEntityTypeConfiguration<TransferReceiptNoteDetail>
    {
        public void Configure(EntityTypeBuilder<TransferReceiptNoteDetail> builder)
        {
            builder.Property(p => p.DONo).HasMaxLength(255);
            builder.Property(p => p.ETONo).HasMaxLength(255);
            builder.Property(p => p.TRNo).HasMaxLength(255);
            builder.Property(p => p.ProductId).HasMaxLength(255);
            builder.Property(p => p.ProductCode).HasMaxLength(255);
            builder.Property(p => p.ProductName).HasMaxLength(255);
            builder.Property(p => p.Grade).HasMaxLength(255);
            builder.Property(p => p.UomId).HasMaxLength(255);
            builder.Property(p => p.UomUnit).HasMaxLength(255);
            builder.Property(p => p.ProductRemark).HasMaxLength(1000);
        }
    }
}
