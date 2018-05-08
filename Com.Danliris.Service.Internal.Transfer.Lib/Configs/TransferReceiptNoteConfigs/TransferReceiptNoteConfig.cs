using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferReceiptNoteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferReceiptNoteConfigs
{
    public class TransferReceiptNoteConfig : IEntityTypeConfiguration<TransferReceiptNote>
    {
        public void Configure(EntityTypeBuilder<TransferReceiptNote> builder)
        {
            builder.Property(p => p.TRNNo).HasMaxLength(255);
            builder.Property(p => p.DivisionId).HasMaxLength(255);
            builder.Property(p => p.DivisionCode).HasMaxLength(255);
            builder.Property(p => p.DivisionName).HasMaxLength(255);
            builder.Property(p => p.UnitId).HasMaxLength(255);
            builder.Property(p => p.UnitCode).HasMaxLength(255);
            builder.Property(p => p.UnitName).HasMaxLength(255);
            builder.Property(p => p.SupplierId).HasMaxLength(255);
            builder.Property(p => p.SupplierCode).HasMaxLength(255);
            builder.Property(p => p.SupplierName).HasMaxLength(255);
            builder.Property(p => p.SONo).HasMaxLength(255);
            builder.Property(p => p.StorageId).HasMaxLength(255);
            builder.Property(p => p.StorageCode).HasMaxLength(255);
            builder.Property(p => p.StorageName).HasMaxLength(255);
            builder.Property(p => p.Remark).HasMaxLength(1000);

            builder
                .HasMany(h => h.TransferReceiptNoteDetails)
                .WithOne(w => w.TransferReceiptNote)
                .HasForeignKey(f => f.TRNId)
                .IsRequired();
        }
    }
}
